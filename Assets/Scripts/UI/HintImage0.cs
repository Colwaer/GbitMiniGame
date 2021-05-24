using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintImage0 : MonoBehaviour
{
    public SpecialCloud0 SpecialCloud0;
    private SpriteRenderer m_SpriteRenderer;
    private float t_Fade = 1f;
    private Coroutine co_Change;

    [SerializeField]
    private bool _Active;
    public bool Active
    {
        get
        {
            return _Active;
        }
        set
        {
            if(_Active != value)
            {
                if (co_Change != null)
                    StopCoroutine(co_Change);
                if (value)
                {
                    co_Change = StartCoroutine(Show());
                }
                else
                {
                    co_Change = StartCoroutine(Fade());
                }
                _Active = value;
            }
        }
    }
    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.clear;
    }

    private void FixedUpdate()
    {
        Active = !SpecialCloud0.Active;
    }

    private IEnumerator Show()
    {
        for (float timer = 0; timer < t_Fade; timer += Time.deltaTime)
        {
            m_SpriteRenderer.color = Color.Lerp(Color.clear, Color.green, timer / t_Fade);
            yield return null;
        }
        co_Change = null;
    }
    private IEnumerator Fade()
    {
        for (float timer = 0; timer < t_Fade; timer += Time.deltaTime)
        {
            m_SpriteRenderer.color = Color.Lerp(Color.green, Color.clear,timer/t_Fade);
            yield return null;
        }
        co_Change = null;
    }
}
