using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbWall : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IClimb>(out IClimb climb))
        {
            climb.SetClimbState();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IWalk>(out IWalk walk))
        {
            walk.SetWalkState();
        }
    }
}
