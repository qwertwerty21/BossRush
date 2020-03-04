using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseFSM : StateMachineBehaviour
{
  private GameObject m_Boss;
  private GameObject m_Player;
  private UnityEngine.AI.NavMeshAgent m_Agent;
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    m_Boss = animator.gameObject;
    m_Agent = m_Boss.GetComponent<UnityEngine.AI.NavMeshAgent>();
    m_Player = m_Boss.GetComponent<AIBossController>().GetPlayer();
  }
}
