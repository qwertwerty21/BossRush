using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{

  [SerializeField] Camera m_Camera;
  [SerializeField] float m_Range = 100f;
  [SerializeField] float m_Damage = 30f;
  [SerializeField] float m_TimeBetweenShots = 0.5f;
  [SerializeField] ParticleSystem m_MuzzleFlash;
  [SerializeField] GameObject m_HitEffect;
  private bool m_CanShoot = true;
  // Bit shift the index of the layer (8) to get a bit mask
  // This would cast rays only against colliders in layer 8.
  // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
  private int m_LayerMask = ~(1 << 8);



  IEnumerator Shoot()
  {
    m_CanShoot = false;
    PlayMuzzleFlash();
    ProcessRaycast();
    yield return new WaitForSeconds(m_TimeBetweenShots);
    m_CanShoot = true;
  }

  private void PlayMuzzleFlash()
  {
    m_MuzzleFlash.Play();
  }

  private void ProcessRaycast()
  {
    RaycastHit hit;


    if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_Range, m_LayerMask))
    {
      Debug.Log("hit collider gameobject " + hit.collider.gameObject);
      if (hit.collider.gameObject.CompareTag("Enemy"))
      {

        Debug.Log("HIT " + hit);
        CreateHitImpact(hit);
      }
    }
    else
    {
      return;
    }
  }

  private void CreateHitImpact(RaycastHit hit)
  {
    GameObject impact = Instantiate(m_HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    Destroy(impact, 1f);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0) && m_CanShoot)
    {
      StartCoroutine(Shoot());
    }
  }
}
