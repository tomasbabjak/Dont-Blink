using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class SpeedPowerUpController : MonoBehaviour
{
    public KeyCode applySpeedKey = KeyCode.Q;

    public SpeedPowerUp speedPowerUp = null;

    [SerializeField] Image stateIcon = null;
    [SerializeField] TextMeshProUGUI powerUpsSizeText = null;

    CharController charController = null;

    public int maxInventorySize = 2;

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
                updateGUISize();
            }
        }
    }

    void Awake()
    {
        CurrentInventorySize = LevelManager.GetLevelData().speedPowerUps;
        //CurrentInventorySize = LevelManager.speedPowerUps;
    }

    void Start()
    {
        charController = gameObject.GetComponent<CharController>();
        updateGUISize();
        tooglePowerUpUssageIcon(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(applySpeedKey))
        {
            if (_currentInventorySize > 0)
            {
                tooglePowerUpUssageIcon(true);
                StartCoroutine(ApplySpeedKeyPowerUp());
                _currentInventorySize -= 1;
                updateGUISize();
            }
        }
    }

    public IEnumerator ApplySpeedKeyPowerUp()
    {
        charController.movespeed = speedPowerUp.moveSpeed;
        yield return new WaitForSeconds(speedPowerUp.durationInSec);
        charController.movespeed = charController.defaultSpeed;
        tooglePowerUpUssageIcon(false);
        //Destroy(gameObject);
    }

    private void updateGUISize()
    {
        if (_currentInventorySize > 0)
        {
            powerUpsSizeText.text = _currentInventorySize.ToString();
        }
        else
        {
            powerUpsSizeText.text = "";
        }
    }

    private void tooglePowerUpUssageIcon(bool state)
    {
        if (state)
        {
            stateIcon.color = Color.yellow;
        }
        else
        {
            stateIcon.color = Color.black;
        }
    }
}
