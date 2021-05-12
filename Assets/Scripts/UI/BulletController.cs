using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int MaxCount;
    public int _Count;
    public int Count
    {
        get
        {
            return _Count;
        }
        set
        {
            if (value > MaxCount) value = 3;
            else if (value < 0) value = 0;
            _Count = value;
            int i = 0;
            for (; i < value; i++)
            {
                Bullets[i].Show();
            }
            for (; i < 3; i++)
            {
                Bullets[i].Hide();
            }
        }
    }
    private Bullet[] Bullets;

    private void Awake()
    {
        Bullets = GetComponentsInChildren<Bullet>();
        MaxCount = PlayerController.Instance.m_Player.MaxShootCount;
    }

    private void OnEnable()
    {
        CEventSystem.Instance.ShootCountChanged += OnShootCountChanged;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.ShootCountChanged -= OnShootCountChanged;
    }

    private void OnShootCountChanged(int shootCount)
    {
        Count = shootCount;
    }
}
