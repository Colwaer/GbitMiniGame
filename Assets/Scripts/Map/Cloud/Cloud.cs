using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cloud : MonoBehaviour
{
    protected float CollisionSpeed;   //撞击云需要达到的速度
    [SerializeField] protected Color TargetColor;
    [SerializeField] protected float t_ChangeColor = 1f;
    private Tilemap m_Tilemap;

    [SerializeField] protected bool _Active; 
    public virtual bool Active
    {
        get
        {
            return _Active;
        }
        set
        {
            _Active = value;
            if (value)
            {
                StopAllCoroutines();
                StartCoroutine(ChangeColor(Color.white,t_ChangeColor));
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ChangeColor(TargetColor, t_ChangeColor));
            }
        }
    }
    
    protected virtual void Awake()
    {
        m_Tilemap = GetComponent<Tilemap>(); 
    }
    protected virtual void OnEnable()
    {
        CEventSystem.Instance.PlayerDie += ResetCloudImmediately;
        CEventSystem.Instance.CollideCloud += ResetCloud;
        CEventSystem.Instance.TouchGround += ResetCloud;
    }
    protected virtual void OnDisable()
    {
        CEventSystem.Instance.PlayerDie -= ResetCloudImmediately;
        CEventSystem.Instance.CollideCloud -= ResetCloud;
        CEventSystem.Instance.TouchGround -= ResetCloud;
    }
    private void Start()
    {
        CollisionSpeed = 16.8f;
        Active = true;
        StopAllCoroutines();
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (!Active)
            return;
        if (other.collider.CompareTag("Player"))
        {
            if(PlayerController.Instance.m_Player.OnGround)
            {    
                CEventSystem.Instance.TouchGround?.Invoke();
            }
            else if (PlayerController.Instance.m_Player.m_Velocity_LastFrame.magnitude > CollisionSpeed)
            {
                CEventSystem.Instance.CollideCloud?.Invoke();
                Active = false;
            }
        }
    }

    protected virtual void ResetCloud()
    {
        Active = true;
    }
    protected virtual void ResetCloudImmediately()
    {
        Active = true;
        StopAllCoroutines();
        m_Tilemap.color = Color.white;
    }

    protected IEnumerator ChangeColor(Color targetColor,float time)
    {
        Color originColor = m_Tilemap.color;
        for (float timer = 0; timer <= time; timer += Time.deltaTime)
        {
            m_Tilemap.color = Color.Lerp(originColor, targetColor, timer / time);
            yield return null;
        }
    }
}
