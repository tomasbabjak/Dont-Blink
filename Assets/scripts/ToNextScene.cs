using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//level management, loading a new scene
public class ToNextScene : MonoBehaviour
{
    public int nextSceneLoad;

    public GameObject endScene;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void reloadScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenuScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextSceneLoad);
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            saveState(player);
            Time.timeScale = 0;
            endScene.SetActive(true);
            //LoadNextScene();
            //SceneManager.LoadScene(nextSceneLoad);
        }
    }

    private void saveState(Collider player)
    {
        GameObject flashlight = GameObject.FindWithTag("Flashlight");
        PlayerData playerData = new PlayerData(
            flashlight ? flashlight.GetComponent<BatteryPowerUpController>().CurrentBatterySize : 0,
            player.GetComponent<SpeedPowerUpController>().CurrentInventorySize,
            player.GetComponent<EyeItemController>().CurrentEyeSize
        );
        LevelManager.SaveLevelData(playerData);
    }
}
