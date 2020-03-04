using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossController : MonoBehaviour
{

  [SerializeField] private Animator m_Animator;
  [SerializeField] private GameObject m_Player;

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
