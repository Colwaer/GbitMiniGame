using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheater : MonoBehaviour
{
    [SerializeField] private Save Save;

    private void Update()
    {
        if (CSceneManager.Instance.Index == 0 && Cheat("clear"))
        {
            Debug.LogWarning("游戏存档被重置");
            Save.ResetGame(GameManager.Instance.SavePath);
        }
        if (CSceneManager.Instance.Index == 0 && Cheat("testmode"))
        {
            Debug.LogWarning("开启调试模式");
            PlayerController.Instance.b_TestMode = true;
            Destroy(gameObject);
        }
    }
    //检测是否有一串案件被同时按下
    public bool Cheat(string s)
    {
        s.ToLower();
        for (int i = 0; i < s.Length; i++)
        {
            if (!Input.GetKey((KeyCode)s[i]))
                return false;
        }
        return true;
    }
}
