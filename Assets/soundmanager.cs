using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 사운드 매니져
/// 참고 : https://unity3d.com/kr/learn/tutorials/projects/2d-roguelike/audio, http://ree96222.blogspot.kr/2013/08/3d.html
/// </summary>
public class soundmanager : MonoBehaviour {


    public static soundmanager Instance = null;

    /// <summary>
    /// 죽을 때 사운드
    /// </summary>
    public AudioClip Die;
    /// <summary>
    /// 화살 돌릴 때
    /// </summary>
    public AudioClip Arrow;
    /// <summary>
    /// 첫번째 문 내려 올 때 소리
    /// </summary>
    public AudioClip WallUP;
    /// <summary>
    /// 화살 문 내려 올 때 소리
    /// </summary>
    public AudioClip ArrowWall;
    /// <summary>
    /// 첫번 째 구슬 획득 했을 떄
    /// </summary>
    public AudioClip GemGet;
    /// <summary>
    /// 첫번째 구슬 2층 문에서 끼웠을 때
    /// </summary>
    public AudioClip GemSet;
    /// <summary>
    /// 마지막 아이템 먹었을 때
    /// </summary>
    public AudioClip GemGetLast;
    /// <summary>
    /// 벽 밀고 올 때
    /// </summary>
    public AudioClip WallMove;
    /// <summary>
    /// 벽 밀고 올 때 BGM
    /// </summary>
    public AudioClip WallMoveBGM;
    /// <summary>
    /// 2층 들어설 때  BGM
    /// </summary>
    public AudioClip SecondEnterBGM;

    /// <summary>
    /// 게임 성공 했을 떄
    /// </summary>
    public AudioClip Success;

    /// <summary>
    /// 바다속 BGM
    /// </summary>
    public AudioSource BgmWater;
    /// <summary>
    /// 바다속 BGM2
    /// </summary>
    public AudioSource BgmSource;
    /// <summary>
    /// 장소가 다른 곳으로 갔을 때 처리할 AudioSource
    /// </summary>
    public AudioSource BGM;
    /// <summary>
    /// 효과음 처리 할 AudioSource
    /// </summary>
    public AudioSource EFXSource;


    public float LowPitchRange = 0.95f;
    public float HighPictchRange = 1.05f;


    // Use this for initialization
    void Awake () {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {   
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    public void PlaySingle(AudioClip clip)
    {
        EFXSource.clip = clip;
        EFXSource.Play();
    }


    public void PlaySingle(AudioClip clip, Vector3 position, float volume=1)
    {
        //AudioSource.PlayClipAtPoint(clip, position);

        GameObject go = new GameObject("OneShotAudio" + DateTime.Now.ToString("yyyyMMddhhmmssf"));
        go.transform.position = position;
        AudioSource source = go.AddComponent<AudioSource>();
        source.spatialBlend = 1;
        source.clip = clip;
        source.volume = volume;
        if (volume > 1)
        {
            source.minDistance = volume;
        }
        source.Play();
        Destroy(go, clip.length);
    }






    public void PlayBGM(AudioClip clip, bool loop=false)
    {
        BGM.clip = clip;
        BGM.loop = loop;
        BGM.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {

        int randomIndex = UnityEngine.Random.Range(0, clips.Length);
        float randomPitch = UnityEngine.Random.Range(LowPitchRange, HighPictchRange);
        EFXSource.pitch = randomPitch;
        EFXSource.clip = clips[randomIndex];
        EFXSource.Play();
    }
}

