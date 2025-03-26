using UnityEngine;

public class TriggerPowerUp : MonoBehaviour
{
    public enum PowerUpType { SpeedBoost, JumpBoost, Transform }
    public PowerUpType powerUpType;

    [Header("PowerUp Settings")]
    public float value = 2f;   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("PowerUp");
                ApplyPowerUp(player);
                Destroy(gameObject);
            }
        }
    }


    private void ApplyPowerUp(PlayerController player)
    {
        switch (powerUpType)
        {
            case PowerUpType.SpeedBoost:
                player.ApplySpeedBoost(value);
                break;
            case PowerUpType.JumpBoost:
                player.ApplyJumpBoost(value);
                break;
            case PowerUpType.Transform:
                player.TransformPlayer(true, value);
                break;
        }
    }


}
