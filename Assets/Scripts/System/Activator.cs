using System.Collections.Generic;
using UnityEngine;

//将需要按顺序激活的游戏物体依次拖入此列表中
public class Activator : MonoBehaviour
{
    [SerializeField] private List<GameObject> list= new List<GameObject>();

    private void Awake()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Second);
        foreach (GameObject obj in list)
        {
            obj.SetActive(true);
        }
        Destroy(gameObject);
    }
}
