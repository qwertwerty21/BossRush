using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rejecter : MonoBehaviour
{
  [SerializeField] float m_Force = 3f;
  private void OnTriggerEnter(Collider otherCollider)
  {
    PlayerController playerController = otherCollider.gameObject.GetComponent<PlayerController>();

    if (playerController)
    {
      Vector3 direction = otherCollider.transform.position - transform.position;
      playerController.AddImpact(direction, m_Force);
    }

  }

}