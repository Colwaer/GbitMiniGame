using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    private Image m_Image;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    public void Show()
    {
        m_Image.enabled = true;
    }

    public void Hide()
    {
        m_Image.enabled = false;
    }
}
