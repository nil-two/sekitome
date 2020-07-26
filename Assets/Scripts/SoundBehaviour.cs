using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBehaviour : MonoBehaviour
{
    private static bool dontDestroyed = false;

    private AudioSource audioSource;

    void Start()
    {
        if (dontDestroyed)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        dontDestroyed = true;

        audioSource = GetComponent<AudioSource>();
    }

    public static SoundBehaviour FindInstance()
    {
        return GameObject.Find("AudioSource").GetComponent<SoundBehaviour>();
    }

    public void PlayBGM(AudioClip bgm)
    {
        if (audioSource.clip == bgm)
        {
            return;
        }

        audioSource.clip = bgm;
        audioSource.Play();
    }

    public void PlayBGMWithRestart(AudioClip bgm)
    {
        audioSource.clip = bgm;
        audioSource.Stop();
        audioSource.Play();
    }


    public void PlaySE(AudioClip se, float volume = 1f)
    {
        audioSource.PlayOneShot(se, volume);
    }
}
