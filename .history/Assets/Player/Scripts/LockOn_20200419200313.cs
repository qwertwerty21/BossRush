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
}