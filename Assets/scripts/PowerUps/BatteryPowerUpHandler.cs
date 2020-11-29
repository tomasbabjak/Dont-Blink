using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPowerUpHandler : MonoBehaviour
{
    //public FlashlightPowerUp flashlightPowerUp = null;

 
     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             foreach (Transform child in other.transform)
             {
                 if (child.CompareTag("Flashlight"))
                 {
                     Debug.Log("flashlight");
                     child.GetComponent<BatteryPowerUpController>().CurrentBatterySize += 1;
                     Destroy(gameObject);
                     break;
                 }
             }
         }
     }

   /* public void AddBatteryLife(Flashlight flash)
    {
        //Debug.Log(flash.flashlight.intensity);
        flash.batteryLife += flashlightPowerUp.addIntensity;
        Debug.Log(flashlightPowerUp.addIntensity);
        Destroy(gameObject);

    }*/

}
