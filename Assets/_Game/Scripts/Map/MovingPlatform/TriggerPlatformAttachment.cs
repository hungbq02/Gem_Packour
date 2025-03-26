using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatformAttachment : MonoBehaviour
{
    [SerializeField] private Transform platform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Enter");
            other.transform.SetParent(platform.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
