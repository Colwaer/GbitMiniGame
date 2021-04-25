using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDeadline : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Public.IPlayer player = collision.GetComponent<Public.IPlayer>();
        if (player != null)
        {
            player.Die();
        }
        else
            Destroy(collision.gameObject);  //摧毁到达边界的物体
    }
}
