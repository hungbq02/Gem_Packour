using UnityEngine;
using System.Collections;

public class PingPongPlatform: MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 2f;

    private Transform target;

    private void Start()
    {
        transform.position = startPoint.position; 
        target = endPoint; 
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            target = target == startPoint ? endPoint : startPoint;
        }
    }
}
