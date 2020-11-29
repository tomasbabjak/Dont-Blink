using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeItemController : MonoBehaviour
{

    public GameObject eyePrefab;
    public float eyeDuration = 10f;
    public float throwStrength = 500;

    public int maxEyeSize = 4;

    private int _currentEyeSize = 0;
    public int CurrentEyeSize
    {
        get {return _currentEyeSize ;}
        set
        {
            if ( value > maxEyeSize )
            {
                return;
            }
            else 
            {
                _currentEyeSize = value;
            }
        }
    }

    public CharController charController;

    void Start()
    {
        charController = gameObject.GetComponent<CharController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentEyeSize > 0)
            {
                ThrowEye();
                _currentEyeSize -= 1;
            }
        }
    }

    private void ThrowEye()
    {
        var ey = Instantiate(eyePrefab, transform.position + transform.forward, Quaternion.identity);
        var rb = ey.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwStrength);
        Object.Destroy(ey, eyeDuration);
    }
}
