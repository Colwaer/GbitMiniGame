using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CCloud : MonoBehaviour
{
    public float CollisionSpeed = 15f;   //撞击云需要达到的速度
    [SerializeField] protected Color TargetColor;
    [SerializeField] protected float t_ChangeColor = 1f;
    private Tilemap tilemap;

    [SerializeField] protected bool _Active; 
    protected virtual bool Active
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
        tilemap = GetComponent<Tilemap>(); 
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

    protected void ResetCloud()
    {
        Active = true;
    }
    protected void ResetCloudImmediately()
    {
        Active = true;
        StopAllCoroutines();
        tilemap.color = Color.white;
    }

    protected IEnumerator ChangeColor(Color targetColor,float time)
    {
        Color originColor = tilemap.color;
        for (float timer = 0; timer <= time; timer += Time.deltaTime)
        {
            tilemap.color = Color.Lerp(originColor, targetColor, timer / time);
            yield return null;
        }
    }
}
