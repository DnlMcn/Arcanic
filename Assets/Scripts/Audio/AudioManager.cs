using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static Sound[] sounds;
    [SerializeField] Sound[] setSounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        sounds = setSounds;

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public static void Play(string name)
    {
        if (name == null)
        {
            Debug.LogWarning("Sound " + name + " was not found.");
            return;
        }
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
