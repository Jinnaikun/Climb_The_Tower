using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour {

    [SerializeField] int playerHighScore; //Player's high score.
    [SerializeField] int playerCurrentScore; //Player's current score.


    public static PlayerPrefManager manager;

     void Awake()
    {
        //Keep THIS instance of the manager

        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadScoreData();

        //Player starts with a highscore of zero
        if(PlayerPrefs.GetInt("PlayerHighscore") <= 0)
        {
            PlayerPrefs.SetInt("PlayerHighscore", 0);
        }
    }

    //Records the player's highscore
    public void Highscore()
    {
        //If score is higher than previous, save highscore

        if(playerCurrentScore > PlayerPrefs.GetInt("PlayerHighscore"))
        {
            PlayerPrefs.SetInt("PlayerHighscore", playerCurrentScore);
        }
    }

    //Loads in the player's highscore
    void LoadScoreData()
    {
        playerHighScore = PlayerPrefs.GetInt("PlayerHighscore");
    }

    //Delete player data
    public void DeleteScoreData()
    {
        PlayerPrefs.DeleteAll(); //Delete everything because there's only one thing.
    }

    //Sets their current score to 0
    public void ResetCurrentScore()
    {
        playerCurrentScore = 0;
    }

    //Updates player's current score as they get points
    public void CurrentScore(int addScore)
    {
        playerCurrentScore += addScore;
    }

    //This sends the player's current score back to whatever function needs it, without directly accessing the variable/ a getter
    public int GetPlayerCurrentScore()
    {
        return playerCurrentScore;
    }
    
}
