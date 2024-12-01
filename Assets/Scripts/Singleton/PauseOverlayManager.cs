using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOverlayManager : MonoBehaviour
{
    public GameObject pauseOverlay;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (pauseOverlay != null)
        {
            pauseOverlay.SetActive(false);
        }
    }

    public void ShowPauseOverlay()
    {
        if (pauseOverlay != null)
        {
            pauseOverlay.SetActive(true);
        }
    }

    public void HidePauseOverlay()
    {
        if (pauseOverlay == null)
        {
            pauseOverlay.SetActive(false);
        }
    }
}
