using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class AnimatorHelper : MonoBehaviour
{

  private PlayerController m_PlayerController;
  private Animator m_Animator;


  void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    m_Animator = GameObject.GetComponent<Animator>();
  }
  public void EnableHitboxCollider(string hitboxName)
  {
    m_PlayerController.ToggleHitboxColliders(hitboxName, true);
  }

  public void DisableHitboxCollider(string hitboxName)
  {
    m_PlayerController.ToggleHitboxColliders(hitboxName, false);
  }

  public void ToggleAnimatorParameter(string paramName, bool isEnabled)
  {
    m_Anim
  }
}
