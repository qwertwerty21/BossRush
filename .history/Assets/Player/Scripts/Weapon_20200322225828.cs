using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

  [SerializeField] Camera m_Camera;
  [SerializeField] float m_Range = 100f;
  [SerializeField] float m_Damage = 30f;
  [SerializeField] float m_TimeBetweenShots = 0.5f;

  private bool m_CanShoot = true;

  IEnumerator Shoot()
  {
    m_CanShoot = false;
    // play anmiation
    ProcessRaycast();
    yield return new WaitForSeconds(m_TimeBetweenShots);
    m_CanShoot = true;
  }

  private void ProcessRaycast()
  {
    RaycastHit hit;
    if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_Range))
    {
      CreateHitImpact(hit);
    }
    else
    {
      return;
    }
  }

  private void CreateHitImpact(RaycastHit hit)
  {
    GameObject impact = Instantiate(m_HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    Destroy(impact, 0.1f);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0) && m_CanShoot)
    {
      StartCoroutine(GunShoot());
    }
  }
}
