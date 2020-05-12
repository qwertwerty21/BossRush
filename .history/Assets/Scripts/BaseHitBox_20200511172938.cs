using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHitBox : MonoBehaviour
{

  [SerializeField] public string m_HitBoxName;
  [SerializeField] public Collider m_Collider;

  [SerializeField] public bool m_ShouldDestroyOnCollide = false;

  [HideInInspector] public Damage[] m_Damages;
  [HideInInspector] public Dictionary<string, Damage> m_DamageHash = new Dictionary<string, Damage>();
  private List<Target> m_RegisteredHitTargets = new List<Target>();

  virtual public Vector3 GetDirection(Rigidbody otherRigidBody)
  {
    Vector3 direction = otherRigidBody.transform.position - transform.position;
    return direction.normalized;
  }

  private void OnDisable()
  {
    m_RegisteredHitTargets.Clear()
  }

  private void Awake()
  {

    m_Damages = GetComponents<Damage>();
    foreach (Damage damage in m_Damages)
    {
      m_DamageHash.Add(damage.m_Name, damage);
    }

  }

  private void OnTriggerStay(Collider other)
  {
    // if (type != HitboxType.Hitbox || IsActive == false)
    //   return;

    // var hitbox = other.GetComponent<Hitbox>();
    // if (hitbox != null)
    // {
    //   if (hitbox.type == HitboxType.Hurtbox && owner != hitbox.owner)
    //     OnDetectHit(hitbox.owner);
    // }
  }

  public void OnDetectHit(IHitboxOwner target)
  {
    if (!registeredHits.Contains(target))
    {
      registeredHits.Add(target);
      this.Notify(Message.Combat_DetectHit, target);
    }
  }


}