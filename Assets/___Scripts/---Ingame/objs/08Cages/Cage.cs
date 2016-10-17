using UnityEngine;
using System.Collections;

public class Cage : MonoBehaviour {

	public GameObject standCage;
	public GameObject breakCage;

	public AudioClip breakSound;

	GameObject gameManager;

	void Start(){
		if (Application.loadedLevelName != "Edit") {
			gameManager = GameObject.Find ("GameManager");
			gameManager.GetComponent<GameManager> ().closeHelp [GameManager.Record_help_Max].SetActive (true);
			GameManager.Record_help_Max += 1;
		}
	}

	void OnTriggerEnter(Collider player) {
		if (player.CompareTag ("player")) {
			if(player.GetComponent<PlayerMove>().bounce == Bouncy.Down){
				AudioSource.PlayClipAtPoint (breakSound, player.GetComponent<PlayerMove> ().Camera_ingame.transform.position, GameManager.soundVolume);
				breakCage.SetActive (true);
				standCage.SetActive (false);
				gameManager.GetComponent<GameManager> ().closeHelp[GameManager.Record_help].SetActive(false);
				gameManager.GetComponent<GameManager> ().openHelp[GameManager.Record_help].SetActive(true);
				GameManager.Record_help += 1;
			}
		}
	}


}
