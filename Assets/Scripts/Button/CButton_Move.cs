using UnityEngine;
using Public;

public class CButton_Move : CButton
{
    private int BLOCK_X, BLOCK_Y;   //player2接触后使player1移动的距离
    protected override void Respond()
    {
        CEventCenter.Instance.player1.transform.position += new Vector3(BLOCK_X, BLOCK_Y, 0) * CEventCenter.DISTANCE_BLOCK;
    }
}
