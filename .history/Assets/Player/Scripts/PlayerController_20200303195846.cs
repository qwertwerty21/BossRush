using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
  [RequireComponent(typeof(CharacterController))]
  [RequireComponent(typeof(AudioSource))]
  public class PlayerController : MonoBehaviour
  {
    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float m_WalkSpeed;
    [SerializeField] private float m_RunSpeed;
    [SerializeField] [Range(0f, 1f)] private float m_RunStepLengthen;
    [SerializeField] private float m_DoubleTapDelay = 0.2f;
    [SerializeField] private float m_DashThrust = 40f;
    [SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_MaxJumps = 2;
    [SerializeField] private float m_StickToGroundForce;
    [SerializeField] private float m_GravityMultiplier;
    [SerializeField] private MouseLook m_MouseLook;
    [SerializeField] private float m_StepInterval;
    [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
    [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

    private Animator m_Animator;
    private Camera m_Camera;
    private bool m_CanJump;
    private bool m_CanDash;
    private float m_CurrentJumpCount = 0f;
    private float m_YRotation;
    private Vector2 m_LocomotionInput;
    private Vector3 m_MoveDir = Vector3.zero;
    private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;
    private bool m_PreviouslyGrounded;
    private float m_StepCycle;
    private float m_NextStep;
    private bool m_IsJumping;
    private AudioSource m_AudioSource;
    private float m_DoubleTapLastTapped = -0.1f;

    // Use this for initialization
    private void Awake()
    {
      m_CharacterController = GetComponent<CharacterController>();
      m_Camera = Camera.main;
      m_StepCycle = 0f;
      m_NextStep = m_StepCycle / 2f;
      m_IsJumping = false;
      m_AudioSource = GetComponent<AudioSource>();
      m_MouseLook.Init(transform, m_Camera.transform);
      m_Animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
      RotateView();

      // reset jump if grounded
      if (m_CharacterController.isGrounded)
      {
        m_CurrentJumpCount = 0;
      }

      // the jump state needs to read here to make sure it is not missed
      m_CanJump = CrossPlatformInputManager.GetButtonDown("Jump") && m_CurrentJumpCount < m_MaxJumps;

      if (m_CanJump)
      {
        m_CurrentJumpCount++;
      }

      // dash
      if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
      {

        if ((Time.time - m_DoubleTapLastTapped) < m_DoubleTapDelay)
        {
          m_CanDash = true;
          Debug.Log("dashing");
        }
        m_DoubleTapLastTapped = Time.time;
      }

      // landed
      if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
      {
        PlayLandingSound();
        m_MoveDir.y = 0f;
        m_IsJumping = false;
      }

      m_Animator.SetBool("isGrounded", m_CharacterController.isGrounded);

      m_PreviouslyGrounded = m_CharacterController.isGrounded;
    }

    private void PlayLandingSound()
    {
      m_AudioSource.clip = m_LandSound;
      m_AudioSource.Play();
      m_NextStep = m_StepCycle + .5f;
    }

    private void FixedUpdate()
    {
      float speed;
      SetLocomotionInput(out speed);
      // always move along the camera forward as it is the direction that it being aimed at
      Vector3 desiredMove = transform.forward * m_LocomotionInput.y + transform.right * m_LocomotionInput.x;

      // get a normal for the surface that is being touched to move along it
      RaycastHit hitInfo;
      Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                         m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
      desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

      m_MoveDir.x = desiredMove.x * speed;
      m_MoveDir.z = desiredMove.z * speed;

      if (m_CharacterController.isGrounded)
      {
        m_MoveDir.y = -m_StickToGroundForce;
      }
      else
      {
        m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
      }

      if (m_CanDash)
      {
        m_MoveDir.z = m_MoveDir.z * m_DashThrust;
        m_MoveDir.x = m_MoveDir.x * m_DashThrust;
        m_CanDash = false;
      }

      if (m_CanJump)
      {
        m_Animator.SetTrigger("jump");
        m_MoveDir.y = m_JumpSpeed;
        PlayJumpSound();
        m_IsJumping = true;
      }

      m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

      ProgressStepCycle(speed);

      m_MouseLook.UpdateCursorLock();
    }

    private void PlayJumpSound()
    {
      m_AudioSource.clip = m_JumpSound;
      m_AudioSource.Play();
    }

    private void ProgressStepCycle(float speed)
    {
      if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_LocomotionInput.x != 0 || m_LocomotionInput.y != 0))
      {
        m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunStepLengthen))) *
                     Time.fixedDeltaTime;
      }

      if (!(m_StepCycle > m_NextStep))
      {
        return;
      }

      m_NextStep = m_StepCycle + m_StepInterval;

      PlayFootStepAudio();
    }

    private void PlayFootStepAudio()
    {
      if (!m_CharacterController.isGrounded)
      {
        return;
      }
      // pick & play a random footstep sound from the array,
      // excluding sound at index 0
      int n = Random.Range(1, m_FootstepSounds.Length);
      m_AudioSource.clip = m_FootstepSounds[n];
      m_AudioSource.PlayOneShot(m_AudioSource.clip);
      // move picked sound to index 0 so it's not picked next time
      m_FootstepSounds[n] = m_FootstepSounds[0];
      m_FootstepSounds[0] = m_AudioSource.clip;
    }

    private void SetLocomotionInput(out float speed)
    {
      // Read input
      float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
      float vertical = CrossPlatformInputManager.GetAxis("Vertical");

#if !MOBILE_INPUT
      // On standalone builds, walk/run speed is modified by a key press.
      // keep track of whether or not the character is walking or running
      m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif

      // set the desired speed to be walking or running
      speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
      m_LocomotionInput = new Vector2(horizontal, vertical);

      m_Animator.SetFloat("horizontal", horizontal);
      m_Animator.SetFloat("vertical", vertical);

      // normalize input if it exceeds 1 in combined length:
      if (m_LocomotionInput.sqrMagnitude > 1)
      {
        m_LocomotionInput.Normalize();
      }

    }

    private void RotateView()
    {
      m_MouseLook.LookRotation(transform, m_Camera.transform);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
      Debug.Log("hit by " + hit.collider.name);
      Rigidbody body = hit.collider.attachedRigidbody;
      //dont move the rigidbody if the character is on top of it
      if (m_CollisionFlags == CollisionFlags.Below)
      {
        return;
      }

      if (body == null || body.isKinematic)
      {
        return;
      }
      body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }
  }
}