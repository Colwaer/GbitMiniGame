using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverCheckPoints : MonoBehaviour
{
    
    private void Start() 
    {
        GameManager.Instance.checkpoints = GetComponentsInChildren<CCheckpoint>();

    }
}
