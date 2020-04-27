using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHitBox : MonoBehaviour
{

  [SerializeField] public string m_HitBoxName;

  [SerializeField] public Damage m_Damage;
  [SerializeField] public Dictionary<string, Damage> m_DamageHash;
  [SerializeField] public Collider m_Collider;

  virtual public Vector3 GetDirection(Rigidbody otherRigidBody)
  {
    Vector3 direction = otherRigidBody.transform.position - transform.position;
    return direction;
  }

  private void Awake()
  {
    // Debug.Log("HITBOXNAME" + m_HitBoxName);
    // Debug.Log("DAMAGE" + m_Damage.m_DamageAmount);
    if (m_Damage == null)
    {
      m_Damage = GetComponent<Damage>();
    }
  }


}