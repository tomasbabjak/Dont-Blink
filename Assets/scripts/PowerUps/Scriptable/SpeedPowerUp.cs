using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedPowerUp.asset", menuName = "PowerUps/Speed")]
public class SpeedPowerUp : ScriptableObject
{
    public float moveSpeed = 10f;
    public float durationInSec = 5f;
}
