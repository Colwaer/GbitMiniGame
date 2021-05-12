using UnityEngine;
using UnityEngine.UI;

//已弃用
public class Text_ShootCount : MonoBehaviour
{
    private Text m_Text;

    private void Awake()
    {
        m_Text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        CEventSystem.Instance.ShootCountChanged += OnShootCountChanged;
    }

    private void OnDisable()
    {
        CEventSystem.Instance.ShootCountChanged -= OnShootCountChanged;
    }

    private void OnShootCountChanged(int shootCount)
    {
        m_Text.text = shootCount.ToString();
    }
}
