using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEye : MonoBehaviour
{

    public GameObject eyePrefab;
    public float throwStrength = 20;

    public string itemType = "eye";

    public CharController charController;

    void Start()
    {
        charController = gameObject.GetComponent<CharController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int val;
            if (charController.inventory.TryGetValue(itemType, out val) && val > 0)
            {
                var ey = Instantiate(eyePrefab, transform.position + transform.forward, Quaternion.identity);
                var rb = ey.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * throwStrength);
                Object.Destroy(ey, 30f);
                charController.inventory[itemType] -= 1;
            }
            else
            {
                Debug.Log("no eye in inventory");
            }
        }

        
    }
}
