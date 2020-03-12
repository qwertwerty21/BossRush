using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyAIBossController : AIBossController
{
  [SerializeField] float m_AttackRange = 5f;

  private static readonly Dictionary<string, int> m_State = new Dictionary<string, int> { { "Idle", 0 },
    { "Intro", 1 },
    { "Chase", 2 },
    { "Combat", 3 },
  };

  public void ToggleToothyHitboxColliders(string name, bool isEnabled)
  {
    BaseEnemyHitBox[] hitboxes = GetComponentsInChildren<BaseEnemyHitBox>();
    Debug.Log("toggle swipe hitboxes" + hitboxes);
    for (int i = 0; i < hitboxes.Length; i++)
    {
      if (name == hitboxes[i].m_HitBoxName)
      {
        hitboxes[i].m_Collider.enabled = isEnabled;
      }
    }
  }

  public void ToggleToothyParticleSystemEmission(bool isEnabled = true)
  {
    ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
    Debug.Log("toggle swipe particleSystems" + particleSystems);
    for (int i = 0; i < particleSystems.Length; i++)
    {
      // if (name == particleSystems[i].m_HitBoxName) {
      var emissionModule = particleSystems[i].emission;

      emissionModule.enabled = isEnabled;
      // }
    }
  }

  public void fuk()
  {
    ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
    Debug.Log("toggle swipe particleSystems" + particleSystems);
    for (int i = 0; i < particleSystems.Length; i++)
    {
      // if (name == particleSystems[i].m_HitBoxName) {
      var emissionModule = particleSystems[i];

      emissionModule.enabled = true;
      // }
    }
  }

  // Update is called once per frame
  private void Update()
  {
    var distanceFromPlayer = GetDistanceFromPlayer();
    Debug.Log("Distance " + m_AttackRange);

    if (distanceFromPlayer <= m_AttackRange)
    {
      Debug.Log("attack " + m_State["Combat"]);

      // attack
      m_Animator.SetInteger("state", m_State["Combat"]);
    }
    else
    {
      // Debug.Log("chase " + distanceFromPlayer);
      //chase 
      m_Animator.SetInteger("state", m_State["Chase"]);
    }
  }

}