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
                Debug.Log(transform.rotation.y);
                var newRot = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y - 90.0f, 0f), Time.deltaTime * 200);
                transform.rotation = newRot;
                transform.Translate(-0.6f, 0, 0.6f);
                gameObject.GetComponent<BoxCollider>().isTrigger = false;

            }
        }
    }
}
