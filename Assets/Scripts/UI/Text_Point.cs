using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Text_Point : MonoBehaviour
{
    private Text m_Text;

    private void Awake()
    {
        m_Text = GetComponent<Text>();
        m_Text.text = GameManager.Instance.Point.ToString() + "/" + Save.STAR_NUM.ToString();
    }
}
