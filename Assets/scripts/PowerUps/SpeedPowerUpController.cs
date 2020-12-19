using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUpController : MonoBehaviour
{
    public KeyCode applySpeedKey = KeyCode.Q;

    public SpeedPowerUp speedPowerUp = null;

    CharController charController = null;

    public int maxInventorySize = 2;
    private bool isActivated = false;

    private int _currentInventorySize = 0;
    public int CurrentInventorySize
    {
        get {return _currentInventorySize; }
        set 
        {
            if (value > maxInventorySize )
            {
                return;
            }
            else
            {
                _currentInventorySize = value;
                onSpeedPoweUpUpdate?.Invoke(isActivated,_currentInventorySize);
            }
        }
    }

    public event SpeedPUDelegate onSpeedPoweUpUpdate;
    public delegate void SpeedPUDelegate(bool isActive, int size);

    public event UsingDrinkDelegate onDrinkUse;
    public delegate void UsingDrinkDelegate();

    void Awake()
    {
        CurrentInventorySize = LevelManager.GetLevelData().speedPowerUps;
    }

    void Start()
    {
        charController = gameObject.GetComponent<CharController>();
        onSpeedPoweUpUpdate?.Invoke(false,_currentInventorySize);
    }

    void Update()
    {
        if(Input.GetKeyDown(applySpeedKey))
        {
            if (_currentInventorySize > 0)
            {
                isActivated = true;
                onDrinkUse?.Invoke();
                StartCoroutine(ApplySpeedKeyPowerUp());
                CurrentInventorySize -= 1;
                //onSpeedPoweUpUpdate?.Invoke(isActivated,_currentInventorySize);
            }
        }
    }

    public IEnumerator ApplySpeedKeyPowerUp()
    {
        charController.movespeed = speedPowerUp.moveSpeed;
        yield return new WaitForSeconds(speedPowerUp.durationInSec);
        charController.movespeed = charController.defaultSpeed;
        isActivated = false;
        onSpeedPoweUpUpdate?.Invoke(isActivated,_currentInventorySize);
    }

}
