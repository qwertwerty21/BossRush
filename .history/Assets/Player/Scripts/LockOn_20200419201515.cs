using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LockOn : MonoBehaviour
{
  private PlayerController m_PlayerController;
  private void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();

  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Mouse3))
    {
      m_PlayerController.m_IsLockedOn = !m_PlayerController.m_IsLockedOn;
    }

    // if (Input.GetKeyUp(KeyCode.Q))
    // {

    // }
  }
}