using Public;
using UnityEngine;
using System.Collections;

public class CCameraController : MonoBehaviour
{
    private float Speed;   //缓动时摄像机的速度

    private void Start()
    {
        Speed = 5f;
        CEventSystem.Instance.ExchangePosition += OnSwitch;
    }

    private void FixedUpdate()
    {
        float HorizontalDitance = CPlayerController.Instance.m_Player.transform.position.x - transform.position.x;
        transform.position += new Vector3(HorizontalDitance * Time.deltaTime * Speed, 0, 0);
    }

    private void OnSwitch(int playerIndex)
    {
        Debug.Log(playerIndex);
    }
}
