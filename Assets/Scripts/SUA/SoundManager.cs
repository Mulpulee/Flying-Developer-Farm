using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> m_clips;
    private List<AudioSource> m_sources;
    [SerializeField] private float m_sfxVol;

    private void Start()
    {
        m_sources = new List<AudioSource>();
        m_sfxVol = 0.5f;
    }

    public void SetVolume(float value)
    {
        m_sfxVol = value;
    }

    public void PlaySFX(string pName)
    {
        foreach (AudioClip clip in m_clips)
        {
            if (clip.name == pName)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.loop = false;
                source.clip = clip;
                source.volume = m_sfxVol;
                source.Play();

                m_sources.Add(source);
            }
        }
    }

    private void Update()
    {
        foreach (AudioSource source in m_sources)
        {
            if (!source.isPlaying)
            {
                m_sources.Remove(source);
                Destroy(source);
                break;
            }
        }
    }
}




