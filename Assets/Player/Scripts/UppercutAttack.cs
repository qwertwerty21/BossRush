using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UppercutAttack : MonoBehaviour
{
  private Damage m_Damage;
  private Animator m_Animator;

  // Start is called before the first frame update
  private void Awake()
  {
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Fire2"))
    {
      m_Animator.SetBool("isChargingUppercutAttack", true);
    }
    if (Input.GetButtonUp("Fire2"))
    {
      m_Animator.SetBool("isChargingUppercutAttack", false);
      m_Animator.SetTrigger("uppercutAttack");

    }
  }
}
