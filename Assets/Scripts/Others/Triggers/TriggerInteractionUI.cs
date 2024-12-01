using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject text;

    private void Start()
    {
        text.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
