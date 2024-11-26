using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private string scene = "Level 2";
    
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
