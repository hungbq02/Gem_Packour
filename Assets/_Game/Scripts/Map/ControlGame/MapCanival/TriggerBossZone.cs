using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBossZone : MonoBehaviour
{
    public static event Action activeBoss;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Boss is active");
            activeBoss?.Invoke();
        }
    }
}
