using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyTarget : Target
{

  [SerializeField] GameObject m_HitEffect;

  private Animator m_Animator;

  private void CreateHitImpact(GameObject effect, Vector3 position)
  {
    GameObject impact = Instantiate(effect, position, Quaternion.Identity);
    Destroy(impact, 1f);
  }

  override public void TakeDamage(Damage damage, Vector3 hitPosition)
  {
    base.TakeDamage(damage, hitPosition);
    CreateHitImpact(m_HitEffect, hitPosition);
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