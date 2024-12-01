using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("BGM")]
    public AudioSource music;
    public List<AudioClip> bgTracks;

    [Header("SFX")]
    public AudioSource sfx;
    public List<AudioSO> soundEffects;
    
    private void Awake()
    {
        if (music == null) music = GetComponent<AudioSource>();
        ServiceLocator.Instance.SetService(this);
    }
    
    public void PlayBGM(int bgmIndex)
    {
        if (bgmIndex < 0 || bgmIndex >= bgTracks.Count) return;
        music.clip = bgTracks[bgmIndex];
        music.Play();
    }

    public void PlaySFX(int sfxIndex)
    {
        if (sfxIndex < 0 || sfxIndex >= soundEffects.Count) return;
        soundEffects[sfxIndex].PlaySound(sfx);
    }
}
