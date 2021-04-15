using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Public
{
    //单例模式
    public class CSigleton<T> : MonoBehaviour
        where T : CSigleton<T>
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
}




