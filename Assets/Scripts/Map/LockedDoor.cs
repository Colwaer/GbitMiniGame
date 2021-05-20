using UnityEngine;

//收集够钥匙后会打开的门
public class LockedDoor : MonoBehaviour
{
    [SerializeField] private int Key_Needed;    //开门需要的钥匙数

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && PlayerController.Instance.m_Player.KeyCount >= Key_Needed)
        {
            PlayerController.Instance.m_Player.KeyCount -= Key_Needed;
            Destroy(gameObject);
        }
    }
}
