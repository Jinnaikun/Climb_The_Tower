using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {


    [SerializeField] int mainMenuScene = 0; //This is the scene number of the main menu
    PlayerPrefManager playerPrefManager; //Used to reset player's current score
    PathManager pathManager; //Tells the path manager when the player is on the overworld/map
    bool isStart = false; //Checks if you have started the game at any point.

    void Awake()
    {
        //Fill reference
        playerPrefManager = FindObjectOfType<PlayerPrefManager>();
        pathManager = FindObjectOfType<PathManager>();
    }

    //This lets the button, when pressed, start the game by loading up the overworld/character deck screen
    public void StartGame()
    {
        isStart = true;
        int nextScene;
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
        playerPrefManager.ResetCurrentScore(); //Make sure player's score resets when leaving/beating the game.
    }

    public void NextLevel()
    {
        int nextScene;
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }

    public void EndLevel()
    {
        pathManager.isFight = true;
        pathManager.onOverworld = true;
        SceneManager.LoadScene("Overworld");
    }

    //Load high score/settings screen
    public void HighScores()
    {
        SceneManager.LoadScene("HighscoreScene");
    }

    //On the menu, goes to back to the main menu screen
    public void Back()
    {
        if (isStart)
        {
            playerPrefManager.ResetCurrentScore(); //Makes sure player's score resets when leaving/beating the game
            pathManager.ifHeal = false; //Restarting game if you go back to menu, so heals can be used once again.
        }
        SceneManager.LoadScene(mainMenuScene);

    }

    //Loads the win screen
    public void WinScreen()
    {
        playerPrefManager.Highscore(); //Update player highscore.
        SceneManager.LoadScene("YouWin"); 
    }

    //Loads the credits, assuming the credits are the last scene
    public void Credits()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    //Exits the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
