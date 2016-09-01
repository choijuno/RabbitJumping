﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public static MusicManager instance;

    AudioSource audioSource;

    public AudioClip mainScene;
    public AudioClip selectScene;
    public AudioClip gameScene;
	void Start ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }

        audioSource = this.GetComponent<AudioSource>();
        soundStart();
    }
    void soundStart()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
           audioSource.clip = mainScene;
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            audioSource.clip = selectScene;
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            audioSource.clip = gameScene;
        }
        audioSource.Play();
        audioSource.loop = true;
    }
    public void MusicSelect(bool musicChk)
    {
        if (musicChk)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                audioSource.clip = mainScene;
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                audioSource.clip = selectScene;
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                audioSource.clip = gameScene;
            }
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = null;
            audioSource.Stop();
            audioSource.loop = false;
        }
    }
}
