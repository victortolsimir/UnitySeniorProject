using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeController : MonoBehaviour
{
    private float masterVolume;
    private float musicVolume;
    private float effectVolume;

    [SerializeField]
    private AudioLayerSO musicLayer;
    [SerializeField]
    private AudioLayerSO effectLayer;

    private void Start()
    {
        masterVolume = PlayerPrefs.GetFloat("Master Volume", 1f);
        musicVolume = PlayerPrefs.GetFloat("Music Volume", 1f);
        effectVolume = PlayerPrefs.GetFloat("Effect Volume", 1f);
        BroadcastAllChanges();
    }

    private void Update()
    {
        float newMasterVolume = PlayerPrefs.GetFloat("Master Volume", 1f);
        float newMusicVolume;
        float newEffectVolume;

        if (newMasterVolume != masterVolume)
        {
            masterVolume = newMasterVolume;
            BroadcastAllChanges();
        }
        else if ((newMusicVolume = PlayerPrefs.GetFloat("Music Volume", 1f)) != musicVolume)
        {
            musicVolume = newMusicVolume;
            musicLayer.ChangeVolume(musicVolume * masterVolume);
        }
        else if ((newEffectVolume = PlayerPrefs.GetFloat("Effect Volume", 1f)) != effectVolume)
        {
            effectVolume = newEffectVolume;
            effectLayer.ChangeVolume(effectVolume * masterVolume);
        }
    }

    private void BroadcastAllChanges()
    {
        musicLayer.ChangeVolume(musicVolume * masterVolume);
        effectLayer.ChangeVolume(effectVolume * masterVolume);
    }
}
