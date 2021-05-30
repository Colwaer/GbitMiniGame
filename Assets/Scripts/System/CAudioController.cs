using Public;
using System.Collections.Generic;
using UnityEngine;

public enum ESound
{
    Jump,Dash,Wind,GetStar,Collide,Pass,Die
}
public class CAudioController : Singleton<CAudioController>
{
    [SerializeField]
    private AudioSource[] AudioSources;
    private Dictionary<ESound, AudioSource> m_AudioDict = new Dictionary<ESound, AudioSource>();

    protected override void Awake()
    {
        base.Awake();
        AudioSources = GetComponentsInChildren<AudioSource>();
        BuildDict();
    }

    private void BuildDict()
    {
        AudioSource FindSound(string name)
        {
            foreach (AudioSource item in AudioSources)
            {
                if (item.gameObject.name == name)
                    return item;
            }
            Debug.LogWarning("cannot find the audiosouce \"" + name + "\"");
            return null;
        }

        m_AudioDict.Add(ESound.Jump, FindSound("fx_jump"));
        m_AudioDict.Add(ESound.Dash, FindSound("fx_dash"));
        m_AudioDict.Add(ESound.Wind, FindSound("fx_wind"));
        m_AudioDict.Add(ESound.GetStar, FindSound("fx_getstar"));
        m_AudioDict.Add(ESound.Collide, FindSound("fx_collide"));
        m_AudioDict.Add(ESound.Pass, FindSound("fx_pass"));
        m_AudioDict.Add(ESound.Die, FindSound("fx_die"));
    }
    
    public void PlaySound(ESound ename)
    {
        AudioSource audio = m_AudioDict[ename];
        if (audio == null)
            return;
        if (ename == ESound.Pass)
        {
            m_AudioDict[ename].pitch = Random.Range(0.15f, 0.25f);
        }
        m_AudioDict[ename].Play();
    }
    public void StopSound(ESound ename)
    {
        AudioSource audio = m_AudioDict[ename];
        if (audio == null)
            return;
        m_AudioDict[ename].Stop();
    }
    public void StopAllsounds()
    {
        foreach (AudioSource item in AudioSources)
        {
            item.Stop();
        }
    }

    private void Update()
    {
        PlayerController.Instance.FollowPlayer(transform);
    }
}
