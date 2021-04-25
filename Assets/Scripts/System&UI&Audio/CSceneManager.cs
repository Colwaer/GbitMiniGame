using System.Collections;
using UnityEngine;
using Public;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class CSceneManager : CSigleton<CSceneManager>
{
    public int m_Index = 0;  //当前关的index

    public Slider loadSlider;

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
        StartCoroutine(ILoadLevel(m_Index + 1));
    }

    IEnumerator ILoadLevel(int index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        asyncLoad.allowSceneActivation = false;
        for (; !asyncLoad.isDone;)
        {
            loadSlider.value = asyncLoad.progress;
            if (asyncLoad.progress >= 0.9f)
            {
                loadSlider.value = 1.0f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
        CEventSystem.Instance.SceneLoaded?.Invoke(index);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
        CEventSystem.Instance.SceneLoaded?.Invoke(0);
    }
}
