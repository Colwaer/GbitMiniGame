using UnityEngine;
using UnityEngine.UI;

//已弃用
public class Text_Point : MonoBehaviour
{
    private Text m_Text;

    private void Awake()
    {
        m_Text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        CEventSystem.Instance.PointChanged += OnPointChanged;
    }

    private void OnDisable()
    {
        CEventSystem.Instance.PointChanged -= OnPointChanged;
    }

    private void OnPointChanged(int Point)
    {
        m_Text.text = Point.ToString();
    }
}
