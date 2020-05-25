using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class UppercutAttack : MonoBehaviour
{

  [SerializeField] private float m_MaxChargeDuration = 5f;

  [SerializeField] private float m_HitTimeScaleSlowdownDuration = 1f;

  [SerializeField] private float m_YKnockbackForceOverride = 3f;

  [SerializeField] private float m_TimeScaleSlowdown = .4f;

  [SerializeField] public TimeManager m_TimeManager;

  private float m_CurrentChargeDuration;
  private Animator m_Animator;

  private BaseHitBox m_BaseHitBox;

  private PlayerController m_PlayerController;

  private NavMeshAgent m_EnemyNavMeshAgent;
  private Rigidbody m_EnemyRigidBody;
  private AIBossController m_EnemyAIBossController;


  // IEnumerator EndUppercutAttack()
  // {
  //   yield return new WaitForSecondsRealtime(0.5f);
  //   m_PlayerController.ToggleHitboxColliders("UppercutAttack", false);
  //   m_CurrentChargeDuration = 0f;

  // }

  // IEnumerator ResetTimeScale()
  // {
  //   yield return new WaitForSecondsRealtime(m_HitTimeScaleSlowdownDuration);
  //   if (Time.timeScale < 1f)
  //   {
  //     Time.timeScale = 1f;
  //     Time.fixedDeltaTime = 0.02F;
  //   }
  // }

  IEnumerator ResetEnemy()
  {
    yield return new WaitForSecondsRealtime(m_HitTimeScaleSlowdownDuration);
    // m_EnemyAIBossController.m_IsNavMeshAgentUpdating = true;

  }

  IEnumerator ResetChargeDuration()
  {
    yield return new WaitForSecondsRealtime(.5f);
    m_CurrentChargeDuration = 0f;
  }

  // Start is called before the first frame update
  private void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    m_BaseHitBox = gameObject.GetComponent<BaseHitBox>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButton("Fire2"))
    {
      m_PlayerController.ToggleHitboxColliders("UppercutAttack", false);

      if (!m_Animator.GetBool("isChargingUppercutAttack"))
      {
        m_Animator.SetBool("canSwitchWeapon", false);
        m_Animator.SetBool("isChargingUppercutAttack", true);
      }
      m_CurrentChargeDuration += Time.deltaTime;
      Debug.Log("CURRENTCHARGET" + m_CurrentChargeDuration);
    }
    if (Input.GetButtonUp("Fire2") || m_CurrentChargeDuration >= m_MaxChargeDuration)
    {
      m_Animator.SetBool("isChargingUppercutAttack", false);
      m_Animator.SetTrigger("uppercutAttack");
      m_Animator.SetBool("canSwitchWeapon", true);
      StartCoroutine(ResetChargeDuration());
      // m_PlayerController.ToggleHitboxColliders("UppercutAttack", true);
      // StartCoroutine(EndUppercutAttack());

    }
  }

  private void OnTriggerEnter(Collider otherCollider)
  {
    Debug.Log("YO YOU HIT SOMETHING WITH UPPERCUT ATTACK" + otherCollider);
    if (otherCollider.gameObject.tag == "Enemy")
    {
      Debug.Log("CHARAGE TIEM " + m_CurrentChargeDuration);
      m_EnemyRigidBody = otherCollider.gameObject.GetComponent<Rigidbody>();
      m_EnemyAIBossController = otherCollider.gameObject.GetComponent<AIBossController>();

      Target enemyTarget = otherCollider.gameObject.GetComponent<Target>();

      Vector3 direction = m_BaseHitBox.GetDirection(m_EnemyRigidBody);
      Damage damage = m_BaseHitBox.m_DamageHash["UppercutAttack"];
      float force = damage.m_KnockbackForce;
      direction.y = m_YKnockbackForceOverride;



      // Time.timeScale = Mathf.Clamp(1 / (m_TimeScaleSlowdown * m_CurrentChargeDuration), .1f, 1);
      // Time.fixedDeltaTime = 0.02F * Time.timeScale;
      // Debug.Log("TIMESCALE" + Time.timeScale);
      m_TimeManager.DoSlowmotion();

      m_EnemyAIBossController.m_IsNavMeshAgentUpdating = false;
      m_EnemyRigidBody.AddForce(direction * force, ForceMode.Impulse);

      StartCoroutine(ResetTimeScale());
      StartCoroutine(ResetEnemy());


      var originalDamageAmount = damage.m_DamageAmount;
      damage.m_DamageAmount *= m_CurrentChargeDuration;
      enemyTarget.TakeDamage(damage, transform.position);
      damage.m_DamageAmount = originalDamageAmount;
    }
  }
}
