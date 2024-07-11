using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance  = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject @object = new GameObject();
                    instance = @object.AddComponent<SoundManager>();
                }
            }

            return instance;
        }
    }

    private Dictionary<string, AudioClip> m_clips;
    private List<AudioSource> m_sources;
    [SerializeField] private float m_sfxVol;
    [SerializeField] private float m_masterVol;


    private void Start()
    {
        m_sources = new List<AudioSource>();
        m_sfxVol = 0.5f;
        m_masterVol = 1.0f;

        m_clips = new Dictionary<string, AudioClip>();

        foreach (var s in Resources.LoadAll<AudioClip>("Sounds"))
        {
            m_clips.Add(s.name, s);
        }
    }


    public void SetVolume(float value)
    {
        m_sfxVol = value;
    }
    public void SetMasterVolume(float value)
    {
        m_sfxVol = value;
    }

    public void PlaySFX(string pName)
    {
        if (!m_clips.ContainsKey(pName)) return;

        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.loop = false;
        source.clip = m_clips[pName];
        source.volume = m_sfxVol;
        source.Play();

       m_sources.Add(source);
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





