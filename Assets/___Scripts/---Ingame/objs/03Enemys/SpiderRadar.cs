using UnityEngine;
using System.Collections;

public class SpiderRadar : MonoBehaviour {
	public GameObject Body;


	void OnTriggerEnter(Collider player) {
		if (player.CompareTag ("player")) {
			Body.GetComponent<Spider> ().radarCheck = true;
		}
	}

}