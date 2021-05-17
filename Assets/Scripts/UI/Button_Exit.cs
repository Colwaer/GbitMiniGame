using UnityEngine.UI;
using UnityEngine;

public class Button_Exit : MonoBehaviour
{
    Button m_Button;

    private void Awake()
    {
        m_Button = GetComponent<Button>();
    }

    private void Start()
    {
        if(CSceneManager.Instance.Index == 0)
        {
            m_Button.onClick.AddListener(CSceneManager.Instance.Quit);
        }
        else
        {
            m_Button.onClick.AddListener(CSceneManager.Instance.Exit);
        }
    }
}
