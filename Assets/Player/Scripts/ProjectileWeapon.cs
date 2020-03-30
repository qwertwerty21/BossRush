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
  [SerializeField] float m_TimeBetweenShots = 0.5f;
  [SerializeField] ParticleSystem m_MuzzleFlash;
  [SerializeField] GameObject m_HitEffect;
  [SerializeField] GameObject m_MissEffect;
  [SerializeField] string m_CrosshairName;

  private float m_ShotsLeft;
  private Animator m_Animator;
  private bool m_CanShoot = true;
  private Damage m_Damage;
  private Sprite m_CrosshairSprite;
  // Layer 8 is the Player Layer
  // Bit shift the index of the layer (8) to get a bit mask
  // This would cast rays only against colliders in layer 8.
  // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
  private int m_LayerMask = ~(1 << 8);

  private void SetCrosshair()
  {
    m_CrosshairSprite = Resources.Load<Sprite>(m_CrosshairName) as Sprite;
    GameObject CrosshairGameObject = GameObject.FindGameObjectWithTag("Crosshair");
    Image CrosshairImage = CrosshairGameObject.GetComponent<Image>();
    // CrosshairImage.sprite = m_CrosshairSprite;
    // CrosshairImage.color = Color.red;
  }

  IEnumerator Shoot()
  {
    m_ShotsLeft--;
    Debug.Log("gameObject" + gameObject);
    MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
    meshRenderer.enabled = true;
    m_Animator.SetBool("isShooting", true);
    m_CanShoot = false;
    PlayMuzzleFlash();
    ProcessRaycast();
    yield return new WaitForSeconds(m_TimeBetweenShots);
    m_CanShoot = true;
    m_Animator.SetBool("isShooting", false);
    meshRenderer.enabled = false;
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
      if (hit.transform.tag == "Enemy")
      {

        Debug.Log("HIT " + hit);
        CreateHitImpact(m_HitEffect, hit);
        Target target = hit.transform.GetComponent<Target>();
        target.TakeDamage(m_Damage);
      }
      else
      {
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
    m_Animator.SetBool("isReloading", true);
    m_CanShoot = false;
    yield return new WaitForSeconds(m_ReloadTime);
    m_ShotsLeft = m_ShotsPerRound;
    m_Animator.SetBool("isReloading", false);
    m_CanShoot = true;

  }

  private void Awake()
  {
    Debug.Log("ransformparet fuck" + GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>());
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    m_Damage = GetComponent<Damage>();
    m_ShotsLeft = m_ShotsPerRound;
    SetCrosshair();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButton("Fire1") && m_CanShoot)
    {
      if (m_ShotsLeft > 0)
      {
        StartCoroutine(Shoot());
      }
      else
      {
        StartCoroutine(Reload());
      }
    }
    if (Input.GetKeyDown("r"))
    {
      StartCoroutine(Reload());
    }
  }
}
