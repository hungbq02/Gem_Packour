using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TNTArea : MonoBehaviour, IInteracable
{
    bool isActive = false;
    public string message;
    [SerializeField] private GameObject tnt;
    [SerializeField] private GameObject explosionEffect; 
    [SerializeField] private float explosionDelay = 3f; 
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private int requiredKeys = 1;

    public string InteractMessage => message;
    public bool canInteract => !isActive;

    public void Interact(Interactor interactor)
    {
        if (!canInteract) return;
        if (interactor.currentKey > 0)
        {
            requiredKeys--;
            interactor.currentKey--;
            tnt.SetActive(true);
            interactor.DeactiveKeyObject();
            StartCoroutine(ExplosionSequence());
            message = "TNT will explode after 3s";

        }
        else
        {
            message = "The path is locked by stone, a TNT would help!";
        }
    }

    private IEnumerator ExplosionSequence()
    {
        isActive = true;
        yield return new WaitForSeconds(explosionDelay);

        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }

        Instantiate(explosionEffect, tnt.transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        //Takedame
    }
}
