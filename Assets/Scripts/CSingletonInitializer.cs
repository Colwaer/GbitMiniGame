using Public;
using UnityEngine;


public class CSingletonInitializer : CSigleton<CSingletonInitializer>
{
    protected void Start()
    {
        CEventSystem.Instance.Initialize();
        CSceneManager.Instance.Initialize();
        CPlayerController.Instance.Initialize();
        Destroy(this.gameObject);
    }
}
