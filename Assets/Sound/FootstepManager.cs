using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepManager : MonoBehaviour
{
    private static FootstepManager instance;

    private AudioSource audioSource;

    private bool isPlaying = false;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound()
    {
        if (instance)
            instance.OnPlaySound();
    }

    private void OnPlaySound()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.Play();
        }
    }

    public static void StopSound()
    {
        if (instance)
            instance.OnStopSound();
    }

    private void OnStopSound()
    {
        if (isPlaying)
        {
            isPlaying = false;
            audioSource.Stop();
        }
    }
}
