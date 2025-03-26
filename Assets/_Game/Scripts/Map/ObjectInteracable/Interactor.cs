using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius = 1f;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private InteractionMessageUI interactionMessageUI; //Display message when interact with object

    private readonly Collider[] colliders = new Collider[3];
    private IInteracable currentInteractable = null;
    [SerializeField] private int numFoundColliders;
    //object chua all key
     private GameObject keyObject;
    private int idKeyActive;

    public int currentKey = 0;
    [SerializeField] private Button btnPickUp;

    private void Start()
    {
        keyObject = GameObject.Find("Key");
        btnPickUp.onClick.AddListener(OnPickupButtonPressed);

    }
    private void Update()
    {
        numFoundColliders = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, colliders, interactionLayer);
        if (numFoundColliders > 0)
        {
            currentInteractable = colliders[0].GetComponentInParent<IInteracable>();
            if (currentInteractable != null && currentInteractable.canInteract)
            {
                //Complete unlock condition, don't show button pickup
                btnPickUp.gameObject.SetActive(true);
            }
        }
        else
        {
            currentInteractable = null;
            btnPickUp.gameObject.SetActive(false);
        }
    }
    //Pickup button pressed
    public void OnPickupButtonPressed()
    {
        currentInteractable?.Interact(this);
        interactionMessageUI.ShowMessage(currentInteractable.InteractMessage);

    }
    //Active key show on player
    internal void ActiveKeyObjectByID(int idKey)
    {
        keyObject.transform.GetChild(idKey).gameObject.SetActive(true);
        idKeyActive = idKey;
    } 
    public void DeactiveKeyObject()
    {
        keyObject.transform.GetChild(idKeyActive).gameObject.SetActive(false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}
