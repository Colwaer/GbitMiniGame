using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSingletonActivator : MonoBehaviour
{
    [SerializeField] List<GameObject> list= new List<GameObject>();

    private void Awake()
    {
        foreach (GameObject obj in list)
        {
            obj.SetActive(true);
        }
        Destroy(gameObject);
    }
}
