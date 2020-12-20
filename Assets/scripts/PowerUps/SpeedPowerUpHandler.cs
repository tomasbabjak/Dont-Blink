using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUpHandler : MonoBehaviour
{
 
     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
            other.GetComponent<SpeedPowerUpController>().CurrentInventorySize += 1;
            Destroy(gameObject);
         }
     }

}
