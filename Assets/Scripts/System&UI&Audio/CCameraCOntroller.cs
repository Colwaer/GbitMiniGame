using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CCameraCOntroller : MonoBehaviour
{
    CinemachineVirtualCamera cvc;
    private void Awake()
    {
        cvc = GetComponent<CinemachineVirtualCamera>();
        cvc.Follow = CPlayerController.Instance.Player.transform;
    }
}
