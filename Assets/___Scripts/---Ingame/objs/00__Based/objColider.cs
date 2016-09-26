using UnityEngine;
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
	public GameObject plantModel;
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
			plant_ani.SetInteger("hp",(int)standhp);
			break;
		
		}

		if (Application.loadedLevelName == "Edit") {
			StartCoroutine ("editCollider");
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
					yeon_ani.SetTrigger("reset");
					yeon_ani.SetInteger("hp",(int)standhp);
					GetComponent<BoxCollider> ().enabled = true;
					break;

				case "2000":
					break;

				case "1030031": //plant
					//plant_ani.SetTrigger ("reset");
					plant_ani.SetInteger ("hp", (int)standhp);
					plantModel_dead.SetActive (false);
					plantModel.SetActive (true);
					plantDeadPoint.GetComponent<BoxCollider> ().enabled = true;
					GetComponent<BoxCollider> ().enabled = true;
					plant_ani.ResetTrigger ("reset");
					break;

				case "1040021": //croc
					croc_ani.SetTrigger("reset");
					croc_ani.SetInteger("hp",(int)standhp);
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

			switch (transform.parent.name.Substring (0, 4)) {

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
			case "1040021":
				transform.parent.gameObject.SetActive (false);
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
				AudioSource.PlayClipAtPoint (crocSound, Camera_ingame.transform.position);
				croc_ani.SetTrigger ("attack");
				crocAngry.SetActive (true);
			}
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
			standhp--;
			yeon_ani.SetInteger ("hp", (int)standhp);
			if (standhp <= 0) {
				GetComponent<BoxCollider> ().enabled = false;
			}
		}
	}

	void plant(){
		if (standhp > 0) {
			standhp--;
			if (standhp <= 0) {
				plantModel.SetActive (false);
				plantModel_dead.SetActive (true);
				
				//plant_ani.SetInteger ("hp", (int)standhp);
				//plant_ani.SetTrigger ("break");
				plantDeadPoint.GetComponent<BoxCollider> ().enabled = false;
				GetComponent<BoxCollider> ().enabled = false;
			}
		}
	}
}
