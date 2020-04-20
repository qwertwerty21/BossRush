using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

public class RejectBox : MonoBehaviour
{
  private void OnTriggerEnter(Collider otherCollider)
  {
    Rigidbody body = otherCollider.attachedRigidbody;

    PlayerController playerController = otherCollider.gameObject.GetComponent<PlayerController>();
    if (playerController)
    {
      Vector3 direction = body.transform.position - this.transform.position;
      Debug.Log("FUCK YOUFMOVESIMPLE" + direction);
      playerController.AddImpact(direction, 20f);
      // m_CharacterController.SimpleMove((new Vector3(100f * Time.deltaTime, 0, 100f * Time.deltaTime)));

    }

  }

}