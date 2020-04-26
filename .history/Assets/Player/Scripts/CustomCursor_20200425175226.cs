using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomCursor : MonoBehaviour
{
  [SerializeField] string m_CrosshairName;

  private Sprite m_CrosshairSprite;
  private Image m_CrosshairImage;

  private void SetCrosshair()
  {
    m_CrosshairSprite = Resources.Load<Sprite>(m_CrosshairName) as Sprite;
    GameObject CrosshairGameObject = GameObject.FindGameObjectWithTag("Crosshair");
    m_CrosshairImage = CrosshairGameObject.GetComponent<Image>();
  }
}
