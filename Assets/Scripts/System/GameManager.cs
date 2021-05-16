using System.Net;
using System.Collections;
using UnityEngine;
using System.IO;

//通过这个类和存档交互
public class GameManager : Public.Sigleton<GameManager>
{
    public string SavePath;
    [SerializeField]
    private Save Save;
    public CCheckpoint[] Checkpoints;
    public int ActiveCheckpointIndex
    {
        get
        {
            for (int i = 0; i < Checkpoints.Length; ++i)
            {
                if (Checkpoints[i].Active)
                    return i;
            }
            return -1;
        }
    }
    public int GetCheckPointIndex(CCheckpoint c)
    {
        for (int i = 0; i < Checkpoints.Length; ++i)
        {
            if (c == Checkpoints[i])
                return i;
        }
        return -1;
    }
    //只能在继续游戏时使用

    [Header("存档数据")]
    public int SceneIndex;
    public int CheckPointIndex;
    public int Point;
    public bool[] Stars_Destroyed;    //不能在这里初始化，必须先复制Save中的数据
    public void SetStar(int index, bool destroyed)
    {
        if (index < 0 || index >= Save.Stars_Destroyed.Length)
            return;
        Stars_Destroyed[index] = destroyed;
    }

    protected override void Awake() 
    {
        base.Awake();
        SavePath = Application.dataPath + "/autosave";
    }

    //存档时,GameManager从游戏进程获取数据，Save再从GameManager获取数据，再将其写入存档文件
    public void SaveGame()
    {
        SceneIndex = CSceneManager.Instance.Index;
        CheckPointIndex = ActiveCheckpointIndex;
        Point = PlayerController.Instance.m_Player.Point;
        //Stars_Destroyed在不是在这里修改的
        Save.SaveData(SavePath);
    }
    //读档时,Save从存档文件获取数据，GameManager再从Save获取数据，再决定游戏进程
    public void StartGame()
    {
        Debug.Log("开始游戏");
        if (!File.Exists(SavePath))
        {
            Debug.Log("创建了新存档");
            Save.SaveData(SavePath);
        }
        if (File.Exists(SavePath))
        {
            Save.LoadData(SavePath);

            Point = Save.Point;
            Stars_Destroyed = Save.Stars_Destroyed;

            CSceneManager.Instance.LoadLevel(1);
            PlayerController.Instance.m_Player.Point = Point;
        }
    }
    
    public void ContinueGame()
    {
        Debug.Log("继续游戏");

        if(!File.Exists(SavePath))
        {
            Debug.Log("创建了新存档");
            Save.SaveData(SavePath);
        }
        if (File.Exists(SavePath))
        {
            Save.LoadData(SavePath);

            SceneIndex = Save.SceneIndex;
            CheckPointIndex = Save.CheckPointIndex;
            Point = Save.Point;
            Stars_Destroyed = Save.Stars_Destroyed;

            if (SceneIndex == 0) SceneIndex = 1;
            CSceneManager.Instance.LoadLevel(SceneIndex);
            StartSpwan(CheckPointIndex);
            PlayerController.Instance.m_Player.Point = Point;
        }
    }

    private void StartSpwan(int index)
    {
        StartCoroutine(StartSpawn_(index));
    }
    private IEnumerator StartSpawn_(int index)
    {
        //默认在0号记录点出生，在此之后再在指定的出生点重生
        for (; ActiveCheckpointIndex != 0;)
        {
            yield return null;
        }
        Checkpoints[index].Spawn();
    }
}
