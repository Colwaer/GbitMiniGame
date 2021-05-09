using UnityEngine;

public class CheckpointsController : MonoBehaviour
{
    private void Start() 
    {
        GameManager.Instance.Checkpoints = GetComponentsInChildren<CCheckpoint>();
        GameManager.Instance.Checkpoints[0].Spawn();
    }
}
