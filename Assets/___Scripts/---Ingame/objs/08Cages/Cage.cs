using UnityEngine;
using System.Collections;

public class Cage : MonoBehaviour {

	public GameObject standCage;
	public GameObject breakCage;

	public AudioClip breakSound;

	public GameObject UI_Cage_open;
	public GameObject UI_Cage_close;

	void Start(){
		GameManager.Record_help_Max += 1;
	}

	void OnTriggerEnter(Collider player) {
		if (player.CompareTag ("player")) {
			if(player.GetComponent<PlayerMove>().bounce == Bouncy.Down){
				AudioSource.PlayClipAtPoint (breakSound, player.GetComponent<PlayerMove> ().Camera_ingame.transform.position, GameManager.soundVolume);
				breakCage.SetActive (true);
				standCage.SetActive (false);
				GameManager.Record_help += 1;
				UI_Cage_close.SetActive (false);
				UI_Cage_open.SetActive (true);
			}
		}
	}


}
