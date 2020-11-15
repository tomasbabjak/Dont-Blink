using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public FlashlightPowerUp flashlightPowerUp = null;
 
     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             foreach (Transform child in other.transform)
             {
                 if (child.CompareTag("Flashlight"))
                 {
                     child.GetComponent<Flashlight>().AddBatteryLife(flashlightPowerUp.addIntensity);
                     break;
                 }
             }
            Destroy(gameObject);
         }
     }

}
