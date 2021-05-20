using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelPanel : MonoBehaviour
{
    private Button[] btn_Levels;
    private void Awake() 
    {
        btn_Levels = GetComponentsInChildren<Button>();    
    }
    private void OnEnable() 
    {
        int unlockIndex = GameManager.Instance.UnlockSceneIndex;
        for (int i = 0; i < btn_Levels.Length; i++)
        {
            if (unlockIndex - 1 >= i)
            {
                //Debug.Log(i + " true");
                btn_Levels[i].interactable = true;
            }             
            else
                btn_Levels[i].interactable = false;
        }
    }
}
