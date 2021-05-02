using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int _ShootCount;

    public void Load(CPlayer player)
    {
        player.ShootCount = _ShootCount;
    }
    public void Save(CPlayer player)
    {
        _ShootCount = player.ShootCount;
    }
}
