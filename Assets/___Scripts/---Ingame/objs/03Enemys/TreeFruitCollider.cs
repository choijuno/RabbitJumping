using UnityEngine;
using System.Collections;

public class TreeFruitCollider : MonoBehaviour {

	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("player")) {
			this.gameObject.SetActive (false);

		}

		if (player.CompareTag ("ground")) {
			this.gameObject.SetActive (false);

		}

	}
}
