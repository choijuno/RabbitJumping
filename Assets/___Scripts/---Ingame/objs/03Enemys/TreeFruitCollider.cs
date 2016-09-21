using UnityEngine;
using System.Collections;

public class TreeFruitCollider : MonoBehaviour {

	void Start(){
		transform.parent = null;
	}

	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("player")) {
			this.gameObject.SetActive (false);

		}

		if (player.CompareTag ("ground")) {
			this.gameObject.SetActive (false);

		}

	}
}
