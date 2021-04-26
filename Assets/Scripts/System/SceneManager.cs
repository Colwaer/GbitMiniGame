using System.Collections;
using UnityEngine;
using Public;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : Sigleton<SceneManager>
{
    internal int m_Index = 0;  //当前关的index

    [SerializeField] private Pre_LoadScene loadScenePrefab;

    private void OnEnable()
    {
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
        m_Index = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
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
        loadScenePrefab.gameObject.SetActive(true);
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);
        asyncLoad.allowSceneActivation = false;
        for (; !asyncLoad.isDone;)
        {
            loadScenePrefab.slider.value = asyncLoad.progress;
            if (asyncLoad.progress >= 0.9f)
            {
                loadScenePrefab.text.gameObject.SetActive(true);
                loadScenePrefab.slider.value = 1.0f;
                if (Input.anyKeyDown)
                {
                    loadScenePrefab.gameObject.SetActive(false);
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
        CEventSystem.Instance.SceneLoaded?.Invoke(index);
    }

    public void Exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        CEventSystem.Instance.SceneLoaded?.Invoke(0);
    }
}
