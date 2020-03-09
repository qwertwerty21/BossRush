using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAttack : StateMachineBehaviour
{

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateEnter(animator, stateInfo, layerIndex);

  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateUpdate(animator, stateInfo, layerIndex);
    // Debug.Log("player position" + m_Player.transform.position);
    m_Agent.SetDestination(m_Player.transform.position);
    m_BossController.Move(m_Agent.desiredVelocity);

  }

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    
  //}

  // OnStateMove is called right after Animator.OnAnimatorMove()
  //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    // Implement code that processes and affects root motion
  //}

  // OnStateIK is called right after Animator.OnAnimatorIK()
  //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    // Implement code that sets up animation IK (inverse kinematics)
  //}
}
