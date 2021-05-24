using System.Collections;
using UnityEngine;

public class ShootEffect : EffectOnPlayer
{
    [SerializeField] private GameObject EffectCloud;
    [SerializeField] private GameObject[] EffectCloudPool;
    private float Interval = 0.05f; //产生效果云的时间间隔
    private int Num = 3;            //产生效果云的数量    

    private void Awake()
    {
        EffectCloudPool = new GameObject[Num];
        for (int i = 0; i < Num; i++)
        {
            EffectCloudPool[i] = Instantiate(EffectCloud,transform);
            EffectCloudPool[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        CEventSystem.Instance.PlayerShoot += PlayEffect;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.PlayerShoot -= PlayEffect;
    }

    protected override void Effect()
    {
        StartCoroutine(ActivateEffectCloud());
    }

    private IEnumerator ActivateEffectCloud()
    {
        for (int i = 0; i < Num; i++)
        {
            EffectCloudPool[i].SetActive(false);
            EffectCloudPool[i].SetActive(true);
            yield return Public.CTool.Wait(Interval);
        }
    }
}
