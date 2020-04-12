using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class AnimatorHelper : MonoBehaviour
{

  private PlayerController m_PlayerController;

  void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
  }
  public void EnableHitboxCollider(string hitboxName)
  {
    m_PlayerController.ToggleHitboxColliders(hitboxName, true);
  }

  public void DisableHitboxCollider(string hitboxName)
  {
    m_PlayerController.ToggleHitboxColliders(hitboxName, false);
  }
}
