using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deliver_Checkpoints : MonoBehaviour
{
    private void Start() 
    {
        GameManager.Instance.checkpoints = GetComponentsInChildren<CCheckpoint>();
    }
}
