using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCloud1_Extra : MonoBehaviour
{
    private SpecialCloud1 m_SpecialCloud1;
    [SerializeField] private SpecialCloud1 SpecialCloud1;

    private void Awake()
    {
        m_SpecialCloud1 = GetComponent<SpecialCloud1>();
    }

    private void Update()
    {
        if(!m_SpecialCloud1.Active)
        {
            SpecialCloud1.CheckPoint = null;
            Destroy(this);
        }
    }
}
