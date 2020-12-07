using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public static int batteries = 0;
    static public int speedPowerUps = 0;
    public static int eys = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
