using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/AudioLayer")]
public class AudioLayerSO : ScriptableObject
{
    public delegate void VolumeChanged(float volume);
    public event VolumeChanged OnVolumeChanged;
    public float CurrentVolume { get; private set; } = 1f;

    [SerializeField]
    private string layerName;

    private void OnEnable()
    {
        CurrentVolume = PlayerPrefs.GetFloat($"{layerName} Volume", 1f) * PlayerPrefs.GetFloat("Master Volume", 1f);
    }

    public void ChangeVolume(float volume)
    {
        CurrentVolume = volume;
        OnVolumeChanged?.Invoke(volume);
    }
}
