using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    public string bgmName = "";

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = null;

        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                sound = s;
                break;
            }
        }

        if (sound == null)
        {
            Debug.Log("Sound : " + name + " File not found!!!");
            return;
        }
        bgmName = sound.name;
        sound.source.Play();
    }

    public void StopBgm()
    {
        Sound sound = null;

        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                sound = s;
                break;
            }
        }

        if (sound == null)
        {
            //Debug.Log("Stop Sound : " + name + "File not found!!!");
            return;
        }

        bgmName = "";
        sound.source.Stop();
    }

    public void PlayBgm(string name)
    {
        //기존에 플레이 되는 배경음과 새로운 배경음이 같을 때
        if (bgmName == name)
        {
            return;
        }

        //기존 배경음 정지
        StopBgm();

        //새로운 배경음 플레이
        Sound sound = null;

        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                sound = s;
                break;
            }
        }

        if (sound == null)
        {
            Debug.Log("Sound : " + name + " File not found!!!");
            return;
        }
        sound.source.Play();
    }
}
