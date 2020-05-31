using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyTarget : Target {

  [SerializeField] GameObject m_HitEffect;

  [SerializeField] private float m_StaggerThreshold = 5f;

  private Animator m_Animator;

  private void CreateHitImpact (GameObject effect, Vector3 position) {
    Debug.Log ("FUCKING POSITION" + position);
    GameObject impact = Instantiate (effect, position, Quaternion.identity);
    Destroy (impact, 1f);
  }

  override public void TakeDamage (Damage damage, Vector3 hitPosition) {
    base.TakeDamage (damage, hitPosition);
    CreateHitImpact (m_HitEffect, hitPosition);
    if (damage.m_KnockbackForce > m_StaggerThreshold) {
      m_Animator.SetTrigger ("stagger");
    }
  }

  override protected void Awake () {
    base.Awake ();
    m_Animator = GetComponent<Animator> ();
  }
}