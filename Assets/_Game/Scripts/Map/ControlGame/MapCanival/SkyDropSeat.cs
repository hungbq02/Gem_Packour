using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyDropSeat : MonoBehaviour
{
    [SerializeField] private Transform seatPos;
    private PlayerController playerController;
    private Rigidbody rb;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    public void StartSkyDrop()
    {
        playerController.transform.position = seatPos.position;
        playerController.transform.rotation = seatPos.rotation;

        playerController.transform.SetParent(seatPos);

        playerController.Animator.ResetTrigger("Ground");
        playerController.Animator.SetTrigger("sit");
        playerController.Animator.speed = 1f;

        playerController.Controller.enabled = false;
        playerController.Input.enabled = false;
        StartCoroutine(DelayDrop());    
    }
    IEnumerator DelayDrop()
    {
        yield return new WaitForSeconds(1.5f);
        rb.isKinematic = false;

    }
}
