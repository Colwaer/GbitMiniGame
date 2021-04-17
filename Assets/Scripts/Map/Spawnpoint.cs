﻿using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    private Vector3 m_Offset = new Vector3(0, -1f, 0);    //玩家2生成时相对于玩家1的偏移量
    private void Start()
    {
        //Debug.Log(CPlayerController.Instance);
        //Debug.Log(CPlayerController.Instance.Player1);
        CPlayerController.Instance.players[0].transform.position = transform.position;
        CPlayerController.Instance.players[1].transform.position = transform.position + m_Offset;
    }
    private void Update()
    {
        //Debug.Log(CPlayerController.Instance.Player1);
    }
}