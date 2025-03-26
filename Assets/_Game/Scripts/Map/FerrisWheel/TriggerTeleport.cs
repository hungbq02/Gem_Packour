using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleport : MonoBehaviour
{
    [SerializeField] private Transform teleportLocation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            other.transform.position = teleportLocation.position;
        }
    }
}
