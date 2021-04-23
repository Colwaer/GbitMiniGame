using System.Collections;
using UnityEngine;
using Public;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CSceneManager : CSigleton<CSceneManager>
{
    public int m_Index = 0;  //当前关的index
    Camera mainSceneCamera;
    private void OnEnable()
    {
        mainSceneCamera = Camera.main;
        Debug.Log(1);
        //CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
        m_Index = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnDisable()
    {
        //CEventSystem.Instance.SceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
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

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        
        SceneManager.LoadScene(m_Index + 1, LoadSceneMode.Additive);
        //Debug.Log("load");
        yield return null;
        //Debug.Log("before active");
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(m_Index));
        //Debug.Log("after active before action");
        if (m_Index == 0)
            mainSceneCamera.gameObject.SetActive(false);
        CEventSystem.Instance.SceneLoaded?.Invoke(m_Index + 1);
        //Debug.Log("after action");
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
        CEventSystem.Instance.SceneLoaded?.Invoke(0);
    }
}
