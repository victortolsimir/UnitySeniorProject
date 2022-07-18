using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceController : MonoBehaviour
{
    protected AudioSource audioSource;
    [SerializeField]
    private AudioLayerSO audioLayer;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioLayer.OnVolumeChanged += OnVolumeChangedCallback;
        audioSource.volume = audioLayer.CurrentVolume;
    }

    private void OnDestroy()
    {
        audioLayer.OnVolumeChanged -= OnVolumeChangedCallback;
    }

    private void OnVolumeChangedCallback(float volume)
    {
        audioSource.volume = volume;
    }
}
