using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextScene : MonoBehaviour
{
    public int nextSceneLoad;
    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void reloadScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(nextSceneLoad);
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            saveState(player);
            LoadNextScene();
            //SceneManager.LoadScene(nextSceneLoad);
        }
    }

    private void saveState(Collider player)
    {
        LevelManager.speedPowerUps = player.GetComponent<SpeedPowerUpController>().CurrentInventorySize;
        LevelManager.eys = player.GetComponent<EyeItemController>().CurrentEyeSize;
        GameObject flashlight = GameObject.FindWithTag("Flashlight");
        LevelManager.batteries = flashlight.GetComponent<BatteryPowerUpController>().CurrentBatterySize;
    }
}
