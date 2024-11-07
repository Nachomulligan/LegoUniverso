using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkVFX : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float blinkDuration = 0.5f;

    private void Start()
    {
        StartCoroutine(BlinkAnimation());
    }

    private IEnumerator BlinkAnimation()
    {
        while (true)
        {
            text.enabled = !text.enabled;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
