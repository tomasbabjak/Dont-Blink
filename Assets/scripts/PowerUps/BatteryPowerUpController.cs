using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BatteryPowerUpController : MonoBehaviour
{

    public int maxBatterySize = 4;

    private int _currentBatterySize = 0;
    public int CurrentBatterySize
    {
        get {return _currentBatterySize ;}
        set
        {
            if ( value > maxBatterySize )
            {
                return;
            }
            else 
            {
                _currentBatterySize = value;
                onBatteryUpdate?.Invoke(_currentBatterySize, maxBatterySize);
            }
        }
    }


    public event BatteryDelegate onBatteryUpdate;
    public delegate void BatteryDelegate(int currentsize, int maxsize);

    public BatteryPowerUp batteryPowerUp = null;

    void Awake()
    {
        CurrentBatterySize = LevelManager.GetLevelData().batteries;
    }

    void Start()
    {
        onBatteryUpdate?.Invoke(_currentBatterySize, maxBatterySize);
    }

    void Update()
    {
        if(Input.GetButtonDown("ReloadFlashlight"))
        {
            if (_currentBatterySize > 0)
            {
                AddBatteryLife(gameObject.GetComponent<Flashlight>());
                CurrentBatterySize -= 1;
            }
        }
        
    }

    public void AddBatteryLife(Flashlight flash)
    {
        flash.batteryLife += batteryPowerUp.addIntensity;
    }
}
