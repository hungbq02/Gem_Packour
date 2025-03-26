using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrain : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 12f;

    [Header("Reset Settings")]
    [SerializeField] private float resetDelay = 3f; 

    private Transform target;
    private bool isResetting = false; 

    private void Start()
    {
        transform.position = startPoint.position;
        target = endPoint;
    }

    private void Update()
    {
        if (isResetting) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
        {
            StartCoroutine(ResetToStart());
        }
    }

    private IEnumerator ResetToStart()
    {
        isResetting = true; 
        yield return new WaitForSeconds(resetDelay); 

        transform.position = startPoint.position;
        target = endPoint; 
        isResetting = false; 
    }
}
