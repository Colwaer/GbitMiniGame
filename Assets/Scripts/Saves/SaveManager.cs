using UnityEngine;
using Public;
public class SaveManager : Sigleton<SaveManager>
{
    public Save save;
    public CPlayer player;
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.O))
            Save();
        if (Input.GetKeyDown(KeyCode.L))
            Load();
    }
    public void Save()
    {
        save.SaveMethod(player);
    }
    public void Load()
    {
        save.LoadMethod(player);
    }
}
