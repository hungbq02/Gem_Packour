using UnityEngine;

public class WindZoneScript : MonoBehaviour
{
    public Vector3 windDirection = Vector3.forward; 
    public float windStrength = 5f; 

    private void OnTriggerStay(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.inWindZone = true;
            player.windForce = windDirection.normalized * windStrength;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.inWindZone = false;
            player.windForce = Vector3.zero;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + windDirection.normalized * windStrength);
    }

}
