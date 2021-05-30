using UnityEngine.UI;
using UnityEngine;

//提示牌
public class Board : MonoBehaviour
{
    private GameObject Canvas;

    private void Awake()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas == null)
            Destroy(this);
        Canvas = canvas.gameObject;
        Canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Canvas.SetActive(false);
        }
    }
}
