using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallArm : MonoBehaviour
{
    private Vector3 aPoint;
    private Vector3 bPoint;
    private Vector3 target;
    private Vector3 animPoint;

    public float offsetX;

    private float speed;

    [SerializeField] private float speedToY;
    [SerializeField] private float speedToX;

    [SerializeField] private float delayAtBPoint = 1.5f;
    [SerializeField] private float startDelay = 0f;

    private bool isWaiting = false;
    private float delayTimer = 0f;
    private bool hasStarted = false; // to prevent the arm from moving before the player is ready

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();

        aPoint = transform.position;
        bPoint = new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z);
        target = bPoint;
        speed = speedToY;
        animator.SetFloat("Blend", -1f);
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
                if(target == bPoint)
                {
                    animator.speed = 1f;
                }

                speed = target == bPoint ? speedToY : speedToX;
            }
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        float blendValue = Mathf.Lerp(1f, -1f, Mathf.InverseLerp(aPoint.x, bPoint.x, transform.position.x));
        animator.SetFloat("Blend", blendValue);

        if (Vector3.Distance(transform.position, target) < 0.1f && !isWaiting)
        {
            isWaiting = true;
        }
    }
}
