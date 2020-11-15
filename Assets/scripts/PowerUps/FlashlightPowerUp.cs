using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBatteryPowerUp.asset", menuName = "PowerUps/Battery")]
public class FlashlightPowerUp : ScriptableObject
{
    public float addIntensity = 10f;
    public float durationInSec = 5f;
}
