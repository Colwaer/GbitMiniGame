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
            => new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        public static Vector3 RandomVector3()
            => new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        public static Vector2 Angle2Direction(float angle)
            => new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        public static float Direction2Angle(Vector2 direction)
            => Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }
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
    //会受到玩家伤害的单位继承此接口
    public interface IDamagable
    {
        void GetDamage(int damage);
    }
    //会受到敌人伤害的单位继承此接口
    public interface IDamagable_Friendly
    {
        void GetDamage(int damage);
    }
}




