using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUnlockTarget : MonoBehaviour
{
    public static event Action unlockTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            unlockTarget?.Invoke();
        }
    }
}
