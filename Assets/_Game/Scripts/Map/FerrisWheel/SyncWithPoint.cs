using UnityEngine;

public class SyncWithPoint : MonoBehaviour
{
    public Transform point; 
    private void Update()
    {
        if (point == null) return;

        transform.position = point.position;

        transform.rotation = point.rotation;
    }
}
