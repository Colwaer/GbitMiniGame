using System.Collections;
using UnityEngine;

public class GameManager : Public.Sigleton<GameManager>
{
    public Save Save;
    public CCheckpoint[] Checkpoints;

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
        for(; ActiveCheckpointIndex!=0; )
        {
            yield return null;
        }
        Checkpoints[index].Spawn();
    }
}
