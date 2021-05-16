using UnityEngine.UI;
using UnityEngine;

public class Button_ContinueGame : MonoBehaviour
{
    Button m_Button;

    private void Awake()
    {
        m_Button = GetComponent<Button>();
    }

    private void Start()
    {
        m_Button.onClick.AddListener(GameManager.Instance.ContinueGame);
        m_Button.onClick.AddListener(OnClick);      
    }

    private void OnClick()
    {
        gameObject.SetActive(false);
    }
}
