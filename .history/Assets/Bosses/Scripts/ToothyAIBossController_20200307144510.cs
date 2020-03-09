using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyAIBossController : AIBossController
{
  [SerializeField] float m_AttackRange = 3f;

  private Dictionary<string, float> m_State =  {
    "Idle": 0
    "Intro": 1
    "Chase": 2
    "Combat": 3
  };


  // Update is called once per frame
  private void Update()
  {
    var distanceFromPlayer = GetDistanceFromPlayer();

    if (distanceFromPlayer <= m_AttackRange)
    {
      // attack
      m_Animator.Set
    }
    else
    {
      //chase 
    }
  }

}
