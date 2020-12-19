using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject levelsScreen;
    public GameObject playButton;
    public GameObject levelsButton;
    public GameObject introButton;
    public GameObject introductionScreen;
    public GameObject titleText;

    public Button[] levels;


    public void ShowLevels () {
        playButton.SetActive(false);
        levelsButton.SetActive(false);
        levelsScreen.SetActive(true);
        introButton.SetActive(false);
    }

    public void HideLevels () {
        playButton.SetActive(true);
        levelsButton.SetActive(true);
        levelsScreen.SetActive(false);
        introButton.SetActive(true);
    }

    public void ShowIntroduction() {
        playButton.SetActive(false);
        levelsButton.SetActive(false);
        introButton.SetActive(false);
        titleText.SetActive(false);
        introductionScreen.SetActive(true);
    }

    public void HideIntroduction() {
        playButton.SetActive(true);
        levelsButton.SetActive(true);
        introButton.SetActive(true);
        titleText.SetActive(true);
        introductionScreen.SetActive(false);
    }

    public void Start()
    {
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
