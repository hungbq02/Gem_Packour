using UnityEngine;
using UnityEngine.EventSystems;

public class UIVirtualTouchZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public Vector2 TouchDist;
    [HideInInspector] public Vector2 PointerOld;
    [HideInInspector] protected int PointerId;
    //   [HideInInspector]
    public bool Pressed;

    [Header("Settings")]
    public bool invertYAxis;
    public float sensitivity = 1f;

    private const float touchDetectionThreshold = 0.1f;
    void Update()
    {
        if (Pressed)
        {
            bool validTouch = false;


            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == PointerId)
                {
                   // Debug.Log("TouchId Matched: " + touch.fingerId);

                    Vector2 newTouchPos = touch.position;
                    TouchDist = newTouchPos - PointerOld;
                    TouchDist = new Vector2(TouchDist.x * sensitivity,
                                            TouchDist.y * sensitivity * (invertYAxis ? -1 : 1));
                    PointerOld = newTouchPos;
                    validTouch = true;
                    break;
                }
            }

            if (!validTouch)
            {
                Debug.Log("No valid touch found for PointerId: " + PointerId);
                Pressed = false; // Dừng cập nhật khi mất touch
                TouchDist = Vector2.zero;
            }
        }
        else
        {
            TouchDist = Vector2.zero;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;

        // Find the touch that matches the ID
        foreach (Touch touch in Input.touches)
        {
            if (Vector2.Distance(touch.position, eventData.position) < touchDetectionThreshold) // Fix joystick anh huong TouchZone
            {
                PointerId = touch.fingerId; //Get the touch ID
                PointerOld = touch.position;
                return;
            }
        }

        // If find not found, use mouse position
        PointerId = -1;
        PointerOld = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
