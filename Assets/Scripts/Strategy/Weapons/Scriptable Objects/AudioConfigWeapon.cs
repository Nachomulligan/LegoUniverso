using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfigWeapon", menuName = "Weapons/Audio Config", order = 4)]
public class AudioConfigWeapon : ScriptableObject
{
    [Range(0, 1f)] public float volume = 1f;
    public AudioClip fire;

    public void PlayShootingSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(fire, volume);
    }
}
