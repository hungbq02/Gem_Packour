using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteracable, IHasID
{
    /// <summary>
    /// ID KEY
    /// TNT: 0
    /// </summary>
    public int idKey;
    public string nameKey;
    private string message;
    public string InteractMessage => message;

    public bool canInteract => true;

    public int ID => idKey;

    public void Interact(Interactor interactor)
    {
        if (interactor.currentKey == 0) 
        {
            interactor.currentKey = 1;
            message = "You pick up "+nameKey;
            //pickup object
            interactor.ActiveKeyObjectByID(idKey);
            Destroy(gameObject);

        }
        else
        {
            message = "Can't pick up, you can only carry one item at a time";
        }
    }
}
