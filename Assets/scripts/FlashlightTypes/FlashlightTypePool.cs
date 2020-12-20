using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a list of different types of batteries used in the game
[CreateAssetMenu(fileName = "NewFlashlightTypePool.asset", menuName = "Flashlight/Pool")]
public class FlashlightTypePool : ScriptableObject
{
    public List<FlashlightType> flashlightTypes;

    private int numberType = 0;
    public FlashlightType GetNextFlashlightType(){
        numberType = (numberType + 1) % flashlightTypes.Count;
        return flashlightTypes[numberType];
    }

    public FlashlightType GetDefaultFlashlight(){
        foreach(FlashlightType flashlight in flashlightTypes)
        {
            if (flashlight.isDefault)
            {
                return flashlight;
            }
        }
        return null;
    }

}
