using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejectBox : MonoBehaviour
{
  private void OnTriggerEnter(Collider otherCollider)
  {
    Rigidbody body = otherCollider.attachedRigidbody;

    PlayerController playerController = otherCollider.gameObject.GetComponent<PlayerController>();
    if (playerController)
    {
      // Vector3 direction = body.transform.position - this.gameObject.transform.position;
      Vector3 direction = new Vector3(10f, 10f, 10f);
      Debug.Log("FUCK YOUFMOVESIMPLE" + direction);
      playerController.AddImpact(direction, 5f);
      // m_CharacterController.SimpleMove((new Vector3(100f * Time.deltaTime, 0, 100f * Time.deltaTime)));

    }

  }

}