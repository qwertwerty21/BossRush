using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHitBox : MonoBehaviour
{

  [SerializeField] public string m_HitBoxName;
  [SerializeField] public Collider m_Collider;

  [HideInInspector] public Damage[] m_Damages;
  [HideInInspector] public Dictionary<string, Damage> m_DamageHash = new Dictionary<string, Damage>();

  virtual public Vector3 GetDirection(Rigidbody otherRigidBody)
  {
    Vector3 direction = otherRigidBody.transform.position - transform.position;
    return direction;
  }

  private void Awake()
  {

    m_Damages = GetComponents<Damage>();
    foreach (Damage damage in m_Damages)
    {
      m_DamageHash.Add(damage.m_Name, damage);
    }

  }


}