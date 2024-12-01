using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractPriority
{
    Low,
    Medium,
    High
}

public interface IInteractable
{
    InteractPriority InteractPriority { get; }
    void Interact();
}
