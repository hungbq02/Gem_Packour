using UnityEngine;

public class TrapLeg : MonoBehaviour
{
     private Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Trap");
            animator.speed = 1f;
        }
    }


}
