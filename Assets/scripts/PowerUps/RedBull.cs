using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBull : MonoBehaviour
{
    public SpeedPowerUp speedPowerUp = null;
 
     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             
             //other.GetComponent<CharController>().ApplySpeedPowerUp(speedPowerUp);

            other.GetComponent<CharController>().StartCoroutine(other.GetComponent<CharController>().ApplySpeedPowerUp(speedPowerUp));
            Destroy(gameObject);
         }
     }

}
