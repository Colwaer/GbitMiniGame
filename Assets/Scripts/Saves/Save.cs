using System.Collections;
using System;
using UnityEngine;
using System.IO;


[CreateAssetMenu]
public class Save : ScriptableObject
{
    public int SceneIndex;
    public int ActiveCheckPointIndex;
    public int Point;
    public int Level;

    public void SaveGame(string savePath)
    {
        SceneIndex = GameManager.Instance.SceneIindex;
        ActiveCheckPointIndex = GameManager.Instance.ActiveCheckpointIndex;
        Point = PlayerController.Instance.m_Player.Point;
        using (BinaryWriter writer = new BinaryWriter(File.Open(savePath, FileMode.Create)))
        {
            writer.Write(SceneIndex);
            writer.Write(ActiveCheckPointIndex);
            writer.Write(Point);
            writer.Write(Level);
        }

    }

    public void LoadGame(string savePath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(savePath, FileMode.Open)))
        {
            SceneIndex = reader.ReadInt32();
            ActiveCheckPointIndex = reader.ReadInt32();
            Point = reader.ReadInt32();
            Level = reader.ReadInt32();
        }
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
