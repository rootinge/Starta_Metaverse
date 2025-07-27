using UnityEngine;

public class WaterObstacle : MonoBehaviour
{
    [SerializeField]
    private PlayerType obstacleType;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FireWaterController player = other.GetComponent<FireWaterController>();
            if(player.playerName != obstacleType)
            {
                player.Death();
            }
        }

    }
}
