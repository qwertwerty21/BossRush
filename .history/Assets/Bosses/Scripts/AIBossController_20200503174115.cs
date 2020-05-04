using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIBossController : MonoBehaviour
{
  [SerializeField] private GameObject m_Player;
  [SerializeField] float m_MovingTurnSpeed = 360;
  [SerializeField] float m_StationaryTurnSpeed = 180;
  [SerializeField] public GameObject[] m_Prefabs;
  private float m_TurnAmount;
  private float m_ForwardAmount;
  protected Animator m_Animator;

  protected Rigidbody m_RigidBody;

  private Vector3 m_Impact = Vector3.zero;

  public GameObject GetPlayer()
  {
    return m_Player;
  }

  public virtual void Move(Vector3 move)
  {

    // convert the world relative moveInput vector into a local-relative
    // turn amount and forward amount required to head in the desired
    // direction.
    if (move.magnitude > 1f) move.Normalize();
    move = transform.InverseTransformDirection(move);
    // CheckGroundStatus();
    move = Vector3.ProjectOnPlane(move, Vector3.up);
    m_TurnAmount = Mathf.Atan2(move.x, move.z);
    m_ForwardAmount = move.z;

    ApplyExtraTurnRotation();

    // // control and velocity handling is different when grounded and airborne:
    // if (m_IsGrounded)
    // {
    //   HandleGroundedMovement(crouch, jump);
    // }

    // send input and other state parameters to the animator
    UpdateLocomotionAnimator(move);
  }

  public virtual void RotateTowards(Transform target)
  {
    Vector3 direction = (target.position - transform.position).normalized;
    direction.y = 0f;
    Quaternion lookRotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_MovingTurnSpeed);
  }

  private void ApplyExtraTurnRotation()
  {
    // help the character turn faster (this is in addition to root rotation in the animation)
    float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
    transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
  }

  // set animator params in here
  private void UpdateLocomotionAnimator(Vector3 move)
  {
    // update the animator parameters
    m_Animator.SetFloat("horizontal", m_TurnAmount);
    m_Animator.SetFloat("vertical", m_ForwardAmount);
  }

  public float GetDistanceFromPlayer()
  {
    float distanceFromPlayer = Vector3.Distance(transform.position, m_Player.transform.position);
    return distanceFromPlayer;
  }

  public void ToggleHitboxColliders(string name, bool isEnabled)
  {
    BaseHitBox[] hitboxes = GetComponentsInChildren<BaseHitBox>();
    Debug.Log("toggle swipe hitboxes" + hitboxes);
    for (int i = 0; i < hitboxes.Length; i++)
    {
      if (name == hitboxes[i].m_HitBoxName)
      {
        hitboxes[i].m_Collider.enabled = isEnabled;
      }
    }
  }

  public void InstantiatePrefab(string name)
  {
    foreach (GameObject prefab in m_Prefabs)
    {
      if (prefab.name == name)
      {
        Instantiate(prefab, transform.localPosition, Quaternion.identity);
      }
    }
  }

  public void PlayParticleSystemEffect(string name)
  {
    ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
    Debug.Log("play  particleSystems" + particleSystems);
    for (int i = 0; i < particleSystems.Length; i++)
    {
      if (name == particleSystems[i].name)
      {
        particleSystems[i].Play();
      }
    }
  }

  void Awake()
  {
    m_Animator = GetComponent<Animator>();
    m_RigidBody = GetComponent<Rigidbody>();
  }

  void OnCollisionEnter(Collision otherCollider)
  {
    // if (otherCollider.gameObject.tag == "Ground")
    // {
    //   NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
    //   if (!navMeshAgent.enabled)
    //   {

    //     navMeshAgent.enabled = true;
    //   }
    //   // if (!m_RigidBody.isKinematic)
    //   // {
    //   //   m_RigidBody.isKinematic = true;
    //   // }
    // }
  }

  // void OnControllerColliderHit(ControllerColliderHit hit)
  // {
  //   var hitNormal = hit.normal;
  //   var controller = hit.collider.GetComponent<CharacterController>();
  //   if (controller != null)
  //   {
  //     Debug.Log("MOVE BITCH GET OUT TH EWYA ");
  //     controller.SimpleMove((new Vector3(100f * Time.deltaTime, 0, 100f * Time.deltaTime)));
  //   }
  // }

  // void Update()
  // {
  //   // apply the impact force:
  //   Move(m_Impact * Time.deltaTime);
  //   // consumes the m_Impact energy each cycle:
  //   m_Impact = Vector3.Lerp(m_Impact, Vector3.zero, 5 * Time.deltaTime);
  // }
}
