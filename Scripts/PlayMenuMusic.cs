using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuMusic : MonoBehaviour {

    PlayMusic playMusic; //Will be used to access song list and play a song.

    private void Awake()
    {
        //Fill reference
        playMusic = FindObjectOfType<PlayMusic>();
    }

    private void Start()
    {
        //Play song on start
        playMusic.PlayMainMenuMusic();
    }
}
