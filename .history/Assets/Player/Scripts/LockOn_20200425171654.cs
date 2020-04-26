using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LockOn : MonoBehaviour
{

  [SerializeField] float m_Range = 100f;
  private PlayerController m_PlayerController;

  private Animator m_Animator;
  private Camera m_Camera;

  // Layer 8 is the Player Layer
  // Bit shift the index of the layer (8) to get a bit mask
  // This would cast rays only against colliders in layer 8.
  // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
  private int m_LayerMask = ~(1 << 8);

  private void ProcessRaycast()
  {
    RaycastHit hit;

    if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_Range, m_LayerMask))
    {
      Debug.Log("hit collider gameobject " + hit.collider.gameObject);
      if (hit.transform.tag == "Enemy")
      {
        m_PlayerController.m_LockOnTarget = hit.transform;
      }
    }
    else
    {
      return;
    }
  }
  private void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    m_Camera = Camera.main;
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q))
    {
      ProcessRaycast();
      m_PlayerController.m_IsLockedOn = !m_PlayerController.m_IsLockedOn;

      m_Animator.SetBool("canSwitchWeapon", !m_PlayerController.m_IsLockedOn);
    }

    // if (Input.GetKeyUp(KeyCode.Q))
    // {

    // }
  }
}