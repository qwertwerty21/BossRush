using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UppercutAttack : MonoBehaviour
{
  [SerializeField] private Damage m_Damage;
  private Animator m_Animator;
  private MeshRenderer m_MeshRenderer;

  private PlayerController m_PlayerController;

  IEnumerator EndUppercutAttack()
  {
    yield return new WaitForSeconds(.8f);
    m_MeshRenderer.enabled = false;
    m_PlayerController.ToggleHitboxColliders("UppercutAttack", false);
  }

  // Start is called before the first frame update
  private void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    m_MeshRenderer = gameObject.GetComponent<MeshRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Fire2"))
    {
      m_MeshRenderer.enabled = true;
      m_Animator.SetBool("isChargingUppercutAttack", true);
    }
    if (Input.GetButtonUp("Fire2"))
    {
      m_PlayerController.ToggleHitboxColliders("UppercutAttack", true);
      m_Animator.SetBool("isChargingUppercutAttack", false);
      m_Animator.SetTrigger("uppercutAttack");
      StartCoroutine(EndUppercutAttack());
    }
  }

  private void OnTriggerEnter(Collider otherCollider)
  {
    Debug.Log("YO YOU HIT SOMETHING WITH UPPERCUT ATTACK" + otherCollider);
    if (otherCollider.gameObject.tag == "Enemy")
    {
      Rigidbody rb = otherCollider.gameObject.GetComponent<Rigidbody>();
      // BaseHitBox enemyHitbox = otherCollider.gameObject.GetComponent<BaseHitBox>();
      // Vector3 direction = enemyHitbox.GetDirection(m_RigidBody);
      float force = m_Damage.m_KnockbackForce * 100;
      Debug.Log("fuckfasdkfdsak;lkl;" + rb);
      rb.AddExplosionForce(Vector3.forward * 500f, transform.position, 10f, 10f);
    }
  }
}
