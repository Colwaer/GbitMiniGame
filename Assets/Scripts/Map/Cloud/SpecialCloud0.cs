using UnityEngine;
using UnityEngine.Tilemaps;

public class SpecialCloud0 : Cloud
{
    public WindArea WindArea;
    protected override bool Active
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
                WindArea.Count--;
                StopAllCoroutines();
                StartCoroutine(ChangeColor(Color.white, t_ChangeColor));
            }
            else
            {
                WindArea.Count++;
                StopAllCoroutines();
                StartCoroutine(ChangeColor(TargetColor, t_ChangeColor));
            }
        }
    }
    protected override void OnEnable()
    {
        CEventSystem.Instance.PlayerDie += ResetCloudImmediately;
        CEventSystem.Instance.TouchGround += ResetCloud;
    }

    protected override void OnDisable()
    {
        CEventSystem.Instance.PlayerDie -= ResetCloudImmediately;
        CEventSystem.Instance.TouchGround -= ResetCloud;
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (!Active)
            return;
        if (other.collider.CompareTag("Player"))
        {
            //不能先判断有没有落地
            if (PlayerController.Instance.m_Player.m_Velocity_LastFrame.magnitude > CollisionSpeed)
            {
                CEventSystem.Instance.CollideCloud?.Invoke();
                Active = false;
                
            }
            else if (PlayerController.Instance.m_Player.OnGround)
            {
                CEventSystem.Instance.TouchGround?.Invoke();
            }   
        }
    }
}
