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

    private float batteryLife = 100f;
    private bool isActive = false;
    private bool outOfBattery = false;

    private float defaultIntensity = 100f;

    public KeyCode flashlightToggleKey = KeyCode.L;
    public KeyCode changeLightSetting = KeyCode.E;

    private IEnumerator IE_UpdateBatteryLife = null;

    public FlashlightTypePool flashlights;
    private Light flashlight;

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


    void Start()
    {
        fow = gameObject.transform.parent.gameObject.GetComponent<FieldOfView>();
        flashlight = gameObject.GetComponent<Light>();
        InitializeDefaults();
        ToogleFlashlight(!isActive);
    }

    void ToogleFlashlight(bool state)
    {
        isActive = state;
        flashlight.enabled = state;

        UpdateBatteryLifeProcess();

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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(flashlightToggleKey))
        {
            ToogleFlashlight(!isActive);
        }
/*
        if (isActive)
        {
            flashlight.enabled = true;
            flashlight.intensity -= batteryLife / batteryLifeSecounds * Time.deltaTime;
        } 
        else 
        {
            flashlight.enabled = false;
        }
*/
        if (Input.GetKeyDown(changeLightSetting) && isActive)
        {
            changeFlashlight();
        }
        
    }

    private void changeFlashlight()
    {
        FlashlightType newFlashlight = flashlights.GetNextFlashlightType();
        
        distance = newFlashlight.distance;
        angle = newFlashlight.angle;
        powerUssage = newFlashlight.powerUssage;

        fow.viewAngle = angle;
        fow.viewRadius = distance;

        flashlight.spotAngle = angle;
        flashlight.range = distance;

    }

    public void AddBatteryLife(float _batteryPower)
    {
        flashlight.intensity += _batteryPower;

    }
}
