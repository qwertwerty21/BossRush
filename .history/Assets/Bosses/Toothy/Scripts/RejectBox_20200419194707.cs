using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejectBox : MonoBehaviour
{
  [SerializeField] float m_Force = 5f;
  private Vector3 m_Direction = new Vector3(10f, 10f, 10f);
  private void OnTriggerEnter(Collider otherCollider)
  {
    PlayerController playerController = otherCollider.gameObject.GetComponent<PlayerController>();

    if (playerController)
    {
      Vector3 direction = otherCollider.transform.position - transform.position;
      playerController.AddImpact(m_Direction, m_Force);
    }

  }

}