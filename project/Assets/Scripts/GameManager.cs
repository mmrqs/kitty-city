using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game handler, it is responsible to switch between the levels.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Player of the game
    /// </summary>
    public Player player;
    /// <summary>
    /// Excarotbur : the objective of the game
    /// </summary>
    public Excarrotbur excarrotbur;

    /// <summary>
    /// Start method
    /// If excarrotbur is not null, we subscribe the playlevel method to its event.
    /// </summary>
    void Start()
    {
        if(excarrotbur != null)
            excarrotbur.Subscription(PlayLevel);
    }

    /// <summary>
    /// Update method.
    /// If the player is not null and his life is under or equals 0, we launch the gameover
    /// </summary>
    void Update()
    {
        if (player != null)
            if (player.Life <= 0)
                GameOver();          
    }

    /// <summary>
    /// Launch the first level
    /// </summary>
    private void PlayLevel1()
    {
        SceneManager.LoadScene(Constants.LEVEL1_SCENE_NAME);
    }

    /// <summary>
    /// Play the second level if the player is currently in the first level otherwise launch the victory menu.
    /// </summary>
    public void PlayLevel()
    {
        if (SceneManager.GetActiveScene().name == Constants.LEVEL1_SCENE_NAME)
            PlayLevel2();
        else if (SceneManager.GetActiveScene().name == Constants.LEVEL2_SCENE_NAME)
            Victory();
    }

    /// <summary>
    /// Launch the level 2
    /// </summary>
    private void PlayLevel2()
    {
        SceneManager.LoadScene(Constants.LEVEL2_SCENE_NAME);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Launch the game over.
    /// </summary>
    public void GameOver()
    {
        SceneManager.LoadScene(Constants.GAME_OVER_SCENE_NAME);
    }
    /// <summary>
    /// Launch the victory.
    /// </summary>
    public void Victory()
    {
        SceneManager.LoadScene(Constants.VICTORY_SCENE_NAME);
    }
}
