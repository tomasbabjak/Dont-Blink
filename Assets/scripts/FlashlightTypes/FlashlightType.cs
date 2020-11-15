using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFlashlightType.asset", menuName = "Flashlight/type")]
public class FlashlightType : ScriptableObject
{

    public int distance;
    public int angle;
    public float powerUssage;
    public bool isDefault = false;
}
