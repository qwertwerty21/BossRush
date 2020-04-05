using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UppercutAttack : MonoBehaviour
{
  [SerializeField] private Damage m_Damage;

  [SerializeField] private float m_MaxChargeTime = 5f;

  private float m_CurrentChargeTime;
  private Animator m_Animator;
  private MeshRenderer m_MeshRenderer;

  private BaseHitBox m_BaseHitBox;

  private PlayerController m_PlayerController;

  public void EndUppercutAttack()
  {
    if (m_MeshRenderer.enabled)
    {

      m_MeshRenderer.enabled = false;
    }
    m_PlayerController.ToggleHitboxColliders("UppercutAttack", false);
  }

  // Start is called before the first frame update
  private void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    m_MeshRenderer = gameObject.GetComponent<MeshRenderer>();
    m_BaseHitBox = gameObject.GetComponent<BaseHitBox>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButton("Fire2"))
    {
      if (!m_MeshRenderer.enabled)
      {

        m_MeshRenderer.enabled = true;
      }
      if (!m_Animator.GetBool("isChargingUppercutAttack"))
      {
        m_Animator.SetBool("isChargingUppercutAttack", true);
      }
      m_CurrentChargeTime += Time.deltaTime;
    }
    if (Input.GetButtonUp("Fire2") || m_CurrentChargeTime >= m_MaxChargeTime)
    {
      m_Animator.SetBool("isChargingUppercutAttack", false);
      m_Animator.SetTrigger("uppercutAttack");
      m_PlayerController.ToggleHitboxColliders("UppercutAttack", true);
    }
  }

  private void OnTriggerEnter(Collider otherCollider)
  {
    Debug.Log("YO YOU HIT SOMETHING WITH UPPERCUT ATTACK" + otherCollider);
    if (otherCollider.gameObject.tag == "Enemy")
    {
      Rigidbody enemyRigidBody = otherCollider.gameObject.GetComponent<Rigidbody>();
      Target enemyTarget = otherCollider.gameObject.GetComponent<Target>();
      NavMeshAgent enemyNavMeshAgent = otherCollider.gameObject.GetComponent<NavMeshAgent>();
      Animator enemyAnimator = otherCollider.gameObject.GetComponent<Animator>();
      // BaseHitBox hitbox = otherCollider.gameObject.GetComponent<BaseHitBox>();
      Vector3 direction = m_BaseHitBox.GetDirection(enemyRigidBody);
      float force = m_Damage.m_KnockbackForce;
      direction.y = 5;
      // Vector3 fuck = Vector3.forward * force;
      // fuck.y = 1000f;
      enemyNavMeshAgent.enabled = false;
      Debug.Log("fuckfasdkfdsak;lkl;" + enemyRigidBody);
      enemyRigidBody.AddForce(direction * force, ForceMode.Impulse);
      enemyTarget.TakeDamage(m_Damage);
    }
  }
}
