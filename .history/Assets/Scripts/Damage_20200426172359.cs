using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

  [SerializeField] public string m_Name = "Damage";
  [SerializeField] public float m_DamageAmount = 10f;
  [SerializeField] public float m_KnockbackForce = 3f;
}
