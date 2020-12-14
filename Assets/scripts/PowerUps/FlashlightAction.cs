using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;
using System.Text;

[RequireComponent(typeof(BatteryPowerUpController)),RequireComponent(typeof(Flashlight))]
public class FlashlightAction : MonoBehaviour
{
    BatteryPowerUpController batteryPowerUpController;
    Flashlight flashlight;
    [SerializeField] TextMeshProUGUI countText = null;
    [SerializeField] Image stateIcon = null;
    [SerializeField] Image healthBar = null;


    void Start()
    {
        batteryPowerUpController = GetComponent<BatteryPowerUpController>();
        flashlight = GetComponent<Flashlight>();
        batteryPowerUpController.onBatteryUpdate += UpdateBatteryGuiCount;
        flashlight.onFlashlightUpdate += UpdateFlashlightGui;
    }

    private void UpdateFlashlightGui(bool isOn, float intensity)
    {
        if (isOn)
        {
            stateIcon.color = Color.yellow;
        }
        else
        {
            stateIcon.color = Color.black;
        }
        healthBar.fillAmount = intensity;
    }

    private void UpdateBatteryGuiCount(int currentsize, int maxsize)
    {
        StringBuilder sb = new StringBuilder("Batteries: ");
        sb.Append(currentsize);
        sb.Append(" / ");
        sb.Append(maxsize);

        countText.text = sb.ToString();
    }
}
