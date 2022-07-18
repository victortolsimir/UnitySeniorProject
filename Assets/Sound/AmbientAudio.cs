using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudio : MonoBehaviour
{
    private static AmbientAudio instance;

    private AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> drinkSounds = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> eatSounds = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> coinSounds = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> maleThinkingSounds = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> femaleThinkingSounds = new List<AudioClip>();

    private System.Random random;

    private void Awake()
    {
        instance = this;
        random = new System.Random();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayAudioClip(AudioClip audioClip)
    {
        if (instance && audioClip && instance.audioSource)
        {
            instance.audioSource.PlayOneShot(audioClip);
        }
    }

    public static void PlayAudioClipWithInterrupt(AudioClip audioClip)
    {
        StopAudio();
        PlayAudioClip(audioClip);
    }

    public static void StopAudio()
    {
        if (instance.audioSource.isPlaying) instance.audioSource.Stop();
    }

    public static void PlayDrinkSound()
    {
        if (instance) instance.PlaySoundFromList(instance.drinkSounds);
    }

    public static void PlayEatSound()
    {
        if (instance) instance.PlaySoundFromList(instance.eatSounds);
    }

    public static void PlayCoinSound()
    {
        if (instance) instance.PlaySoundFromList(instance.coinSounds);
    }

    public static void PlayThinkingSound()
    {
        if (instance)
        {
            if (GlobalControl.instance.isMale) instance.PlaySoundFromList(instance.maleThinkingSounds);
            else instance.PlaySoundFromList(instance.femaleThinkingSounds);
        }
    }

    private void PlaySoundFromList(List<AudioClip> audioClips)
    {
        if (audioClips.Count == 0) return;
        var index = random.Next(audioClips.Count);
        PlayAudioClip(audioClips[index]);
    }
}
