using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Save : ScriptableObject
{
    public PlayerSave playerSave;

    
    


    public void LoadMethod(CPlayer player)
    {
        playerSave.Load(player);
    }
    public void SaveMethod(CPlayer player)
    {
        playerSave.Save(player);
    }
}
