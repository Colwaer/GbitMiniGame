using UnityEngine;

//地图的边界
public class Boader : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Public.IPlayer player = collision.GetComponent<Public.IPlayer>();
        if (player != null)
        {
            player.Die();
        }
    }
}
