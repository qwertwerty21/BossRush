using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
  [SerializeField] private Transform m_TargetTransform;
  private void Awake()
  {

  }

  // Update is called once per frame
  void Update()
  {
    Vector3 position = new Vector3(m_TargetTransform.position.x, m_TargetTransform.position.y, m_TargetTransform.position.z);
    gameObject.LookAt(position);
  }
}
