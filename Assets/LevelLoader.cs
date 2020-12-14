using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject levelsScreen;
    public GameObject playButton;
    public GameObject levelsButton;

    public Button[] levels;

    //private bool showingLevels = false;

    public void ShowLevels () {
        playButton.SetActive(false);
        levelsButton.SetActive(false);
        levelsScreen.SetActive(true);
    }

    public void HideLevels () {
        playButton.SetActive(true);
        levelsButton.SetActive(true);
        levelsScreen.SetActive(false);
    }

    public void Start()
    {
        //Debug.Log(LevelManager.levelData.Count);
        for (int i = 0; i < LevelManager.levelData.Count && i<levels.Length; i++) 
        {
            levels[i].interactable = true;
        }
    }



    public void LoadLevel (int level)
    {
        SceneManager.LoadScene(level);
    }

}
