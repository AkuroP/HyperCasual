using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    private Dictionary<SoundState, List<Sound>> DicoActualSound = new Dictionary<SoundState, List<Sound>>();


    public static AudioManager instance;

    void Start()
    {
        // Garantit que ce GameObject ne sera pas d�truit lorsque la sc�ne est chang�e
        DontDestroyOnLoad(gameObject);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (Sound s in sounds)
       {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.outputAudioMixerGroup = s.AudioMixer;

            if (!DicoActualSound.ContainsKey(s.ActualSound))
            {
                DicoActualSound.Add(s.ActualSound, new List<Sound>());
            }
            DicoActualSound[s.ActualSound].Add(s);

            if (s.AtStart)
                Play(s.Name);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.Source.Play();
    }

    public void Stop(SoundState soundState)
    {
        if (DicoActualSound.ContainsKey(soundState))
        {
            int i = Random.Range(0, DicoActualSound[soundState].Count);

            Sound s = DicoActualSound[soundState][i];
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }
            s.Source.Stop();

        }
        else
        {
            throw new ArgumentNullException();
        }
    }

    public void PlayRandom(SoundState soundState)
    {
        if (DicoActualSound.ContainsKey(soundState))
        {
            int i = Random.Range(0, DicoActualSound[soundState].Count);

            Sound s = DicoActualSound[soundState][i];
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }
            s.Source.Play();
        }
        else
        {
            throw new ArgumentNullException();
        }   
    }

    public void Paused(SoundState soundState)
    {
        if (DicoActualSound.ContainsKey(soundState))
        {
            int i = Random.Range(0, DicoActualSound[soundState].Count);

            Sound s = DicoActualSound[soundState][i];
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }
            s.Source.Pause();
            
        }
        else
        {
            throw new ArgumentNullException();
        }
    }
    public void Unpaused(SoundState soundState)
    {
        if (DicoActualSound.ContainsKey(soundState))
        {
            int i = Random.Range(0, DicoActualSound[soundState].Count);

            Sound s = DicoActualSound[soundState][i];
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }
            s.Source.UnPause();

        }
        else
        {
            throw new ArgumentNullException();
        }
    }
}
