﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BossBaseFSM
{
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateEnter(animator, stateInfo, layerIndex);
    m_Agent.SetDestination(m_Player.transform.position);
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    Debug.Log("player position" + m_Player.transform.position);
    m_Boss.Move(m_Agent.desiredVelocity);
  }
}