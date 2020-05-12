using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyTarget : Target
{
  private Animator m_Animator;

  override public void TakeDamage(Damage damage)
  {
    base.TakeDamage(damage);
    if (damage.m_KnockbackForce > 0f)
    {
      m_Animator.SetTrigger("stagger");
    }
  }

  override protected void Awake()
  {
    base.Awake();
    m_Animator = GetComponent<Animator>();
  }
}