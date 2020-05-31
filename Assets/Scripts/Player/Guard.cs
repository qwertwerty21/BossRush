using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Guard : MonoBehaviour
{
  private Animator m_Animator;

  private PlayerController m_PlayerController;


  // Start is called before the first frame update
  private void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.LeftShift))
    {

      m_Animator.SetBool("canSwitchWeapon", false);
      m_Animator.SetTrigger("block");
      m_Animator.SetBool("isGuarding", true);
      m_PlayerController.DisableHitboxColliders();

    }
    if (Input.GetKeyUp(KeyCode.LeftShift))
    {
      m_Animator.SetBool("isGuarding", false);
      m_Animator.SetBool("canSwitchWeapon", !m_PlayerController.m_IsLockedOn);

    }
  }

  // private void OnTriggerEnter(Collider otherCollider)
  // {
  //   Debug.Log("YO YOU HIT SOMETHING WITH UPPERCUT ATTACK" + otherCollider);
  //   if (otherCollider.gameObject.tag == "Enemy")
  //   {
  //     Rigidbody enemyRigidBody = otherCollider.gameObject.GetComponent<Rigidbody>();
  //     Target enemyTarget = otherCollider.gameObject.GetComponent<Target>();
  //     NavMeshAgent enemyNavMeshAgent = otherCollider.gameObject.GetComponent<NavMeshAgent>();
  //     Animator enemyAnimator = otherCollider.gameObject.GetComponent<Animator>();

  //     Vector3 direction = m_BaseHitBox.GetDirection(enemyRigidBody);
  //     float force = m_Damage.m_KnockbackForce;
  //     direction.y = Mathf.Floor(m_YKnockbackForceOverride * m_CurrentChargeDuration);

  //     enemyNavMeshAgent.enabled = false;

  //     Time.timeScale = Mathf.Clamp(1 / (m_TimeScaleSlowdown * m_CurrentChargeDuration), .4f, 1);
  //     Debug.Log("TIMESCALE" + Time.timeScale);

  //     StartCoroutine(ResetTimeScale());

  //     enemyRigidBody.AddForce(direction * force, ForceMode.Impulse);
  //     var originalDamageAmount = m_Damage.m_DamageAmount;
  //     m_Damage.m_DamageAmount *= m_CurrentChargeDuration;
  //     enemyTarget.TakeDamage(m_Damage);
  //     m_Damage.m_DamageAmount = originalDamageAmount;
  //   }
  // }
}
