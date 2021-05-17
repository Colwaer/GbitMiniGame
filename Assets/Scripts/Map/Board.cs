using UnityEngine.UI;
using UnityEngine;

//提示牌
public class Board : MonoBehaviour
{
    private Text m_Text;
    private GameObject Textbox;

    private void Awake()
    {
        m_Text = GetComponentInChildren<Text>();
        if (m_Text == null)
            Destroy(this);
        Textbox = m_Text.gameObject;
        Textbox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Textbox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Textbox.SetActive(false);
        }
    }
}
