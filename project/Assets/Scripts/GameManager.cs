﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Excarrotbur excarrotbur;

    void Start()
    {
        if(excarrotbur != null)
            excarrotbur.Subscription(PlayLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            if (player.Life <= 0)
                GameOver();          
    }

    private void PlayLevel1()
    {
        SceneManager.LoadScene(Constants.LEVEL1_SCENE_NAME);
    }

    public void PlayLevel()
    {
        if (SceneManager.GetActiveScene().name == Constants.LEVEL1_SCENE_NAME)
            PlayLevel2();
        else if (SceneManager.GetActiveScene().name == Constants.LEVEL2_SCENE_NAME)
            Victory();
    }

    private void PlayLevel2()
    {
        SceneManager.LoadScene(Constants.LEVEL2_SCENE_NAME);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(Constants.GAME_OVER_SCENE_NAME);
    }

    public void Victory()
    {
        SceneManager.LoadScene(Constants.VICTORY_SCENE_NAME);
    }
}