using UnityEngine.UI;
using UnityEngine;

public class Button_Return : MonoBehaviour
{
    Button m_Button;

    private void Awake()
    {
        m_Button = GetComponent<Button>();
    }

    private void Start()
    {
        m_Button.onClick.AddListener(CSceneManager.Instance.Quit);
    }


}
