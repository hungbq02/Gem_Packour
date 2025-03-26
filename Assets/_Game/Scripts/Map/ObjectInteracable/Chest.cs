using UnityEngine;

public class Chest : MonoBehaviour, IInteracable
{
    [SerializeField] private string message;
    public string InteractMessage => message;


    public int requiredKeys = 1;
    private bool isOpened = false;
    //Check if the chest is opened
    public bool canInteract => !isOpened;

    public void Interact(Interactor interactor)
    {
        if (isOpened)
        {
            Debug.Log("Chest is already opened!");
            return;
        }
        if (interactor.currentKey > 0)
        {
            requiredKeys--;
            interactor.currentKey--;
            Debug.Log("Key-1");
            if (requiredKeys <= 0)
            {
                Open();
                Debug.Log("Chest is opened!");
            }
        }
        else
        {
            Debug.Log("Not enough keys!");
        }
    }

    private void Open()
    {
        isOpened = true;
    }
}
