using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyAIBossController : AIBossController
{
  [SerializeField] float m_AttackRange = 2f;

  private static readonly Dictionary<string, int> m_State = new Dictionary<string, int> { { "Idle", 0 },
    { "Intro", 1 },
    { "Chase", 2 },
    { "Combat", 3 },
  };

  // override protected void Awake(){
  //   base.Awake();
  // }

  // Update is called once per frame
  override protected void Update()
  {
    // base.Update();
    var distanceFromPlayer = GetDistanceFromPlayer();
    // Debug.Log("Distance " + m_AttackRange);

    if (distanceFromPlayer <= m_AttackRange)
    {
      Debug.Log("attack " + m_State["Combat"]);

      // attack
      m_Animator.SetInteger("state", m_State["Combat"]);
    }
    else
    {
      // Debug.Log("chase " + distanceFromPlayer);
      //chase 
      m_Animator.SetInteger("state", m_State["Chase"]);
    }
  }



}