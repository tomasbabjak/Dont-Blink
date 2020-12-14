using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

[RequireComponent(typeof(EyeItemController))]
public class EyeItemAction : MonoBehaviour
{
   EyeItemController eyeItemController;

    [SerializeField] TextMeshProUGUI eyeSizeText = null;

    void Start()
    {
        eyeItemController = GetComponent<EyeItemController>();
        eyeItemController.onEyeUpdate += updateEyeGUI;
    }

    private void updateEyeGUI(int size)
    {
        if (size > 0)
        {
            eyeSizeText.text = size.ToString();
        }
        else
        {
            eyeSizeText.text = "";
        }
    }
}
