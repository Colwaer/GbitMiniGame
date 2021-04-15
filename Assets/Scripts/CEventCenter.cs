using System.Collections.Generic;
using UnityEngine;
using Public;

public class CEventCenter : CSigleton<CEventCenter>
{
    public GameObject player1, player2;//上面和下面的玩家
    public const float DISTANCE_BLOCK = 1f;//一格的距离
}
