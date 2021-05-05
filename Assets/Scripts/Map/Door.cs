using UnityEngine;

public class Door : MonoBehaviour
{
    bool b_IsTouchingPlayer = false;  //正在接触玩家

    private void Update()
    {
        if (b_IsTouchingPlayer && Input.GetKeyDown(KeyCode.S))
            CSceneManager.Instance.LoadNextLevel();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            b_IsTouchingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            b_IsTouchingPlayer = false;
        }
    }
}
