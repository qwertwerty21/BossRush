using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LockOn : MonoBehaviour
{
  [SerializeField] float m_Radius = 10f;
  [SerializeField] float m_Range = 10f;
  private PlayerController m_PlayerController;

  private Animator m_Animator;
  private Camera m_Camera;

  // Layer 8 is the Player Layer
  // Bit shift the index of the layer (8) to get a bit mask
  // This would cast rays only against colliders in layer 8.
  // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
  private int m_LayerMask = ~(1 << 8);

  private void ProcessSpherecast()
  {
    RaycastHit hit;
    Debug.DrawRay(m_Camera.transform.position, m_Camera.transform.forward, Color.green, 10, false);

    if (Physics.SphereCast(m_Camera.transform.position, m_Radius, m_Camera.transform.forward, out hit, m_Range, m_LayerMask))
    {
      Debug.Log("SPHERECAST " + hit.collider.gameObject);
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
      ProcessSpherecast();
      m_PlayerController.m_IsLockedOn = !m_PlayerController.m_IsLockedOn;

      m_Animator.SetBool("canSwitchWeapon", !m_PlayerController.m_IsLockedOn);
    }

    if (m_PlayerController.m_IsLockedOn && m_PlayerController.m_LockOnTarget)
    {

      float distance = Vector3.Distance(m_PlayerController.transform.position, m_PlayerController.m_LockOnTarget.transform.position);
      if (distance > m_Range)
      {
        m_PlayerController.m_IsLockedOn = false;

        m_Animator.SetBool("canSwitchWeapon", true);
      }
    }


    // if (Input.GetKeyUp(KeyCode.Q))
    // {

    // }
  }
}