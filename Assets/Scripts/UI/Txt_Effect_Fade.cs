using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Effect_Fade : MonoBehaviour
{
    Text text;
    Color color;
    float m_FadeSpeed = 0.5f;
    bool show = true;

    private void Awake() 
    {
        text = GetComponent<Text>();   
        color = text.color; 
    }
    
    private void Update() 
    {
        color = text.color;
        if (show)
        {
            text.color = new Color(color.r, color.g, color.b, Mathf.Max(0f, color.a - Time.deltaTime * m_FadeSpeed));
            if (text.color.a <= 0.03f)
                show = false;
        }
        else
        {
            text.color = new Color(color.r, color.g, color.b, Mathf.Min(1f, color.a + Time.deltaTime * m_FadeSpeed));
            if (text.color.a >= 0.97f)
                show = true;
        }
    }
}
