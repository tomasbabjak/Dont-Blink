using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public bool doorKey;
    public int doorPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorKey)
            {
                if (doorPosition == 1)
                {
                    var newRot = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y - 90.0f, 0f), Time.deltaTime * 200);
                    transform.rotation = newRot;
                    transform.Translate(-0.6f, 0, 0.6f);
                    gameObject.GetComponent<BoxCollider>().isTrigger = false;
                }
                else if (doorPosition == 2)
                {
                    var newRot = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y - 90.0f, 0f), Time.deltaTime * 200);
                    transform.rotation = newRot;
                    transform.Translate(1f, 0, 1f);
                    gameObject.GetComponent<BoxCollider>().isTrigger = false;

                }

            }
        }
    }
}
