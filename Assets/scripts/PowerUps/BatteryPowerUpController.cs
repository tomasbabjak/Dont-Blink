using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
public class BatteryPowerUpController : MonoBehaviour
{

    public KeyCode reloadBatteryKey = KeyCode.R;

    [SerializeField] TextMeshProUGUI countText = null;

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
                UpdateBatteryGuiCount();
            }
        }
    }

    public BatteryPowerUp batteryPowerUp = null;

    void Awake()
    {
        CurrentBatterySize = LevelManager.GetLevelData().batteries;
        //CurrentBatterySize = LevelManager;
    }

    void Start()
    {
        UpdateBatteryGuiCount();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(reloadBatteryKey))
        {
            if (_currentBatterySize > 0)
            {
                AddBatteryLife(gameObject.GetComponent<Flashlight>());
                _currentBatterySize -= 1;
                UpdateBatteryGuiCount();
            }
        }
        
    }

    public void AddBatteryLife(Flashlight flash)
    {
        flash.batteryLife += batteryPowerUp.addIntensity;
        Debug.Log(batteryPowerUp.addIntensity);
    }

    void UpdateBatteryGuiCount()
    {
        StringBuilder sb = new StringBuilder("Batteries: ");
        sb.Append(_currentBatterySize);
        sb.Append(" / ");
        sb.Append(maxBatterySize);

        countText.text = sb.ToString();
    }
}
