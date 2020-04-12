using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

  [SerializeField] private int m_SelectedWeaponIndex = 0;

  private Animator m_Animator;

  private void SelectWeapon()
  {
    int i = 0;
    foreach (Transform weapon in transform)
    {
      weapon.gameObject.SetActive(i == m_SelectedWeaponIndex);
      i++;
    }
  }
  void Awake()
  {
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
  }
  // Start is called before the first frame update
  void Start()
  {
    SelectWeapon();
  }

  // Update is called once per frame
  void Update()
  {
    int previousSelectedWeaponIndex = m_SelectedWeaponIndex;

    if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.Tab))
    {
      if (m_SelectedWeaponIndex >= transform.childCount - 1)
      {
        m_SelectedWeaponIndex = 0;
      }
      else
      {
        m_SelectedWeaponIndex++;
      }
    }
    if (Input.GetAxis("Mouse ScrollWheel") < 0f)
    {
      if (m_SelectedWeaponIndex <= 0)
      {
        m_SelectedWeaponIndex = transform.childCount - 1;
      }
      else
      {
        m_SelectedWeaponIndex--;
      }
    }

    Boolean canSwitchWeapons = !m_Animator.GetBool("isReloading");

    if (m_SelectedWeaponIndex != previousSelectedWeaponIndex && canSwitchWeapons)
    {
      SelectWeapon();
    }
  }
}
