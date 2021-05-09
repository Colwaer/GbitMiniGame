using Public;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private float Distance = 3f ;
    [SerializeField] private Transform PlayerPos;

    private void OnEnable()
    {
        if(PlayerPos!= null)
        {
            transform.position = PlayerPos.position - new Vector3(PlayerController.Instance.Direction.x, PlayerController.Instance.Direction.y, 0f).normalized * Distance;
            transform.rotation = Quaternion.Euler(0f, 0f, 270f - CTool.Direction2Angle(PlayerController.Instance.Direction));
        }
    }

    private void Start()
    {
        PlayerPos = PlayerController.Instance.Player.transform;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = PlayerPos.position - new Vector3(PlayerController.Instance.Direction.x, PlayerController.Instance.Direction.y, 0f).normalized * Distance;
        transform.rotation = Quaternion.Euler(0f, 0f, 270f - CTool.Direction2Angle(PlayerController.Instance.Direction));
    }
}
