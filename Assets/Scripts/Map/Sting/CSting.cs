using UnityEngine;

public class CSting : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Public.IPlayer player = collision.GetComponent<Public.IPlayer>();
        if(player!=null)
        {
            player.Die();
        }
    }
}
