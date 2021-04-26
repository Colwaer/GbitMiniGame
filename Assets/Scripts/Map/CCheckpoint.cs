using UnityEngine;

public class CCheckpoint : MonoBehaviour
{
    private bool b_IsActive;
    private SpriteRenderer m_SpriteRenderer;

    public bool Active //记录点被激活
    { 
        get
        {
            return b_IsActive;
        }
        set
        {
            b_IsActive = value;
            if(value)
            {
                m_SpriteRenderer.color = Color.green;   //待修改
                Debug.Log($"记录点重设为{transform.position}");
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

    private void Start()
    {
        if (gameObject.name == "Checkpoint0")
        {
            Active = true;
            Spawn();
        }
        else
            Active = false;
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
            Public.IPlayer obj = collision.GetComponent<Public.IPlayer>();
            if (obj != null)
            {
                //设置新的记录点时，先禁用（只是修改b_IsActive）所有检查点
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
