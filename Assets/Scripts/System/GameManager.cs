using UnityEngine;

public class GameManager : Public.Sigleton<GameManager>
{
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
    public int GetSentCheckPointIndex(CCheckpoint c)
    {
        for (int i = 0; i < Checkpoints.Length; ++i)
        {
            if (c == Checkpoints[i])
                return i;
        }
        return -1;
    }
}
