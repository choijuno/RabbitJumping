﻿using UnityEngine;
using System.Collections;

public class objColider : MonoBehaviour {
	public GameObject Camera_ingame;





	public bool reset;
	public float standhp;
	public float angryhp;

	public Animator croc_ani;
	public GameObject crocAngry;
	public AudioClip crocSound;



	public Animator yeon_ani;
	public GameObject yeon_collider;
	public AudioClip yeonSound;


	public GameObject yeon1;
	public GameObject yeon2;
	public GameObject yeon3;
	public GameObject yeonCollider;

	public Animator plant_ani;
	public GameObject plantDeadPoint;
	BoxCollider plant_deadzone_box;
	public GameObject plantModel;
	BoxCollider plant_model_box;
	public GameObject plantModel_dead;


	// Use this for initialization
	void Awake () {
		
		standhp = transform.parent.GetComponent<objMovement> ().standHp;
		angryhp = transform.parent.GetComponent<objMovement> ().angryHp;
		switch (transform.parent.name.Substring (0, 7)) {
		case "1010021": //yeon
			yeon_ani.SetInteger("hp",(int)standhp);
			break;
		case "1040021": //croc
			croc_ani.SetInteger("hp",(int)standhp);
			break;
		case "1030031": //plant
			plant_deadzone_box = plantDeadPoint.GetComponent<BoxCollider>();
			plant_model_box = GetComponent<BoxCollider>();
			break;
		
		}

		if (Application.loadedLevelName == "Edit") {
			StartCoroutine ("editCollider");
		} else {
			StartCoroutine ("editCollider");
			//StartCoroutine ("resetAll");
		}

	}


	IEnumerator resetAll(){



		while (true) {
			yield return new WaitForSeconds (0.06f);


		}

	}

	IEnumerator editCollider(){
		while (true) {
			yield return new WaitForSeconds (0.06f);

			if (reset) {
				Debug.Log ("reset");
				switch (transform.parent.name.Substring (0, 7)) {
				case "Plan":
					
					break;
				case "1000":
					break;
				case "1001":
					break;

				case "1010021": //yeon
					Debug.Log ("resetYeon");
					yeon_ani.ResetTrigger("hit");
					yeon_ani.SetInteger("hp",(int)standhp);
					yeon_ani.SetTrigger("reset");
					GetComponent<BoxCollider> ().enabled = true;
					break;

				case "2000":
					break;

				case "1030031": //plant
				case "1030032":
				case "1030033":
					//plant_ani.SetTrigger ("reset");
					plantModel_dead.SetActive (false);
					plantModel.SetActive (true);
					plant_deadzone_box.enabled = true;
					plant_model_box.enabled = true;
					plant_ani.ResetTrigger ("reset");
					break;

				case "1040021": //croc
					standhp = transform.parent.GetComponent<objMovement> ().standHp;
					croc_ani.SetInteger("hp",(int)standhp);
					croc_ani.SetTrigger("reset");
					crocAngry.SetActive (false);
					break;
				}
				standhp = transform.parent.GetComponent<objMovement> ().standHp;
				angryhp = transform.parent.GetComponent<objMovement> ().angryHp;
				reset = false;
			}

		}
	}

