using Public;
using UnityEngine;
using System.Collections;

public class CCameraController : MonoBehaviour
{
    private float Speed;   //缓动时摄像机的速度

    private void Start()
    {
        Speed = 5f;
        CEventSystem.Instance.Switched += OnSwitch;
    }

    private void FixedUpdate()
    {
        //float HorizontalDitance =Mathf.Min(Speed * Time.fixedDeltaTime, 
        //CPlayerController.Instance.m_Player.transform.position.x-transform.position.x);
        float HorizontalDitance = CPlayerController.Instance.m_Player.transform.position.x - transform.position.x;
        transform.position += new Vector3(HorizontalDitance, 0, 0);
    }

    private void OnSwitch(int playerIndex)
    {
        Debug.Log(playerIndex);
    }
}
