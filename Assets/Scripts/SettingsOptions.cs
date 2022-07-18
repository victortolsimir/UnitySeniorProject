using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsOptions : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private Slider masterVolumeSlider;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider effectVolumeSlider;

    private void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", 1f);
        effectVolumeSlider.value = PlayerPrefs.GetFloat("Effect Volume", 1f);
    }

    public void OnMasterVolumeChanged()
    {
        PlayerPrefs.SetFloat("Master Volume", masterVolumeSlider.value);
    }

    public void OnMusicVolumeChanged()
    {
        PlayerPrefs.SetFloat("Music Volume", musicVolumeSlider.value);
    }

    public void OnEffectVolumeChanged()
    {
        PlayerPrefs.SetFloat("Effect Volume", effectVolumeSlider.value);
    }

    public void OnBackButton()
    {
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
