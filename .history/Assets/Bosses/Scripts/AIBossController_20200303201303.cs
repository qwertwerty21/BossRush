using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossController : MonoBehaviour
{
  [SerializeField] private GameObject m_Player;
  private Animator m_Animator;

  public GameObject GetPlayer()
  {
    return m_Player;
  }
  // set animator params in here

  void Awake()
  {
    m_Animator = GetComponent<Animator>();
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
