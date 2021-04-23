using UnityEngine.UI;
using UnityEngine;

public class Button_StartGame : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CSceneManager.Instance.LoadNextLevel);
    }
}
