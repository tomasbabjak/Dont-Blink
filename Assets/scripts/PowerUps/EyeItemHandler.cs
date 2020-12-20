using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeItemHandler : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
         {
            other.GetComponent<EyeItemController>().CurrentEyeSize += 1;
            Destroy(gameObject);
         }
    }

}
