﻿
using UnityEngine;

public class TimeManager : MonoBehaviour
{

  public float m_DefaultSlowdownFactor = 0.05f;
  public float m_SlowdownLength = 2f;

  void Update()
  {
    Time.timeScale += (1f / m_SlowdownLength) * Time.unscaledDeltaTime;
    Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
  }

  public void DoSlowmotion(float slowdownFactor)
  {
    float slowdownTime = slowdownFactor || m_DefaultSlowdownFactor
    Time.timeScale = slowdownTime;
    Time.fixedDeltaTime = Time.timeScale * .02f;
  }

}