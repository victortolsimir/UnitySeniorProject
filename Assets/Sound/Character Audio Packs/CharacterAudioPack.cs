using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/CharacterAudioPack")]
public class CharacterAudioPack : ScriptableObject
{
    [SerializeField]
    private List<AudioClip> deathSounds = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> damageTakenSounds = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> attackSounds = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> spellSounds = new List<AudioClip>();

    public AudioClip GetDeathSound() => GetSound(deathSounds);

    public AudioClip GetDamageTakenSound() => GetSound(damageTakenSounds);

    public AudioClip GetAttackSound() => GetSound(attackSounds);

    public AudioClip GetSpellSound() => GetSound(spellSounds);

    private AudioClip GetSound(List<AudioClip> audioClips)
    {
        if (audioClips.Count == 0) return null;
        return audioClips[Random.Range(0, audioClips.Count)];
    }
}
