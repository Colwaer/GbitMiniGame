using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pre_LoadScene : MonoBehaviour
{
    public Slider slider;
    public GameObject text;

    private void OnEnable() 
    {
        text.SetActive(false);    
    }
    
}
