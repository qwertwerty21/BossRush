using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejectBox : MonoBehaviour
{
  [SerializeField] float m_Force = 5f;
  private Vector3 m_Direction = new Vector3(10f, 10f, 10f);
  private void OnTriggerEnter(Collider otherCollider)
  {
    Rigidbody body = otherCollider.attachedRigidbody;

    PlayerController playerController = otherCollider.gameObject.GetComponent<PlayerController>();
    if (playerController)
    {
      // Vector3 direction = body.transform.position - this.gameObject.transform.position;
      playerController.AddImpact(m_Direction, 5f);
      // m_CharacterController.SimpleMove((new Vector3(100f * Time.deltaTime, 0, 100f * Time.deltaTime)));

    }

  }

}