using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Damage : MonoBehaviour
{

  [SerializeField] public string m_Name = "Damage" + Random.RandomRange(0f, 1f);
  [SerializeField] public float m_DamageAmount = 10f;
  [SerializeField] public float m_KnockbackForce = 3f;
}
