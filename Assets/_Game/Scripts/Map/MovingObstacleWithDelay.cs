using UnityEngine;

public class MovingObstacleWithDelay : MonoBehaviour
{
    private Vector3 aPoint;
    private Vector3 bPoint;
    private Vector3 target;
    public float offsetX;
    public float offsetY;
    public float offsetZ;

     private float speed;

    [SerializeField] private float speedToY;
    [SerializeField] private float speedToX;

    [SerializeField] private float delayAtBPoint = 1.5f;
    [SerializeField] private float startDelay = 0f;

    private bool isWaiting = false;
    private float delayTimer = 0f;
    private bool hasStarted = false; // to prevent the arm from moving before the player is ready

    private void Start()
    {
        aPoint = transform.position;
        bPoint = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z +offsetZ);
        target = bPoint;
        speed = 4f;
    }

    private void Update()
    {

        if (!hasStarted)
        {
            startDelay -= Time.deltaTime;
            if (startDelay <= 0f)
            {
                hasStarted = true;
            }
            return;
        }
        if (isWaiting)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= delayAtBPoint)
            {
                isWaiting = false;
                delayTimer = 0f;
                target = target == bPoint ? aPoint : bPoint;
                speed = target == bPoint ? speedToY : speedToX;
            }
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f && !isWaiting)
        {
            isWaiting = true; 
        }
    }
}
