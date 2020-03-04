using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseFSM : StateMachineBehaviour
{
  public GameObject boss;
  public GameObject player;
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    boss = animator.gameObject;
    player = boss.GetComponent<AIBossController>().GetPlayer();
  }
}
