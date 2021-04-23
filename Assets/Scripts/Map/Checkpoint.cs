using UnityEngine;
using Public;

public class Checkpoint : MonoBehaviour
{
    public bool b_IsActive=false; //记录点被激活

    private void Start()
    {
        CEventSystem.Instance.PlayerDie += OnPlayerDie;
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
        CEventSystem.Instance.CheckPointChanged += OnCheckPointChanged;
    }

    //使玩家重生
    public void Spawn()
    {
        CPlayerController.Instance.m_Player.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer obj = collision.GetComponent<IPlayer>();
        if (obj != null)
        {
            CEventSystem.Instance.CheckPointChanged?.Invoke();
            b_IsActive = true;
        }
    }

    private void OnCheckPointChanged()
    {
        b_IsActive = false;
    }

    private void OnPlayerDie()
    {
        if (b_IsActive) Spawn();
    }

    private void OnSceneLoaded(int sceneIndex)
    {
        if (b_IsActive) Spawn();
    }
}
