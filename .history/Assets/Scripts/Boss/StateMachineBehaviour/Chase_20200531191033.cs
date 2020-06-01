using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseBossStateMachineBehaviour {

  // var to keep track of stopping distance?
  override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    base.OnStateEnter (animator, stateInfo, layerIndex);

  }

  override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    base.OnStateUpdate (animator, stateInfo, layerIndex);
    if (m_Agent.enabled) {

      m_Agent.SetDestination (m_Player.transform.position);
      m_AIBossController.Move (m_Agent.desiredVelocity);
    }

  }
}