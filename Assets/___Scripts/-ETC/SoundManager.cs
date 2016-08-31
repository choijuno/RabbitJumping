using UnityEngine;
using System.Collections;

//AudioSource.PlayClipAtPoint ('public AudioClip 변수;', transform.position);

public class SoundManager : MonoBehaviour {
	public Transform Camera;


	public static int soundNum;
	public static bool soundOn=false;

	public AudioClip Win;
	public AudioClip Win2;
	public AudioClip Lose;
	public AudioClip batting;
	public AudioClip cardSet;
	public AudioClip cardTurn;
	public AudioClip button;



	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (soundOn) {
			switch (soundNum) {
			case 0:
				break;
			case 1:
				AudioSource.PlayClipAtPoint (Win, Camera.transform.position);
				AudioSource.PlayClipAtPoint (Win2, Camera.transform.position);
				break;
			case 2:
				AudioSource.PlayClipAtPoint (Lose, Camera.transform.position);
				break;
			case 3:
				AudioSource.PlayClipAtPoint (batting, Camera.transform.position);
				break;
			case 4:
				AudioSource.PlayClipAtPoint (cardSet, Camera.transform.position);
				break;
			case 5:
				AudioSource.PlayClipAtPoint (cardTurn, Camera.transform.position);
				break;
			case 6:
				AudioSource.PlayClipAtPoint (button, Camera.transform.position);
				break;
			}
			soundNum = 0;
			soundOn = false;
		}
	}

	public static void Sound(int i) {
		soundNum = i;
		soundOn = true;
	}
}
