using Public;
using System.Collections.Generic;
using UnityEngine;

public enum ESound
{
    Jump,Dash,Wind
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
    }
    
    public void PlaySound(ESound ename)
    {
        m_AudioDict[ename]?.Play();
    }
    public void StopSound(ESound ename)
    {
        m_AudioDict[ename]?.Stop();
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
