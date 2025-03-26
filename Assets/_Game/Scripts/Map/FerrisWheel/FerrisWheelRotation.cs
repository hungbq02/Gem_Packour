using UnityEngine;

public class FerrisWheelRotation : MonoBehaviour
{
    public Vector3 rotationAxis = new Vector3(1, 0, 0); // Set a default axis, e.g., X-axis
    public float rotationSpeed = 10f; // Speed of the rotation

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}