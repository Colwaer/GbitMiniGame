using UnityEngine;

[CreateAssetMenu]
public class PlayerSave : ScriptableObject
{
    public Vector3 position;
    public Vector3 velocity;
    public int shootCount;

    public void Load(CPlayer player)
    {
        player.transform.position = position;
        player.m_RigidBody.velocity = velocity;
        player.ShootCount = shootCount;
    }
    public void Save(CPlayer player)
    {
        position = player.transform.position;
        velocity = player.m_RigidBody.velocity;
        shootCount = player.ShootCount;
    }
}
