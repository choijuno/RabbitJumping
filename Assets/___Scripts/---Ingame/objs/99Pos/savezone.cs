using UnityEngine;
using System.Collections;

public class savezone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider player) {
		if (player.CompareTag ("player")) {
			this.gameObject.SetActive (false);
		}
	}
}
