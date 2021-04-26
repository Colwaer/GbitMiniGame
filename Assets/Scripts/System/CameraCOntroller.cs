using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCOntroller : MonoBehaviour
{
    CinemachineVirtualCamera cvc;
    private void Awake()
    {
        cvc = GetComponent<CinemachineVirtualCamera>();
        cvc.Follow = PlayerController.Instance.Player.transform;
    }
}
