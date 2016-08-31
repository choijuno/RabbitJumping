using UnityEngine;
using System.Collections;

public class FishCollider : MonoBehaviour {

	public Animator fishAni;


	void OnTriggerEnter(Collider player){

		if (player.CompareTag ("player")) {

			fishAni.SetTrigger ("attack");
		}
	}
}
