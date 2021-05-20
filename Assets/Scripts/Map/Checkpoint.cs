using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;

    private bool _Active;
    public bool Active //记录点被激活
    {
        get
        {
            return _Active;
        }
        set
        {
            _Active = value;
            if(value)
            {
                m_SpriteRenderer.color = Color.green;
                GameManager.Instance.SaveGame();
            }
            else
            {
                m_SpriteRenderer.color = Color.red;
            }    
        }
    }

    private void OnEnable()
    {
        CEventSystem.Instance.PlayerDie += OnPlayerDie;
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
        CEventSystem.Instance.CheckPointChanged += OnCheckPointChanged;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.PlayerDie -= OnPlayerDie;
        CEventSystem.Instance.SceneLoaded -= OnSceneLoaded;
        CEventSystem.Instance.CheckPointChanged -= OnCheckPointChanged;
    }

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    //使玩家重生
    public void Spawn()
    {
        PlayerController.Instance.m_Player.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!Active)
        {
            //if (GameManager.Instance.ActiveCheckpointIndex > GameManager.Instance.GetCheckPointIndex(this)) return;

            if(collision.CompareTag("Player"))
            {
                //设置新的记录点时，先禁用（只是修改Active属性）所有检查点
                CEventSystem.Instance.CheckPointChanged?.Invoke();
                Active = true;
            }
        }
    }
    
    private void OnCheckPointChanged()
    {
        Active = false;
    }
    private void OnPlayerDie()
    {
        if (Active) Spawn();
    }
    private void OnSceneLoaded(int sceneIndex)
    {
        if (Active) Spawn();
    }
}
