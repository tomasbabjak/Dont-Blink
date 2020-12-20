using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(SpeedPowerUpController))]
public class SpeedPowerUpAction : MonoBehaviour
{
    
    SpeedPowerUpController speedPowerUpController;
    
    [SerializeField] Image stateIcon = null;
    [SerializeField] TextMeshProUGUI powerUpsSizeText = null;

    void Start()
    {
        speedPowerUpController = GetComponent<SpeedPowerUpController>();
        speedPowerUpController.onSpeedPoweUpUpdate += UpdateSpeedGui;
    }

    private void UpdateSpeedGui(bool isActive, int size)
    {

        powerUpsSizeText.text = size > 0 ? size.ToString() : "";
        stateIcon.color = isActive ? Color.yellow : Color.black;
        
    }
}
