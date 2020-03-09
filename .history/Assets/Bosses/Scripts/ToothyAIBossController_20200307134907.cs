using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyAIBossController : AIBossController
{
  [SerializeField] private GameObject m_Player;
  [SerializeField] float m_MovingTurnSpeed = 360;
  [SerializeField] float m_StationaryTurnSpeed = 180;
  [SerializeField] float m_initialHealth = 100;
  private float m_TurnAmount;
  private float m_ForwardAmount;
  private Animator m_Animator;


}
