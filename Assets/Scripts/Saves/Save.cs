﻿using System;
using UnityEngine;
using System.IO;

[CreateAssetMenu]
public class Save : ScriptableObject
{
    public int UnlockSceneIndex = 1;
    public int SceneIndex;
    public int CheckPointIndex;
    public const int STAR_NUM = 12;
    public bool[] Stars_Destroyed = new bool[STAR_NUM];    //true表示被摧毁了  
    
    public void SaveData(string savePath)
    {
        SceneIndex = GameManager.Instance.SceneIndex;
        CheckPointIndex = GameManager.Instance.ActiveCheckpointIndex;
        using (BinaryWriter writer = new BinaryWriter(File.Open(savePath, FileMode.Create)))
        {
            writer.Write(UnlockSceneIndex);
            writer.Write(SceneIndex);
            writer.Write(CheckPointIndex);
            for (int i = 0; i < STAR_NUM; i++)
            {
                writer.Write(Stars_Destroyed[i]);
            }   
        }
        ;
    }
    
    public void LoadData(string savePath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(savePath, FileMode.Open)))
        {
            UnlockSceneIndex = reader.ReadInt32();
            SceneIndex = reader.ReadInt32();
            CheckPointIndex = reader.ReadInt32();
            for (int i = 0; i < STAR_NUM; i++)
            {
                Stars_Destroyed[i] = reader.ReadBoolean();
            }
        }
        ;
    }

    public void ResetGame(string savePath)
    {
        Array.Clear(Stars_Destroyed, 0, Stars_Destroyed.Length);
        UnlockSceneIndex = 1;
        SceneIndex = 0;
        CheckPointIndex = 0;
        SaveData(savePath);
        CSceneManager.Instance.Quit();
    }
}
