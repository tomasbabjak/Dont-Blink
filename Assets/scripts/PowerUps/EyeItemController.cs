using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
                onEyeUpdate?.Invoke(_currentEyeSize);
            }
        }
    }

    public event EyeThrowDelegate onEyeThrow;
    public delegate void EyeThrowDelegate();

    public event EyeDelegate onEyeUpdate;
    public delegate void EyeDelegate(int size);


    public CharController charController;

    void Awake()
    {
        CurrentEyeSize = LevelManager.GetLevelData().eys;
    }

    void Start()
    {
        charController = gameObject.GetComponent<CharController>();
        onEyeUpdate?.Invoke(_currentEyeSize);
    }

    void Update()
    {
        if (Input.GetButtonDown("ThrowEye"))
        {
            if (_currentEyeSize > 0)
            {
                ThrowEye();
                CurrentEyeSize -= 1;
            }
        }
    }

    private void ThrowEye()
    {
        var ey = Instantiate(eyePrefab, transform.position + transform.forward + transform.up + transform.up, Quaternion.identity);
        var rb = ey.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwStrength);
        onEyeThrow?.Invoke();
        Destroy(ey, eyeDuration);
    }
}
