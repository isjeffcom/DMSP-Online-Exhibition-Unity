using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveAudio : MonoBehaviour
{
    public static InteractiveAudio _ins;

    private AudioSource audioSource;


    private void Awake()
    {
        _ins = this;

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void clipChange(string name)
    {
        audioSource.clip = Resources.Load<AudioClip>("InteractiveAudio/" + name);
        audioPlay();
    }

    private void audioPlay()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

}
