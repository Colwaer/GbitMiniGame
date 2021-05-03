using System.Collections;
using UnityEngine;
using Public;
using UnityEngine.UI;
using static UnityEngine.SceneManagement.SceneManager;

public class CSceneManager : Sigleton<CSceneManager>
{
    //当前关的index
    [SerializeField] private int _Index;
    internal int Index
    {
        get
        {
            return _Index;
        }
        set
        {
            _Index = value;
            LoadScene(value);
            CEventSystem.Instance.SceneLoaded(value);
        }
    }

    [SerializeField] private Pre_LoadScene loadScenePrefab;

    public void LoadNextLevelImmediately()
    {
        Index++;
    }
    public void LoadNextLevel()
    {
        StartCoroutine(ILoadLevel(Index + 1));
    }
    IEnumerator ILoadLevel(int index)
    {
        loadScenePrefab.gameObject.SetActive(true);
        AsyncOperation asyncLoad = LoadSceneAsync(index);
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
        Index = index;
    }
    public void Exit()
    {
        Index = 0;
    }
}
