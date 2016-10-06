using UnityEngine;
using System.Collections;

public class NewPlantDeadPoint : MonoBehaviour {
	public Animator plantAni;
	public GameObject deadPoint;



	void OnTriggerEnter(Collider player){
		
		if (player.CompareTag ("player")) {

			plantAni.SetTrigger ("attack");

			Invoke ("rest", 0.15f);

		}
	}

	void rest() {
		plantAni.ResetTrigger ("attack");
		deadPoint.SetActive (false);
		Invoke ("reset", 3f);
	}

	void reset() {
		deadPoint.SetActive (true);
	}
}
