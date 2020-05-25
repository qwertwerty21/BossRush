using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyTarget : Target
{

  [SerializeField] GameObject m_HitEffect;

  private Animator m_Animator;

  private void CreateHitImpact(GameObject effect, RaycastHit hit)
  {
    GameObject impact = Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal));
    Destroy(impact, 1f);
  }

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