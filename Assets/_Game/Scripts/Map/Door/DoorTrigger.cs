using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteracable
{
    public Door door;
    public int requiredKeys = 1;
    private bool isOpened = false;

    [Header("messeger")]
    private string message;

    //Check if the chest is opened
    public bool canInteract => !isOpened;
    public string InteractMessage => message;

    public void Interact(Interactor interactor)
    {
        if (!canInteract)
        {
            return;
        }
        // 2 types door (need key and ><)
        if (requiredKeys <= 0 || interactor.currentKey > 0)
        {
            if (requiredKeys > 0)
            {
                requiredKeys--;
                interactor.currentKey--;
                message = "A Gear is inserted";
            }

            if (requiredKeys <= 0)
            {
                Open(interactor.transform.position);
            }
        }
        else
        {
            message = "The door is locked! Something is missing.";
        }
    }
    private void Open(Vector3 playerPos)
    {
        message = "The door is opened!";
        isOpened = true;
        door.Open(playerPos);

    }
}
