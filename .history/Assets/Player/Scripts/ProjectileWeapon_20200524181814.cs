using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileWeapon : MonoBehaviour
{
  [SerializeField] Camera m_Camera;
  [SerializeField] float m_ShotsPerRound = 10f;
  [SerializeField] float m_ReloadTime = 2f;
  [SerializeField] float m_Range = 100f;

  [SerializeField] float m_SprayFactor = .02f;
  [SerializeField] float m_TimeBetweenShots = 0.5f;
  [SerializeField] ParticleSystem m_MuzzleFlash;
  [SerializeField] GameObject m_MissEffect;
  [SerializeField] private Damage m_Damage;
  private CustomCrosshair m_CustomCrosshair;
  private PlayerController m_PlayerController;
  private float m_ShotsLeft;
  private Animator m_Animator;
  private bool m_CanShoot = true;
  // Layer 8 is the Player Layer
  // Bit shift the index of the layer (8) to get a bit mask
  // This would cast rays only against colliders in layer 8.
  // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
  private int m_LayerMask = ~(1 << 8);

  IEnumerator Shoot()
  {
    m_ShotsLeft--;
    Debug.Log("gameObject" + gameObject);

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
    Vector3 direction = m_Camera.transform.forward;
    direction.x += Random.Range(-m_SprayFactor, m_SprayFactor);
    direction.y += Random.Range(-m_SprayFactor, m_SprayFactor);
    direction.z += Random.Range(-m_SprayFactor, m_SprayFactor);

    if (Physics.Raycast(m_Camera.transform.position, direction, out hit, m_Range, m_LayerMask))
    {
      Debug.Log("hit collider gameobject " + hit.collider.gameObject);
      if (hit.transform.tag == "Enemy")
      {
        m_CustomCrosshair.SetCrosshairColor(Color.red);
        Debug.Log("HIT " + hit);
        // CreateHitImpact(m_HitEffect, hit);
        Target target = hit.transform.GetComponent<Target>();
        target.TakeDamage(m_Damage);
      }
      else
      {
        m_CustomCrosshair.SetCrosshairColor(Color.white);
        CreateHitImpact(m_MissEffect, hit);
      }
    }
    else
    {
      return;
    }
  }

  private void CreateHitImpact(GameObject effect, RaycastHit hit)
  {
    GameObject impact = Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal));
    Destroy(impact, 1f);
  }

  IEnumerator Reload()
  {
    m_CustomCrosshair.SetCrosshairColor(Color.white);
    m_Animator.SetTrigger("isInterruptingJump");
    m_Animator.ResetTrigger("jump");
    m_Animator.SetBool("isReloading", true);
    m_Animator.SetBool("canSwitchWeapon", false);
    m_CanShoot = false;
    yield return new WaitForSeconds(m_ReloadTime);
    m_ShotsLeft = m_ShotsPerRound;
    m_Animator.SetBool("isReloading", false);
    m_CanShoot = true;
    m_Animator.SetBool("canSwitchWeapon", true);

  }



  private void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    m_CustomCrosshair = GetComponent<CustomCrosshair>();
    m_ShotsLeft = m_ShotsPerRound;

    // SetCrosshair();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButton("Fire1") && m_CanShoot)
    {
      if (m_ShotsLeft > 0)
      {
        m_Animator.SetTrigger("isInterruptingJump");
        m_Animator.ResetTrigger("jump");
        m_Animator.SetBool("isShooting", true);
        m_Animator.SetBool("canSwitchWeapon", false);
        StartCoroutine(Shoot());
        if (!m_Animator.GetBool("isGrounded"))
        {
          m_PlayerController.m_GravityMultiplier = .3f;
        }
      }
      else
      {
        m_Animator.SetBool("isShooting", false);
        m_Animator.SetBool("canSwitchWeapon", !m_PlayerController.m_IsLockedOn);

        StartCoroutine(Reload());
        m_PlayerController.m_GravityMultiplier = 1f;
      }
    }
    if (Input.GetButtonUp("Fire1"))
    {
      m_CustomCrosshair.SetCrosshairColor(Color.white);
      m_PlayerController.m_GravityMultiplier = 1f;
      m_Animator.SetBool("isShooting", false);
      m_Animator.SetBool("canSwitchWeapon", !m_PlayerController.m_IsLockedOn);

    }
    if (Input.GetKeyDown("r"))
    {
      m_PlayerController.m_GravityMultiplier = 1f;
      StartCoroutine(Reload());
    }
  }
}