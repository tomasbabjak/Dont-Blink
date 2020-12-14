using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPowerUpHandler : MonoBehaviour
{

     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             GameObject flashlight = GameObject.FindWithTag("Flashlight");
             flashlight.GetComponent<BatteryPowerUpController>().CurrentBatterySize += 1;
             Destroy(gameObject);
         }
     }

}
