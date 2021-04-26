using UnityEngine;

public class GameManager : Public.Sigleton<GameManager>
{
    public CCheckpoint[] checkpoints;

    public int ActiveCheckpointIndex
    {
        get
        {
            for (int i = 0; i < checkpoints.Length; ++i)
            {
                if (checkpoints[i].Active)
                    return i;
            }
            return -1;
        }
    }
}
