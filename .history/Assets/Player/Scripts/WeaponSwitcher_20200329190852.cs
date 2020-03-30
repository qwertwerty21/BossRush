using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

  private int m_SelectedWeaponIndex = 0;

  private void SelectWeapon()
  {
    int i = 0;
    foreach (Transform weapon in transform)
    {
      weapon.gameObject.SetActive(i == m_SelectedWeaponIndex);
      i++;
    }
  }
  // Start is called before the first frame update
  void Start()
  {
    SelectWeapon();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
