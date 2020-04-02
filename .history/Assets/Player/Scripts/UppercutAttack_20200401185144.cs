using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UppercutAttack : MonoBehaviour
{
  private Damage m_Damage;
  private Animator m_Animator;
  private MeshRenderer m_MeshRenderer;

  private PlayerController m_PlayerController;

  // Start is called before the first frame update
  private void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<m_PlayerController>();
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
      m_MeshRenderer.enabled = false;
      m_PlayerController.ToggleHitboxColliders("UppercutAttack", false);


    }
  }
}
