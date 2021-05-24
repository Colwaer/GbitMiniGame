using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint0 : MonoBehaviour
{
    private HintImage0[] HintImage1s;
    [SerializeField] ParticleSystem ParticleSystem;

    private void Awake()
    {
        HintImage1s = GetComponentsInChildren<HintImage0>();
    }

    private void FixedUpdate()
    {
        foreach (var item in HintImage1s)
        {
            if (!item.Active) return;
        }
        ParticleSystem.Play();
        Invoke(nameof(Destroy), 2f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
