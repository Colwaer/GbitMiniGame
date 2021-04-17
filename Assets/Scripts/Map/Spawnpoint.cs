using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    private Vector3 m_Offset = new Vector3(0, -1f, 0);    //玩家2生成时相对于玩家1的偏移量
    private void Start()
    {
        CPlayerController.Instance.Player1.transform.position = transform.position;
        CPlayerController.Instance.Player2.transform.position = transform.position + m_Offset;
    }
}
