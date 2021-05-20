using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_StartLevel : MonoBehaviour
{
    Button m_Button;
    public int LevelIndex;
    private void Awake()
    {
        m_Button = GetComponent<Button>();
    }
    private void Start()
    {
        m_Button.onClick.AddListener(StartGame);
        m_Button.onClick.AddListener(OnClick);
    }
    private void StartGame()
    {
        CSceneManager.Instance.LoadLevel(LevelIndex);
    }
    private void OnClick()
    {
        gameObject.SetActive(false);
    }
}
