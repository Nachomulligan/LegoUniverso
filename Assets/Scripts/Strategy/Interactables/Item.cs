using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.Medium;

    private Transform player;

    private void Start()
    {
        Character playerComponent = FindObjectOfType<Character>();
        if (playerComponent != null)
        {
            player = playerComponent.transform;
        }
    }

    public void Interact()
    {
        transform.SetParent(player.transform);
    }
}
