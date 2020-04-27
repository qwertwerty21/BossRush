using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Damage : MonoBehaviour
{

  [SerializeField] public string m_Name;
  [SerializeField] public float m_DamageAmount = 10f;
  [SerializeField] public float m_KnockbackForce = 3f;

  private void Awake()
  {
    m_Name = "Damage" + Random.Range(0f, 1f);
  }
}
