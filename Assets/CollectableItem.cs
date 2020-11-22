using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemType = "eye";

    private void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player"))
         {
             int val;
             //other.GetComponent<CharController>().ApplySpeedPowerUp(speedPowerUp);
            if (other.GetComponent<CharController>().inventory.TryGetValue(itemType, out val))
            {
                other.GetComponent<CharController>().inventory[itemType] = val + 1;
            }
            else
            {
                other.GetComponent<CharController>().inventory.Add(itemType, 1);
            }
            
            Destroy(gameObject);
            Debug.Log(other.GetComponent<CharController>().inventory[itemType]);
         }
    }

}
