using UnityEngine;
using UnityEngine.Events;

public class ButtonActiveMap : MonoBehaviour, IInteracable
{
    public string message;
    public string InteractMessage => message;
    public bool canInteract => !isActived;

    public bool isActived = false;

    [Header("Button Event")]
    public UnityEvent OnButtonPressed; // UnityEvent riêng cho mỗi button

    public void Interact(Interactor interactor)
    {
        Debug.Log("Button is pressed");
        if (!isActived)
        {
            isActived = true;
            OnButtonPressed?.Invoke(); // Kích hoạt sự kiện của button
        }
    }
}
