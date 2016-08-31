using UnityEngine;
using System.Collections;

public class PlantDeadPoint : MonoBehaviour {
	public Animator plantAni;


	void OnTriggerEnter(Collider player){
		
		if (player.CompareTag ("player")) {
			
			plantAni.SetTrigger ("attack");
		}
	}
}
