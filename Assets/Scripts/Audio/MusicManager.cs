using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class MusicManager : MonoBehaviour
{
    [Header("AudioSources")]
    [SerializeField]
    private AudioSource aSource1;
    [SerializeField]
    private AudioSource aSource2;
    [SerializeField]
    private AudioSource currentASource;

    [Header("Music Clips")]
    [SerializeField]
    private AudioClip noConflictMX;
    [SerializeField]
    private AudioClip chasedMX;
    [SerializeField]
    private AudioClip gameOverMX;
    [SerializeField]
    private AudioClip youWinMX;
    [Header("Stingers")]
    [SerializeField]
    private AudioClip calmToChasedStinger;
    [SerializeField]
    private AudioClip chasedToCalmStinger;

    [SerializeField]
    private AudioClip currentMusic;
    public bool currentMusicChange = false;

    [SerializeField]
    private float fadeTime;

    public bool source1Fading = false;
    public bool source2Fading = false;

    public GameObject gameOverObject;
    public GameObject winObject;

    public enum State
    {
        Explore,
        Chasing,
        Win,
        Lose,
    }
    public State musicState;

    private void Start()
    {
        currentASource = aSource1;
        currentMusic = noConflictMX;
        PlayMusic();
    }

    public void PlayMusic()
    {
            if (currentASource == aSource1) // this means it will start on source 2
            {
                currentASource = aSource2;
                StartFadeOutSource1();
            } //if current source is 1 go to 2 at fade speed

            else if (currentASource == aSource2)
            {
                currentASource = aSource1;
                StartFadeOutSource2();
            } // if current source is 2 got o 1 at fade speed

            currentASource.clip = currentMusic; // sets clip to currentMusic
            currentASource.Play();
    }
    public void StartFadeOutSource1() // bool flipper 
    {
        source1Fading = true;
    }
    public void StartFadeOutSource2()// bool flipper
    {
        source2Fading = true;
    }

    public void PlayGameOverMX()
    {
        aSource1.volume = 0;
        aSource2.volume = 0;

        gameOverObject.SetActive(true);
    }

    public void PlayWinMX()
    {
        aSource1.volume = 0;
        aSource2.volume = 0;

        winObject.SetActive(true);
    }

    private void Update()
    {
        switch(musicState)
        {
            case State.Explore:
                currentMusic = noConflictMX;
                break;
            case State.Chasing:
                currentMusic = chasedMX;
                break;
        }

        if(currentMusicChange == true)
        {
            PlayMusic();
            currentMusicChange = false;
        }

        if (source1Fading == true) // if the first source is fading is true
        {
            aSource1.volume -= fadeTime * Time.deltaTime; // fade out. 
            if (aSource1.volume <= 0) // when olume reaches 0
            {
                source1Fading = false; //stop fading
            }

            currentASource.volume += fadeTime * Time.deltaTime; // current source is now 2, so fade in
        }

        if (source2Fading == true)
        {
            aSource2.volume -= fadeTime * Time.deltaTime;
            if (aSource2.volume <= 0)
            {
                source2Fading = false;
            }
            currentASource.volume += fadeTime * Time.deltaTime;
        }
    }

}
