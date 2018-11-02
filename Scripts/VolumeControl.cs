using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeControl : MonoBehaviour {

    public AudioSource music; //Music audio source
    public GameObject volumeSliderObject;//Object that has the volume slider
    Slider volumeSlider; //Actual volume slider component
    

    private void Awake()
    {
        //Fill references
        music = FindObjectOfType<AudioSource>();
        volumeSlider = volumeSliderObject.GetComponent<Slider>();
    }

    private void Start()
    {
        //Change volume value to whatever it is currently at
        volumeSlider.value = music.volume;
    }

    //Changes the volume based on the slider's position
    public void VolumeChanger(float volumeAmount)
    {
        music.volume = volumeAmount;
    }
}
