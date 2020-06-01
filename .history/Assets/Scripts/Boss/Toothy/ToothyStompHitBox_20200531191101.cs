using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyStompHitBox : BaseHitBox {

  override public Vector3 GetDirection (Rigidbody otherRigidBody) {
    Vector3 direction = otherRigidBody.transform.position - transform.position;
    direction.y = 20f;
    return direction;
  }

  // private static readonly Dictionary<string, int> m_State = new Dictionary<string, int> {
  //   {"Idle", 0},
  //   {"Intro", 1},
  //   {"Chase", 2},
  //   {"Combat", 3},
  // };

  // // Update is called once per frame
  // private void Update()
  // {
  //   var distanceFromPlayer = GetDistanceFromPlayer();

  //   if (distanceFromPlayer <= m_AttackRange)
  //   {

  //     // attack
  //     m_Animator.SetInteger("state", m_State["Combat"]);
  //   }
  //   else
  //   {
  //     //chase 
  //     m_Animator.SetInteger("state", m_State["Chase"]);
  //   }
  // }

}