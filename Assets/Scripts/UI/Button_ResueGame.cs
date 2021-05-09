using UnityEngine.UI;
using UnityEngine;

public class Button_ResueGame : MonoBehaviour
{
    Button m_Button;

    private void Awake()
    {
        m_Button = GetComponent<Button>();
    }

    private void Start()
    {
        m_Button.onClick.AddListener(GameManager.Instance.Save.LoadGame);
        m_Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        gameObject.SetActive(false);
    }
}
