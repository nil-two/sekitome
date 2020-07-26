using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBehaviour : MonoBehaviour
{
    public AnimationClip fadeInClip;
    public AnimationClip fadeOutClip;

    private Animation fade;

    void Start()
    {
        fade = GetComponent<Animation>();
    }

    public void FadeIn()
    {
        fade.clip = fadeInClip;
        fade.Play();
    }

    public void FadeOut()
    {
        fade.clip = fadeOutClip;
        fade.Play();
    }
}
