using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Public.IPlayer player = collision.GetComponent<Public.IPlayer>();
        if (player != null && Input.GetKeyDown(KeyCode.S))
        {
            CSceneManager.Instance.LoadNextLevelImmediately();
        }
    }
}
