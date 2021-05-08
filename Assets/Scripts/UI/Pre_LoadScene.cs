using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pre_LoadScene : MonoBehaviour
{
    public Slider m_Slider;
    public GameObject m_TextBox;

    private void OnEnable() 
    {
        m_TextBox.SetActive(false);    
    }
    
}
