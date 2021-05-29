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
        Debug.Log(unlockIndex);
        for (int i = 0; i < btn_Levels.Length; i++)
        {
            btn_Levels[i].interactable = unlockIndex - 1 >= i;
        }
        //最后一个是退出按钮
        btn_Levels[btn_Levels.Length - 1].interactable = true;
    }
}
