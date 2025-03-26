using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IEState currentState;

    private Rigidbody rb;
    private Animator anim;
    private PlayerController target;
    [SerializeField] private float speed = 2f;

    public IEState CurrentState { get { return currentState; } }
    public PlayerController Target { get { return target; } }
    public Animator Anim { get { return anim; } }
    public Rigidbody Rb { get { return rb; } }

    private void OnEnable()
    {
        TriggerBossZone.activeBoss += OnActiveBoss;

        TriggerUnlockTarget.unlockTarget += OnUnlockTarget;
    }



    private void OnDisable()
    {
        TriggerBossZone.activeBoss -= OnActiveBoss;
        TriggerUnlockTarget.unlockTarget -= OnUnlockTarget;
    }

    private void OnActiveBoss()
    {
        target = FindObjectOfType<PlayerController>();
        ChangeState(new EChaseState());
    }   
    private void OnUnlockTarget()
    {
        target = null;
        ChangeState(null);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        StopMoving();
        ChangeState(new EChaseState());
    }
    private void FixedUpdate()
    {
        currentState.Execute(this);
        Debug.Log(currentState);
    }

    public void Moving()
    {
        anim.SetTrigger("Ground");
        anim.SetFloat("VelocityZ", 1);
        rb.velocity = transform.forward * speed;
    }

    public void StopMoving()
    {
        anim.ResetTrigger("Ground");
        anim.SetFloat("VelocityZ", 0);
        rb.velocity = Vector3.zero;
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target.Die();
        }
    }

    public void ChangeState(IEState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }
}
