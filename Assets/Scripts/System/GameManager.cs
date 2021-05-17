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
    public int Point
    {
        get 
        {
            int ret = 0;
            foreach (bool flag in Stars_Destroyed)
            {
                if (flag) ret++;
            }
            return ret;
        }
    }
    public bool[] Stars_Destroyed;    //不能在这里初始化，必须先复制Save中的数据
    //必须通过这个方法修改Stars_Destroyed
    public void SetStar(int index, bool destroyed)
    {
        if (index < 0 || index >= Save.Stars_Destroyed.Length)
            return;
        CEventSystem.Instance.PointChanged?.Invoke(Point);
        Stars_Destroyed[index] = destroyed;
    }

    protected override void Awake() 
    {
        base.Awake();
        SavePath = Application.dataPath + "/autosave";
        LoadData();
    }

    //存档时,GameManager从游戏进程获取数据，Save再从GameManager获取数据，再将其写入存档文件
    public void SaveGame()
    {
        SceneIndex = CSceneManager.Instance.Index;
        CheckPointIndex = ActiveCheckpointIndex;
        //Stars_Destroyed在不是在这里修改的
        Save.SaveData(SavePath);
    }
    //无论如何都要在游戏开始时执行
    public void LoadData()
    {
        Debug.Log("读取存档");
        if (!File.Exists(SavePath))
        {
            Debug.Log("创建了新存档");
            Save.SaveData(SavePath);
        }
        Save.LoadData(SavePath);
        SceneIndex = Save.SceneIndex;
        CheckPointIndex = Save.CheckPointIndex;
        Stars_Destroyed = Save.Stars_Destroyed; //获取引用
    }

    private void Update()
    {
        if(CSceneManager.Instance.Index ==0 && Input.GetKey(KeyCode.C)&& Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.R))
        {
            Save.ResetGame(SavePath);
        }
    }

    public void StartGame()
    {
        CSceneManager.Instance.LoadLevel(1);
    }
    
    public void ContinueGame()
    {
        if (SceneIndex <= 0) SceneIndex = 1;
        if (CheckPointIndex < 0) CheckPointIndex = 0;
        CSceneManager.Instance.LoadLevel(SceneIndex);
        StartSpwan(CheckPointIndex);
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
