using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimatorController : MonoBehaviour
{

  private PlayerController m_PlayerController;

  void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
