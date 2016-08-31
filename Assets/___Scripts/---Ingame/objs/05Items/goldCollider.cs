using UnityEngine;
using System.Collections;

public class goldCollider : MonoBehaviour {

	public GameObject model;
	public GameObject modelDestroy;


	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("player")) {
			model.SetActive (false);
			modelDestroy.SetActive (true);
			this.GetComponent<BoxCollider> ().enabled = false;
		}

	}
}
