using UnityEngine;
using System.Collections;

public class Cage : MonoBehaviour {

	public GameObject standCage;
	public GameObject breakCage;

	public AudioClip breakSound;

	void Start(){
		GameManager.Record_help_Max += 1;
	}

	void OnTriggerEnter(Collider player) {
		if (player.CompareTag ("player")) {
			AudioSource.PlayClipAtPoint (breakSound, player.GetComponent<PlayerMove> ().Camera_ingame.transform.position);
			breakCage.SetActive (true);
			standCage.SetActive (false);
			GameManager.Record_help += 1;
		}
	}
}
