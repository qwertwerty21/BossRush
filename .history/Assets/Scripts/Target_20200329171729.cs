using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
  public float m_Health = 50f;

  public Material m_DamageMaterial;

  private Material m_OriginalMaterial;

  public void TakeDamage(Damage damage)
  {
    Debug.Log("TAKing DAMAGE");
    m_Health -= damage.m_DamageAmount;
    StartCoroutine(HighlightMaterials());



    if (m_Health <= 0f)
    {
      Die();
    }
  }

  IEnumerator HighlightMaterials()
  {
    var skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    m_OriginalMaterial = skinnedMeshRenderer.material;
    skinnedMeshRenderer.material = m_DamageMaterial;
    yield return new WaitForSeconds(.5f);
    skinnedMeshRenderer.material = m_OriginalMaterial;
  }

  private void Die()
  {
    Destroy(gameObject);
  }
}
