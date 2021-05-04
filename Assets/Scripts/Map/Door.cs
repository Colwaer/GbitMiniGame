using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CSceneManager.Instance.LoadNextLevelImmediately();
        }
    }
}
