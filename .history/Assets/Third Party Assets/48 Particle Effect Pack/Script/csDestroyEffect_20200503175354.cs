using UnityEngine;
using System.Collections;

public class csDestroyEffect : MonoBehaviour
{

  [SerializeField] private float m_DurationToLive = 5f;

  private float m_CurrentLiveDuration = 0f;

  void Update()
  {
    m_CurrentLiveDuration += Time.deltaTime;

    if (m_CurrentLiveDuration > m_DurationToLive)
    {
      Destroy(gameObject)
        }
  }
}
