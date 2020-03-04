using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossController : MonoBehaviour
{
  [SerializeField] private GameObject m_Player;
  private Animator m_Animator;

  public GameObject GetPlayer()
  {
    return m_Player;
  }

  public void Move(Vector3 move)
  {

    // convert the world relative moveInput vector into a local-relative
    // turn amount and forward amount required to head in the desired
    // direction.
    if (move.magnitude > 1f) move.Normalize();
    move = transform.InverseTransformDirection(move);
    // CheckGroundStatus();
    move = Vector3.ProjectOnPlane(move, m_GroundNormal);
    m_TurnAmount = Mathf.Atan2(move.x, move.z);
    m_ForwardAmount = move.z;

    // ApplyExtraTurnRotation();

    // control and velocity handling is different when grounded and airborne:
    if (m_IsGrounded)
    {
      HandleGroundedMovement(crouch, jump);
    }
    else
    {
      HandleAirborneMovement();
    }

    ScaleCapsuleForCrouching(crouch);
    PreventStandingInLowHeadroom();

    // send input and other state parameters to the animator
    UpdateLocomotionAnimator(move);
  }
  // set animator params in here

  void Awake()
  {
    m_Animator = GetComponent<Animator>();
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
