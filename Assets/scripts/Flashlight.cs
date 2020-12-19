using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Flashlight : MonoBehaviour
{

    public bool autoReduce = true;
    private float powerUssage = 1.1f;

    private const float minBatteryLife = .0f;

    private const float maxBatteryLife = 100f;

    public float batteryLife = 100f;
    private bool isActive = false;
    private bool outOfBattery = false;

    private float defaultIntensity = 100f;

    private IEnumerator IE_UpdateBatteryLife = null;

    public FlashlightTypePool flashlights;
    public Light flashlight;

    private int distance;
    private int angle;
    private FieldOfView fow;

    private int fowNoLightDistance = 1;
    private int fowNoLightAngle = 30;

    float getLifePercentage
    {
        get
        {
            return batteryLife / maxBatteryLife;
        }
    }
    float getLightIntensity
    {
        get
        {
            return defaultIntensity * getLifePercentage;
        }
    }

    public event FlashlightDelegate onFlashlightUpdate;
    public delegate void FlashlightDelegate(bool isOn, float intensity);


    void Start()
    {
        fow = gameObject.transform.root.gameObject.GetComponent<FieldOfView>();
        flashlight = gameObject.GetComponent<Light>();
        InitializeDefaults();
        ToogleFlashlight(!isActive);
    }

    void ToogleFlashlight(bool state)
    {
        isActive = state;
        flashlight.enabled = state;

        UpdateBatteryLifeProcess();
        onFlashlightUpdate?.Invoke(isActive, getLifePercentage);

        if (!isActive)
        {
            fow.viewAngle = fowNoLightAngle;
            fow.viewRadius = fowNoLightDistance;
        }
        else
        {
            fow.viewAngle = angle;
            fow.viewRadius = distance;
        }

    }

    private void UpdateBatteryLifeProcess()
    {
        if(IE_UpdateBatteryLife != null) { StopCoroutine(IE_UpdateBatteryLife); }

        if (isActive && !outOfBattery)
        {
            if (autoReduce)
            {
                IE_UpdateBatteryLife = ReduceBattery();
                StartCoroutine(IE_UpdateBatteryLife);
            }
            return;
        }
    }

    private IEnumerator ReduceBattery()
    {
        while (batteryLife > .0f)
        {

            var newValue = batteryLife - powerUssage * Time.deltaTime;
            batteryLife = Mathf.Clamp(newValue, minBatteryLife, maxBatteryLife);
            flashlight.intensity = getLightIntensity;
            onFlashlightUpdate?.Invoke(isActive, getLifePercentage);
            yield return null;
        }
        outOfBattery = true;
        fow.viewAngle = fowNoLightAngle;
        fow.viewRadius = fowNoLightDistance;
    }

    private void InitializeDefaults()
    {
        FlashlightType defaultflash = flashlights.GetDefaultFlashlight();

        distance = defaultflash.distance;
        angle = defaultflash.angle;
        powerUssage = defaultflash.powerUssage;

        fow.viewAngle = angle;
        fow.viewRadius = distance;

        flashlight.spotAngle = angle;
        flashlight.range = distance;

    }

    void Update()
    {
        if(Input.GetButtonDown("ToogleLight"))
        {
            ToogleFlashlight(!isActive);
        }

        if (Input.GetButtonDown("ChangeLightType") && isActive)
        {
            changeFlashlight();
        }
        
    }

    private void changeFlashlight()
    {
        ToogleFlashlight(false);
        FlashlightType newFlashlight = flashlights.GetNextFlashlightType();
        
        distance = newFlashlight.distance;
        angle = newFlashlight.angle;
        powerUssage = newFlashlight.powerUssage;

        fow.viewAngle = angle;
        fow.viewRadius = distance;

        flashlight.spotAngle = angle;
        flashlight.range = distance;

        ToogleFlashlight(true);

    }
}
