using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EyeItemController : MonoBehaviour
{

    public GameObject eyePrefab;
    public float eyeDuration = 10f;
    public float throwStrength = 500;
    [SerializeField] TextMeshProUGUI eyeSizeText = null;

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
                updateGUISize();
            }
        }
    }

    public CharController charController;

    void Awake()
    {
        CurrentEyeSize = LevelManager.GetLevelData().eys;
        //CurrentEyeSize = LevelManager.eys;
    }

    void Start()
    {
        charController = gameObject.GetComponent<CharController>();
        updateGUISize();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentEyeSize > 0)
            {
                ThrowEye();
                _currentEyeSize -= 1;
                updateGUISize();
            }
        }
    }

    private void updateGUISize()
    {
        if (_currentEyeSize > 0)
        {
            eyeSizeText.text = _currentEyeSize.ToString();
        }
        else
        {
            eyeSizeText.text = "";
        }
    }

    private void ThrowEye()
    {
        var ey = Instantiate(eyePrefab, transform.position + transform.forward + transform.up + transform.up, Quaternion.identity);
        var rb = ey.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwStrength);
        Object.Destroy(ey, eyeDuration);
    }
}
