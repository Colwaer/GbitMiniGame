using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Public
{
    public static class CTool
    {
        public static Quaternion ZeroRotation = new Quaternion();
        //用于协程中，延时
        public static IEnumerator Wait(float duration)
        {
            for (float timer = 0; timer < duration; timer += Time.deltaTime)
                yield return null;
        }
        public static Vector2 RandomVector2()
            => new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        public static Vector3 RandomVector3()
            => new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        //恰好适配transform.eulerangles.z到方向矢量的转换，其他转换需要调试出这个变化的系数和常数
        public static Vector2 Angle2Direction(float angle)
            => new Vector2(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        public static float Direction2Angle(Vector2 direction)
            => Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }
    //单例模式
    public class Singleton<T> : MonoBehaviour
        where T : Singleton<T>
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
    //玩家继承此接口
    public interface IPlayer
    {
        void Die();
    }
    //为背景成随机数
    public class Random_Background
    {
        public static bool[] Nums = new bool[40];
        public static int RandomDepth()
        {
            int num = Random.Range(10, 40);
            for (; Nums[num];)
            {
                num = Random.Range(10, 40);
            }
            Nums[num] = true;
            return num / 2;
        }
        public static float RandomAlphaRandge() => Random.Range(0.4f, 0.6f);
        public static float RandomScaleRandge() => Random.Range(0.6f, 0.8f);

        public static bool[] Blocks= new bool[25];      //交叉点是否被使用过
        public static Vector3 RandomPos()
        {
            int num = Random.Range(0, 25);
            for(; Blocks[num];)
            {
                num = Random.Range(0, 25);
            }
            Blocks[num] = true;
            Vector2 pos = new Vector2(num / 5 * 10f - 5f, num % 5 * 10f - 25f) + CTool.Angle2Direction(Random.Range(0,360))*Random.Range(0,3);
            return pos; 
        }
        public static void Reset()
        {
            for (int i = 0; i < 40; i++)
            {
                Nums[i] = false;
            }
            for (int i = 0; i < 25; i++)
            {
                Blocks[i] = false;
            }
        }
    }
}




