using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

[RequireComponent(typeof(BatteryPowerUpController)),RequireComponent(typeof(Flashlight))]
public class FlashlightAction : MonoBehaviour
{
    BatteryPowerUpController batteryPowerUpController;
    Flashlight flashlight;

    private bool _flashlightStatus = true;
    public bool FlashlightStatus
    {
        get {return _flashlightStatus ;}
        set
        {
            if ( value ==_flashlightStatus )
            {
                return;
            }
            else 
            {
                _flashlightStatus = value;
                flashlightSwitch.Play();
            }
        }
    }

    [SerializeField] TextMeshProUGUI countText = null;
    [SerializeField] Image stateIcon = null;
    [SerializeField] Image healthBar = null;

    AudioSource flashlightSwitch;


    void Start()
    {
        batteryPowerUpController = GetComponent<BatteryPowerUpController>();
        flashlight = GetComponent<Flashlight>();
        flashlightSwitch = flashlight.GetComponent<AudioSource>();
        batteryPowerUpController.onBatteryUpdate += UpdateBatteryGuiCount;
        flashlight.onFlashlightUpdate += UpdateFlashlightGui;
    }

    //  update flashlight state in GUI
    private void UpdateFlashlightGui(bool isOn, float intensity)
    {
        FlashlightStatus = isOn;
        stateIcon.color = isOn ? Color.yellow : Color.black;
        healthBar.fillAmount = intensity;
    }

    //update count of remaining batteries
    private void UpdateBatteryGuiCount(int currentsize, int maxsize)
    {
        StringBuilder sb = new StringBuilder("Batteries: ");
        sb.Append(currentsize);
        sb.Append(" / ");
        sb.Append(maxsize);

        countText.text = sb.ToString();
    }
}
