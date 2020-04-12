using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class AnimatorController : MonoBehaviour
{

  private PlayerController m_PlayerController;

  void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
  }
  public void DisableSwordHitboxCollider()
  {
    m_PlayerController.ToggleHitboxColliders("LightSwordCombo", false);
  }
}
