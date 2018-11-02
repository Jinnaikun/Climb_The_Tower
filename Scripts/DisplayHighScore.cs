using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScore : MonoBehaviour {

    public Text highscoreText; //Holds highscore text


    void Start()
    {
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("PlayerHighscore");  
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("PlayerHighscore");
    }
}
