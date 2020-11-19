using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
    float a = 0f;

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            a++;
        }
    }

    private void Start()
    {

        float r = UnityEngine.Random.value;

        if (r < 1 / a)
            Play("Theme");
        else if (r > 1 / a && r < (1 / a) * 2)
            Play("ThemeII");
        else if (r > (1/a) * 2)
            Play("ThemeIII");
    }

    public void Play(string a)
    {
        Sound s = Array.Find(sounds, sound => sound.name == a);

        if (s == null)
        {
            Debug.LogWarning("The sound: " + a + " could not be found!");
            return;
        }

        s.source.Play();
    }
}