using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour
{
    public DoorControl doorScript;
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorScript.doorKey = true;
            door.GetComponent<BoxCollider>().isTrigger = true;
            Destroy(this.gameObject);
        }
    }
}
