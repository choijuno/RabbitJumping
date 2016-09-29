using UnityEngine;
using System.Collections;

public class TreeFruitCollider : MonoBehaviour {

	public float stunTime;

	void Start(){
		stunTime = transform.parent.GetComponent<Tree_R> ().stunTime;
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
