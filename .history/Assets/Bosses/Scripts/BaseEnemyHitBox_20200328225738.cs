using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyHitBox : MonoBehaviour
{

  [SerializeField] public string m_HitBoxName;
  [SerializeField] public Damage m_Damage;
  [SerializeField] public Collider m_Collider;

  virtual public Vector3 GetDirection(Rigidbody otherRigidBody)
  {
    Vector3 direction = otherRigidBody.transform.position - transform.position;
    return direction;
  }

  private void Awake()
  {
    m_Damage = GetComponent<Damage>();
  }


}