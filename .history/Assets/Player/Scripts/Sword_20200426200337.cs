using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Sword : MonoBehaviour
{
  [SerializeField] private float m_HitSuspensionDuration = 1f;
  [SerializeField] private float m_MaxChargeDuration = 5f;

  private CustomCrosshair m_CustomCrosshair;
  private float m_CurrentChargeDuration;
  private Animator m_Animator;

  private CharacterController m_CharacterController;
  private Camera m_Camera;
  private BaseHitBox m_BaseHitBox;

  private PlayerController m_PlayerController;

  private float m_AttackLastTapped = -0.1f;

  private NavMeshAgent m_EnemyNavMeshAgent;
  private Rigidbody m_EnemyRigidBody;

  private SwordComboType m_CurrentComboType;

  private enum SwordComboType
  {
    LightSwordCombo,
    HeavySwordCombo
  }

  IEnumerator ResetEnemy()
  {
    yield return new WaitForSecondsRealtime(m_HitSuspensionDuration);
    m_CustomCrosshair.SetCrosshairColor(Color.white);
    m_EnemyRigidBody.transform.position = new Vector3(m_EnemyRigidBody.transform.position.x, 0f, m_EnemyRigidBody.transform.position.z);

    if (!m_EnemyNavMeshAgent.enabled)
    {

      m_EnemyNavMeshAgent.enabled = true;
    }
    if (!m_EnemyRigidBody.isKinematic)
    {
      m_EnemyRigidBody.isKinematic = true;
    }
  }

  // public void DisableSwordHitboxCollider()
  // {
  //   m_PlayerController.ToggleHitboxColliders("LightSwordCombo", true);
  // }

  // private void OnEnable()
  // {
  //   m_CustomCrosshair.EnableCrosshair();
  // }

  // Start is called before the first frame update
  private void Awake()
  {
    m_Camera = Camera.main;
    m_CharacterController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CharacterController>();
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    m_BaseHitBox = gameObject.GetComponent<BaseHitBox>();
    m_CustomCrosshair = GetComponent<CustomCrosshair>();

  }

  // Update is called once per frame
  void Update()
  {

    if (Input.GetButtonDown("Fire1"))
    {
      // needed so we can attack in mid-air
      m_Animator.SetTrigger("isInterruptingJump");
      m_Animator.ResetTrigger("jump");
      m_Animator.SetTrigger("lightSwordAttack");
      m_CurrentComboType = SwordComboType.LightSwordCombo;
      // if (!m_Animator.GetBool("isGrounded"))
      // {
      //   m_PlayerController.m_GravityMultiplier = .3f;
      // }
      // else
      // {
      //   m_PlayerController.m_GravityMultiplier = 1f;
      // }
      // var direction = new Vector3(Vector3.forward, 0f, m_Camera.transform.position.z);
      // m_PlayerController.AddImpact(Vector3.forward, 5f);
      // var moveDir = new Vector3(m_Camera.transform.position.x, 0, m_Camera.transform.position.z);
      // m_CharacterController.Move(Vector3.forward * 10f * Time.deltaTime);


      // if (!m_Animator.GetBool("isChargingUppercutAttack"))
      // {
      //   m_Animator.SetBool("isChargingUppercutAttack", true);
      // }
      // m_CurrentChargeDuration += Time.deltaTime;
      // Debug.Log("CURRENTCHARGET" + m_CurrentChargeDuration);
    }

    if (Input.GetButton("Fire2"))
    {
      if (!m_Animator.GetBool("isChargingHeavySwordAttack"))
      {
        m_Animator.SetBool("canSwitchWeapon", false);
        m_Animator.SetBool("isChargingHeavySwordAttack", true);
      }
      m_CurrentChargeDuration += Time.deltaTime;
    }

    if (Input.GetButtonUp("Fire2") || m_CurrentChargeDuration >= m_MaxChargeDuration)
    {
      m_Animator.SetBool("isChargingHeavySwordAttack", false);
      m_Animator.SetTrigger("heavySwordAttack");
      m_CurrentComboType = SwordComboType.HeavySwordCombo;
      m_Animator.SetBool("canSwitchWeapon", !m_PlayerController.m_IsLockedOn);
      m_CurrentChargeDuration = 0f;
      // m_PlayerController.ToggleHitboxColliders("UppercutAttack", true);
      // StartCoroutine(EndUppercutAttack());

    }
  }

  private void OnTriggerEnter(Collider otherCollider)
  {
    if (otherCollider.gameObject.tag == "Enemy")
    {
      m_CustomCrosshair.SetCrosshairColor(Color.red);
      m_EnemyRigidBody = otherCollider.gameObject.GetComponent<Rigidbody>();
      m_EnemyNavMeshAgent = otherCollider.gameObject.GetComponent<NavMeshAgent>();
      Target enemyTarget = otherCollider.gameObject.GetComponent<Target>();

      Vector3 direction = m_BaseHitBox.GetDirection(m_EnemyRigidBody);
      Damage damage = m_BaseHitBox.m_DamageHash[m_CurrentComboType.ToString()];
      float force = damage.m_KnockbackForce;

      m_EnemyRigidBody.isKinematic = false;
      m_EnemyNavMeshAgent.enabled = false;
      StartCoroutine(ResetEnemy());

      m_EnemyRigidBody.AddForce(direction * force, ForceMode.Impulse);
      enemyTarget.TakeDamage(damage);

    }
  }
}
