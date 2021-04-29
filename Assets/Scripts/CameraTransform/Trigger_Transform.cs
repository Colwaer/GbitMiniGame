using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Trigger_Transform : MonoBehaviour
{
    private bool b_HasTakenEffect = false;
    public Transform follow;
    CinemachineVirtualCamera cvc;
    private void Start() 
    {
        cvc = Camera.main.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (b_HasTakenEffect)
            return;
        if (other.CompareTag("Player"))
        {
            cvc.m_Lens.LensShift.Set(cvc.m_Lens.LensShift.x / 5, cvc.m_Lens.LensShift.y / 5);
            b_HasTakenEffect = true;
            cvc.Follow = follow;
            StartCoroutine(ScaleUp(17.5f, 10.5f));
        }    
    }
    IEnumerator ScaleUp(float delay, float tar)
    {
        // delay越大，镜头切换越慢
        float value = cvc.m_Lens.OrthographicSize;
        float tick = 0;
        while (tick < delay)
        {
            //Debug.Log(value);
            tick += Time.deltaTime;
            value = Mathf.Lerp(value, tar, tick / delay);
            cvc.m_Lens.OrthographicSize = value;
            if (value == tar) 
                break;
            yield return null;
        }
        cvc.m_Lens.OrthographicSize = tar;
    }
}
