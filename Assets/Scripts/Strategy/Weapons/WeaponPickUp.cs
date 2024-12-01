using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.High;

    [SerializeField] private WeaponScriptableObject weaponToPickUp;
    private PlayerWeaponSelector playerWeaponSelector;
    
    [SerializeField] private int interactionSound;
    private AudioManager audioManager;

    private void Start()
    {
        playerWeaponSelector = FindObjectOfType<PlayerWeaponSelector>();
        audioManager = GameManager.Instance.audioManager;
    }
    
    private void PlayInteractionSound()
    {
        if (interactionSound >= 0 && interactionSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(interactionSound);
        }
    }
    
    public void Interact()
    {
        if (playerWeaponSelector.activeGun != null && playerWeaponSelector.activeGun.type == GunType.Crowbar)
        {
            playerWeaponSelector.EquipWeapon(weaponToPickUp);
            PlayInteractionSound();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Player can't pick up a new weapon while holding another firearm.");
        }
    }
}
