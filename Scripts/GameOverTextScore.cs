using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTextScore : MonoBehaviour {

    public Text gameoverScore; //Displays current score after death
    PlayerPrefManager playerPrefManager; //Grabs your current score


    public void UpdateGameOverText()
    {
        gameoverScore.text = "Your Score: " + playerPrefManager.GetPlayerCurrentScore();
    }
    
}
