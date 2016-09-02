using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public static MusicManager instance;

    AudioSource audioSource;

    public AudioClip mainScene;
    public AudioClip selectScene;
    public AudioClip gameScene;
	void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            audioSource = this.GetComponent<AudioSource>();
            if (!ES2.Exists("musicChk"))
                ES2.Save<bool>(true, "musicChk");
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }
    public void MusicSelect(bool musicChk)
    {
        if (musicChk)
        {
            ES2.Save<bool>(musicChk, "musicChk");
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                audioSource.clip = mainScene;
            }else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                audioSource.clip = selectScene;
            }else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                audioSource.clip = gameScene;
            }
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                audioSource.clip = null;
                audioSource.Stop();
                audioSource.loop = false;
            }
            else
            {
                ES2.Save<bool>(musicChk, "musicChk");
                audioSource.clip = null;
                audioSource.Stop();
                audioSource.loop = false;
            }
        }
    }
}
