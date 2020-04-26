using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomCursor : MonoBehaviour
{
  [SerializeField] string m_CrosshairName;

  private Sprite m_CrosshairSprite;
  private Image m_CrosshairImage;

  private void EnableCrosshair()
  {
    m_CrosshairSprite = Resources.Load<Sprite>(m_CrosshairName) as Sprite;
    GameObject CrosshairGameObject = GameObject.FindGameObjectWithTag("Crosshair");
    m_CrosshairImage = CrosshairGameObject.GetComponent<Image>();
  }

  private void SetCrosshairColor(Color color)
  {
    if (m_CrosshairImage != null)
    {
      m_CrosshairImage.color = color;
    }
  }
}
