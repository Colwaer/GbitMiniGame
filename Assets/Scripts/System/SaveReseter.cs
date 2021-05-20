using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveReseter : MonoBehaviour
{
    [SerializeField] private Save Save;

    void Update()
    {
        if (CSceneManager.Instance.Index == 0 && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.R))
        {
            Save.ResetGame(GameManager.Instance.SavePath);
        }
    }
}
