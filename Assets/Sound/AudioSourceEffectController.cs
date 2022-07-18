using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceEffectController : AudioSourceController
{
    [SerializeField]
    private CharacterAudioPack audioPack;

    private new void Start()
    {
        base.Start();
        if (CompareTag("Player") && !GlobalControl.instance.isMale)
        {
            var femaleVoice = GetComponent<FemaleAudioContainer>().femaleVoice;
            if (femaleVoice) audioPack = femaleVoice;
        }
    }

    public void PlayDeathSound()
    {
        PlayAudioClip(audioPack.GetDeathSound());
    }

    public void PlayDamageTakenSound()
    {
        PlayAudioClip(audioPack.GetDamageTakenSound());
    }

    public void PlayAttackSound()
    {
        PlayAudioClip(audioPack.GetAttackSound());
    }

    public void PlaySpellSound()
    {
        PlayAudioClip(audioPack.GetSpellSound());
    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        if (audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
