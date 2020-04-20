using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejectBox : MonoBehaviour
{
  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    Rigidbody body = hit.collider.attachedRigidbody;

    AIBossController bossController = hit.gameObject.GetComponent<AIBossController>();
    if (bossController)
    {
      Debug.Log("FUCK YOUFMOVESIMPLE");
      Vector3 direction = body.transform.position - transform.position;
      // AddImpact(direction, 10f);
      // m_CharacterController.SimpleMove((new Vector3(100f * Time.deltaTime, 0, 100f * Time.deltaTime)));

    }

  }

}