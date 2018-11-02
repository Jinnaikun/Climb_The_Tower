using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {

    AudioSource music; //Holds the audio source that plays music

    public AudioClip[] songList; //Holds the list of songs to play

    private void Awake()
    {
        //Fill references
        music = gameObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// All functions from this script play a song according to the current level.
    /// Each level will have a song player that will call the function with the song it needs.
    /// </summary>

    public void PlayMainMenuMusic()
    {
        music.clip = songList[0];
        music.Play();
    }

    public void PlayOverworldMusic()
    {
        music.clip = songList[1];
        music.Play();
    }

    public void PlayBattleMusic()
    {
        music.clip = songList[2];
        music.Play();
    }

    public void PlayBossMusic()
    {
        music.clip = songList[3];
        music.Play();
    }

    public void PlayWinMusic()
    {
        music.clip = songList[4];
        music.Play();
    }

    public void PlayHealMusic()
    {
        music.clip = songList[5];
        music.Play();
    }

}
