using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseFSM : StateMachineBehaviour
{
  protected GameObject m_Boss;
  protected AIBossController m_BossController;
  protected GameObject m_Player;
  protected UnityEngine.AI.NavMeshAgent m_Agent;
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    m_Boss = animator.gameObject;
    m_Agent = m_Boss.GetComponent<UnityEngine.AI.NavMeshAgent>();
    m_BossController = m_Boss.GetComponent<AIBossController>();
    m_Player = m_BossController.GetPlayer();

    // SetDistanceFromPlayer(animator);
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    // GetDistanceFromPlayer(animator);
  }

}
