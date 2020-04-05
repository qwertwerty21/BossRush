using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
  public void BroadcastEndUppercutAttack()
  {
    BroadcastMessage("EndUppercutAttack");
  }
}