	void OnTriggerEnter (Collider player){
		if (player.CompareTag ("ride_elephant")) {

			switch (transform.parent.name.Substring (0, 7)) {

			case "1990011":
				break;

				//01Ground
			case "1010011":
				break;
			case "1010012":
				break;
			case "1010021":
				break;
			case "1010031":
				break;

				//02ActionObj
			case "1020011":

				break;

				//03Stayenemy
			case "1030011":
				transform.parent.gameObject.SetActive (false);
				break;
			case "1030021":
				break;
			case "1030031"://plant
				//transform.parent.gameObject.SetActive (false);
				break;
			case "1030041":
				break;

				//04Moveenemy
			case "1040011":
				break;
			case "1040021": //croc
				//transform.parent.gameObject.SetActive (false);
				Camera_ingame = player.transform.parent.GetComponent<Elephant> ().Camera_ingame;
				croc_ani.SetTrigger ("attack");
				AudioSource.PlayClipAtPoint (crocSound, Camera_ingame.transform.position, GameManager.soundVolume);
				crocAngry.SetActive (false);
				crocAngry.SetActive (true);
				Camera_ingame.GetComponent<GameCamera> ().viveCheck = true;

				break;
			case "1040031":
				break;
			case "1040032":
				break;
			case "1040033":
				break;
				//05Item

				//06Hurddle
			case "1060011":
				break;
			case "1060021":
				break;
			case "1060031":
				break;

				//07Riding
			case "1070011":
				break;
			case "1070021":
				break;

				//08Cage
			case "1080011":
				break;


			case "Plan":
				
				break;
			case "1000":
				break;
			case "1001":
				break;
			case "1010":
				break;
			case "2000":
				break;
			case "2010":
				break;
			default:
				this.enabled = false;
				break;

			}

		}

		if (player.CompareTag ("ride_hawk")) {
			switch (transform.parent.name.Substring (0, 7)) {
			case "1990011":
				break;

				//01Ground
			case "1010011":
				break;
			case "1010012":
				break;
			case "1010021":
				break;
			case "1010031":
				break;

				//02ActionObj
			case "1020011":

				break;

				//03Stayenemy
			case "1030011":
				//transform.parent.gameObject.SetActive (false);
				break;
			case "1030021":
				break;
			case "1030031"://plant
			case "1030032":
			case "1030033":
				//transform.parent.gameObject.SetActive (false);
				Camera_ingame = player.transform.parent.GetComponent<Hawk> ().Camera_ingame;
				Camera_ingame.GetComponent<GameCamera> ().viveCheck = true;
				break;
			case "1030041":
				break;

				//04Moveenemy
			case "1040011":
				break;
			case "1040021": //croc
				//transform.parent.gameObject.SetActive (false);
				Camera_ingame = player.transform.parent.GetComponent<Hawk> ().Camera_ingame;
				croc_ani.SetTrigger ("attack");
				AudioSource.PlayClipAtPoint (crocSound, Camera_ingame.transform.position, GameManager.soundVolume);
				crocAngry.SetActive (false);
				crocAngry.SetActive (true);
				Camera_ingame.GetComponent<GameCamera> ().viveCheck = true;

				break;
			case "1040031":
				break;
			case "1040032":
				break;
			case "1040033":
				break;
				//05Item

				//06Hurddle
			case "1060011":
				break;
			case "1060021":
				break;
			case "1060031":
				break;

				//07Riding
			case "1070011":
				break;
			case "1070021":
				break;

				//08Cage
			case "1080011":
				break;


			case "Plan":

				break;
			case "1000":
				break;
			case "1001":
				break;
			case "1010":
				break;
			case "2000":
				break;
			case "2010":
				break;
			default:
				this.enabled = false;
				break;
			}
		}

	}


	void OnTriggerExit(Collider player){
		if (player.CompareTag ("player")) {

			switch (transform.parent.name.Substring (0, 7)) {

			case "1990011":
				break;

				//01Ground
			case "1010011":
				break;
			case "1010012":
				break;
			case "1010021":
				yeon ();
				break;
			case "1010031":
				break;

				//02ActionObj
			case "1020011":

				break;

				//03Stayenemy
			case "1030011":
				break;
			case "1030021":
				break;
			case "1030031"://plant
			case "1030032":
			case "1030033":
				Camera_ingame = player.GetComponent<PlayerMove> ().Camera_ingame;
				Camera_ingame.GetComponent<GameCamera> ().viveCheck = true;
				plant ();
				break;
			case "1030041":
				break;

				//04Moveenemy
			case "1040011":
				break;
			case "1040021":
				Camera_ingame = player.GetComponent<PlayerMove> ().Camera_ingame;
				croco ();
				break;
			case "1040031":
				break;
			case "1040032":
				break;
			case "1040033":
				break;
				//05Item

				//06Hurddle
			case "1060011":
				break;
			case "1060021":
				break;
			case "1060031":
				break;

				//07Riding
			case "1070011":
				break;
			case "1070021":
				break;

				//08Cage
			case "1080011":
				break;


			case "Plan":
				break;
			case "1000":
				break;
			case "1001":
				break;
			case "1010":
				break;
			case "2000":
				break;
			case "2010":
				break;
			default:
				this.enabled = false;
				break;
			} 
			
				



		}
	}

	void croco(){
		if (standhp > 0) {
			standhp--;
			croc_ani.SetInteger ("hp", (int)standhp);
			if (standhp <= 0) {
				AudioSource.PlayClipAtPoint (crocSound, Camera_ingame.transform.position, GameManager.soundVolume);
				croc_ani.SetTrigger ("attack");
				crocAngry.SetActive (false);
				crocAngry.SetActive (true);
			} /*else {
				AudioSource.PlayClipAtPoint (crocSound, Camera_ingame.transform.position);
				croc_ani.SetTrigger ("attack");
				crocAngry.SetActive (false);
				crocAngry.SetActive (true);
			}*/
			/*
		} else {
			AudioSource.PlayClipAtPoint (crocSound, Camera_ingame.transform.position);
			croc_ani.SetTrigger ("attack");
			crocAngry.SetActive (true);

		}*/
		}
	}

	void yeon() {
		if (standhp > 0) {
			yeon_ani.SetTrigger ("hit");
			standhp--;
			yeon_ani.SetInteger ("hp", (int)standhp);
			if (standhp <= 0) {
				GetComponent<BoxCollider> ().enabled = false;
			}
		}
	}

	void plant(){
				plantModel.SetActive (false);
				plantModel_dead.SetActive (true);
				plantDeadPoint.GetComponent<BoxCollider> ().enabled = false;
				GetComponent<BoxCollider> ().enabled = false;
	}
}
