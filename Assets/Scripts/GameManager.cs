using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Public;

public class GameManager : CSigleton<GameManager>
{

    public CCheckpoint[] checkpoints;
    public int CurrentCheckPoint
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
