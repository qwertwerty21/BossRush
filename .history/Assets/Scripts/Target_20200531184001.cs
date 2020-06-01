using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
  public float m_Health = 50f;

  public Material m_DamageMaterial;

  private Material m_OriginalMaterial;
  private SkinnedMeshRenderer m_SkinnedMeshRenderer;

  virtual public void TakeDamage (Damage damage, Vector3 hitPosition) {
    m_Health -= damage.m_DamageAmount;
    StartCoroutine (HighlightMaterials ());

    if (m_Health <= 0f) {
      Die ();
    }
  }

  IEnumerator HighlightMaterials () {

    m_SkinnedMeshRenderer.material = m_DamageMaterial;
    yield return new WaitForSeconds (.5f);
    m_SkinnedMeshRenderer.material = m_OriginalMaterial;
  }

  private void Die () {
    Destroy (gameObject);
  }

  virtual protected void Awake () {
    m_SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer> ();
    m_OriginalMaterial = m_SkinnedMeshRenderer.material;
  }
}