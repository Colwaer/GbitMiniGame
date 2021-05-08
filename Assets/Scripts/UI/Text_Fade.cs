using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Fade : MonoBehaviour
{
    private Text m_Text;
    private Color m_Color;
    private float FadeSpeed = 0.5f;
    bool b_IsActive = true;

    private void Awake() 
    {
        m_Text = GetComponent<Text>();   
        m_Color = m_Text.color; 
    }
    
    private void Update() 
    {
        m_Color = m_Text.color;
        if (b_IsActive)
        {
            m_Text.color = new Color(m_Color.r, m_Color.g, m_Color.b, Mathf.Max(0f, m_Color.a - Time.deltaTime * FadeSpeed));
            if (m_Text.color.a <= 0.03f)
                b_IsActive = false;
        }
        else
        {
            m_Text.color = new Color(m_Color.r, m_Color.g, m_Color.b, Mathf.Min(1f, m_Color.a + Time.deltaTime * FadeSpeed));
            if (m_Text.color.a >= 0.97f)
                b_IsActive = true;
        }
    }
}
