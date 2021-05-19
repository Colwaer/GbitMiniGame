using System.Collections;
using UnityEngine;
using Public;
using UnityEngine.UI;
using static UnityEngine.SceneManagement.SceneManager;

public class CSceneManager : Sigleton<CSceneManager>
{
    private const int MAXINDEX = 4;
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
            //退出时不保存；最后一关结束回到开始界面时时依然会保存，因为value==MAXINDEX+1，稍后才会被置为0
            if (value != 0)
            {
                CEventSystem.Instance.ScenePassed?.Invoke();
                GameManager.Instance.SaveGame();
            }  
            if (value > MAXINDEX || value < 0)
                value = 0;
            if (value == _Index)
                return;
            StartCoroutine(ILoadLevel(value));
            _Index = value;
            CEventSystem.Instance.SceneLoaded?.Invoke(value);
        }
    }
    [SerializeField] private Pre_LoadScene loadScenePrefab;

    //禁止用不属于本类的方法加载场景
    public void LoadLevel(int index)
    {
        Index = index;
    }
    public void LoadNextLevel()
    {
        Index++;
    }
    //直接启用这个协程是不安全的
    private IEnumerator ILoadLevel(int index)
    {
        AsyncOperation Async_LoadScene = LoadSceneAsync(index);
        switch (index)
        {
            //在这里加载两关之间的过场场景
            case 1:
                loadScenePrefab.gameObject.SetActive(true);
                Async_LoadScene.allowSceneActivation = false;
                for (; !Async_LoadScene.isDone;)
                {
                    loadScenePrefab.m_Slider.value = Async_LoadScene.progress;
                    if (Async_LoadScene.progress >= 0.9f)
                    {
                        loadScenePrefab.m_TextBox.gameObject.SetActive(true);
                        loadScenePrefab.m_Slider.value = 1.0f;
                        if (Input.anyKeyDown)
                        {
                            loadScenePrefab.gameObject.SetActive(false);
                            Async_LoadScene.allowSceneActivation = true;
                        }
                    }
                    yield return null;
                }
                break;
            default:
                Async_LoadScene.allowSceneActivation = true;
                break;
        }
    }
    public void Exit()
    {
        Index = 0;
    }
    //必须从这里退出游戏
    public void Quit()
    {
        Application.Quit();
    }
}
