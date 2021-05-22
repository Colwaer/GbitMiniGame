using Public;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private float Distance = 3f;
    [SerializeField] private Vector2 Direction;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Direction = PlayerController.Instance.Direction;
        PlayerController.Instance.FollowPlayer(transform);
        transform.position -= new Vector3(Direction.x, Direction.y, 0f).normalized * Distance;
        transform.eulerAngles = new Vector3(0f, 0f, 270f - CTool.Direction2Angle(Direction));
    }

    private void Update()
    {
        Direction = PlayerController.Instance.Direction;
        PlayerController.Instance.FollowPlayer(transform);
        transform.position -= new Vector3(Direction.x, Direction.y, 0f).normalized * Distance;
        transform.eulerAngles = new Vector3(0f, 0f, 270f - CTool.Direction2Angle(Direction));
    }
}
