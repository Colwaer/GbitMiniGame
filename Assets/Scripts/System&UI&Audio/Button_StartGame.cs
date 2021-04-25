using UnityEngine.UI;
using UnityEngine;

public class Button_StartGame : MonoBehaviour
{
    Button Button;
    [SerializeField]private GameObject Textbox;

    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    private void Start()
    {
        Button.onClick.AddListener(CSceneManager.Instance.LoadNextLevel);
        Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        gameObject.SetActive(false);
    }
}
