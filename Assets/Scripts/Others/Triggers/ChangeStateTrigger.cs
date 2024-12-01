using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            GameManager.Instance.ChangeGameStatus(new VictoryState());
        }
    }
}
