using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MazeTrigger : MonoBehaviour
{
    [SerializeField] private MazeEnemy enemy;
    [SerializeField] private LayerMask playerLayer;
    
    [SerializeField] private int triggerSound;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameManager.Instance.audioManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            enemy.SetPlayerInMaze(true);
            PlayTriggerSound();
        }
    }
    
    private void PlayTriggerSound()
    {
        if (triggerSound >= 0 && triggerSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(triggerSound);
        }
    }
}