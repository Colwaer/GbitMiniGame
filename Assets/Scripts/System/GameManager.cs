using System.Net;
using System.Collections;
using UnityEngine;
using System.IO;
public class GameManager : Public.Sigleton<GameManager>
{
    [SerializeField]
    private Save Save;
    public CCheckpoint[] Checkpoints;
    [HideInInspector]
    public string savePath;
    protected override void Awake() 
    {
        base.Awake();
        savePath = Application.dataPath + "save";
    }
    public int SceneIindex
    {
        get => CSceneManager.Instance.Index;
    }
    public int ActiveCheckpointIndex
    {
        get
        {
            for (int i = 0; i < Checkpoints.Length; ++i)
            {
                if (Checkpoints[i].Active)
                    return i;
            }
            return -1;
        }
    }

    public int GetCheckPointIndex(CCheckpoint c)
    {
        for (int i = 0; i < Checkpoints.Length; ++i)
        {
            if (c == Checkpoints[i])
                return i;
        }
        return -1;
    }
    //只能在继续游戏时使用
    public void StartSpwan(int index)
    {
        StartCoroutine(StartSpawn_(index));
    }
    private IEnumerator StartSpawn_(int index)
    {
        //默认在0号记录点出生，在此之后再在指定的出生点重生
        for(; ActiveCheckpointIndex !=0; )
        {
            yield return null;
        }
        Checkpoints[index].Spawn();
    }
    public void SaveGame()
    {
        Save.SaveGame(savePath);
    }
    public void LoadGame()
    {
        Debug.Log("LoadGame");
        if (File.Exists(savePath))
            Save.LoadGame(savePath);
        else
        {
            Debug.LogError("save file doesn't exist");
            CSceneManager.Instance.LoadNextLevel();
        }
            
    }
}
