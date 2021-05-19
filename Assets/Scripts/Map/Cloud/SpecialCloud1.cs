using System.Collections;
using UnityEngine;

public class SpecialCloud1 : Cloud
{
    public WindArea WindArea;
    public Transform Target;
    public GameObject Obstacle;
    public Checkpoint spawnPoint;

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
                WindArea.Count = 0;
                StopAllCoroutines();
                StartCoroutine(ChangeColor(Color.white, t_ChangeColor));
            }
            else
            {
                WindArea.Count = 1;
                StopAllCoroutines();
                StartCoroutine(ChangeColor(TargetColor, t_ChangeColor));
                if(Target!=null)
                {
                    CCameraController CameraController = Camera.main.GetComponentInChildren<CCameraController>();
                    CameraController.StartFollow(Target, 2.5f);
                    PlayerController.Instance.PauseControl(2.5f);
                    StartCoroutine(SpawnPlayer(1.0f));
                }
                if (Obstacle != null) 
                    Destroy(Obstacle);
            }
        }
    }
    IEnumerator SpawnPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        spawnPoint.Spawn();
    }
    //不会被重置
    protected override void OnEnable()
    {
        
    }
    protected override void OnDisable()
    {
        
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
