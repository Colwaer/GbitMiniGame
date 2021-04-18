using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    private void Start()
    {
        CPlayerController.Instance.m_Player.transform.position = transform.position;
    }
}
