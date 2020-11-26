using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public bool doorKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorKey)
            {
                var newRot = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, 90.0f, 0.0f), Time.deltaTime * 200);
                transform.rotation = newRot;
                transform.Translate(-0.6f, 0, 0.6f);
                transform.GetComponent<BoxCollider>().isTrigger = false;
                foreach (Transform child in transform)
                {
                    Debug.Log(child);
                    child.GetComponent<BoxCollider>().isTrigger = false;
                }
            }
        }
    }
}
