using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AsyncScenesManager scenesManager;

    private void Start()
    {
        scenesManager.LoadPermanentSceneAsync();
    }
}
