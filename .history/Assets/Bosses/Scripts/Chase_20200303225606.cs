using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BossBaseFSM
{
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateEnter(animator, stateInfo, layerIndex);
    agent.SetDestination(m_Player.position);
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    Debug.Log("fuck");
  }
}
