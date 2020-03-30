using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
  public float m_Health = 50f;

  public Material m_Material;

  public void TakeDamage(Damage damage)
  {
    Debug.Log("TAKing DAMAGE");
    m_Health -= damage.m_DamageAmount;

    if (m_Health <= 0f)
    {
      Die();
    }
  }

  private void Die()
  {
    Destroy(gameObject);
  }
}
