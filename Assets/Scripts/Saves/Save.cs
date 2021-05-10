using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu]
public class Save : ScriptableObject
{
    public int SceneIndex;
    public int ActiveCheckPointIndex;
    public int Point;

    public void SaveGame()
    {
        SceneIndex = GameManager.Instance.SceneIindex;
        ActiveCheckPointIndex = GameManager.Instance.ActiveCheckpointIndex;
        Point = PlayerController.Instance.m_Player.Point;
    }

    public void LoadGame()
    {
        CSceneManager.Instance.LoadLevel(SceneIndex);
        GameManager.Instance.StartSpwan(ActiveCheckPointIndex);
        PlayerController.Instance.m_Player.Point = Point;
    }

    public void ResetGame()
    {
        SceneIndex = 0;
        ActiveCheckPointIndex = 0;
        Point = 0;
    }
}
