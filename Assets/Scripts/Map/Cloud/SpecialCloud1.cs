using System.Collections;
using UnityEngine;

public class SpecialCloud1 : Cloud
{
    public WindArea WindArea;       //要激活(关闭)的气流
    public Transform Target;        //相机暂时移动到的目的地
    public GameObject Obstacle;     //要摧毁的障碍物
    public Checkpoint CheckPoint;   //要重生到的记录点

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
                StopAllCoroutines();
                StartCoroutine(ChangeColor(Color.white, t_ChangeColor));
            }
            else
            {
                if (WindArea != null) WindArea.Count += 1;
                StopAllCoroutines();
                StartCoroutine(ChangeColor(TargetColor, t_ChangeColor));
                if(Target!=null)
                {
                    CCameraController CameraController = Camera.main.GetComponentInChildren<CCameraController>();
                    CameraController.StartFollow(Target, 2.5f);
                    PlayerController.Instance.PauseControl(2.5f);
                }
                if (Obstacle != null) 
                    Destroy(Obstacle);
                if(CheckPoint!= null)
                {
                    StartCoroutine(SpawnPlayer(1.0f));
                }
            }
        }
    }
    IEnumerator SpawnPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        CheckPoint.Spawn();
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
            if (PlayerController.Instance.m_Player.m_Velocity_LastFrame.magnitude > CollisionSpeed)
            {
                CEventSystem.Instance.CollideCloud?.Invoke();
                Active = false;
            }
            //踩上去不会将冲刺次数重置为1
        }
    }
}
