using System.Collections;
using UnityEngine;
using Public;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CSceneManager : CSigleton<CSceneManager>
{
    public int m_Index = 0;  //当前关的index

    private void OnEnable()
    {
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
        m_Index = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnDisable()
    {
        CEventSystem.Instance.SceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        CEventSystem.Instance.SceneLoaded?.Invoke(m_Index);
    }

    private void OnSceneLoaded(int index)
    {
        m_Index = index;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(m_Index + 1);
        CEventSystem.Instance.SceneLoaded?.Invoke(m_Index + 1);
    }

    /*
    IEnumerator LoadLevel()
    {
        SceneManager.LoadScene(m_Index + 1, LoadSceneMode.Additive);
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(m_Index));
        if (m_Index == 0)
            mainSceneCamera.gameObject.SetActive(false);
        CEventSystem.Instance.SceneLoaded?.Invoke(m_Index + 1);
    }
    */

    public void Exit()
    {
        SceneManager.LoadScene(0);
        CEventSystem.Instance.SceneLoaded?.Invoke(0);
    }
}
