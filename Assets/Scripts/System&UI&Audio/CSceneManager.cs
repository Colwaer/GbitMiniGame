using System.Collections;
using UnityEngine;
using Public;
using static UnityEngine.SceneManagement.SceneManager;
using System.Collections.Generic;

public class CSceneManager :CSigleton<CSceneManager>
{
    public static int m_Index = 0;  //当前关的index

    private void OnEnable()
    {
        Debug.Log(1);
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
        m_Index = GetActiveScene().buildIndex;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.SceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        Debug.Log(2);
        CEventSystem.Instance.SceneLoaded?.Invoke(m_Index);
    }

    private void OnSceneLoaded(int index)
    {
        m_Index = index;
        if (index > 0)
        {
            GameObject obj = GameObject.Find("Checkpoint0");
            Checkpoint checkpoint;
            if (obj != null)
            {
                checkpoint = obj.GetComponent<Checkpoint>();
                checkpoint.b_IsActive = true;
                checkpoint.Spawn();
            }
        }
    }

    public static void NextLevel()
    {
        LoadScene(m_Index + 1);
        CEventSystem.Instance.SceneLoaded?.Invoke(m_Index);
    }

    public static void Exit()
    {
        LoadScene(0);
        CEventSystem.Instance.SceneLoaded?.Invoke(0);
    }
}
