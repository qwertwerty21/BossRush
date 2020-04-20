using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejectBox : MonoBehaviour
{
  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    Rigidbody body = hit.collider.attachedRigidbody;

    PlayerController playerController = hit.gameObject.GetComponent<PlayerController>();
    if (playerController)
    {
      Debug.Log("FUCK YOUFMOVESIMPLE");
      Vector3 direction = body.transform.position - transform.position;
      playerController.AddImpact(direction, 10f);
      // m_CharacterController.SimpleMove((new Vector3(100f * Time.deltaTime, 0, 100f * Time.deltaTime)));

    }

  }

}