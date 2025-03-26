using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    public void StartMoving()
    {
        if (!isMoving)
        {
            isMoving = true;
            targetPosition = transform.position + Vector3.up * moveDistance;
        }
    }
}
