using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyActions : MonoBehaviour
{

    EnemyController enemyController;

    public GameObject gameOverUI;

    void Awake()
    {
        gameOverUI.SetActive(false);
    }

    void Start()
    {

        enemyController = GetComponent<EnemyController>();
        enemyController.onPlayerHit += playerhit;

    }

    private void playerhit()
    {
        gameOverUI.SetActive(true);
        gameOverUI.GetComponent<AudioSource>().Play();
    }
}
