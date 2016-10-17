using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {
    public static MusicManager instance;

    AudioSource audioSource;

    public AudioClip mainScene;
    public AudioClip[] selectScene;
    public AudioClip playOneShot;
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
    public void PlayOnShot()
    {
        if(ES2.Load<bool>("HyoGwaSound"))
            AudioSource.PlayClipAtPoint(playOneShot, transform.position);
    }
    public void MusicSelect(bool musicChk)
    {
        if (musicChk)
        {
            ES2.Save<bool>(musicChk, "musicChk");
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                audioSource.volume = 0.7f;
                audioSource.clip = mainScene;
            }else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                audioSource.volume = 0.7f;
                audioSource.clip = selectScene[Random.Range(0,6)];
            }else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                audioSource.volume = 1.0f;
                audioSource.clip = gameScene;
            }
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            if(SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 6)
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
