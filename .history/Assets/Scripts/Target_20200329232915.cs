using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
  public float m_Health = 50f;

  public Material m_DamageMaterial;

  private Material m_OriginalMaterial;
  private MeshRenderer m_SkinnedMeshRenderer;

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

    m_SkinnedMeshRenderer.material = m_DamageMaterial;
    yield return new WaitForSeconds(.5f);
    m_SkinnedMeshRenderer.material = m_OriginalMaterial;
  }

  private void Die()
  {
    Destroy(gameObject);
  }

  private void Awake()
  {
    m_SkinnedMeshRenderer = GetComponentInChildren<MeshRenderer>();
    Debug.Log("FUCK YOU MESHRENDRE" + m_SkinnedMeshRenderer);
    m_OriginalMaterial = m_SkinnedMeshRenderer.material;
  }
}
