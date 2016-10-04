using UnityEngine;
using System.Collections;

public class poisonCollider : MonoBehaviour {
	
	public GameObject model;
	public GameObject effect;

	void OnTriggerEnter(Collider Player){
		if (Player.CompareTag ("player")) {
			Bouncy stat = Player.GetComponent<PlayerMove> ().bounce;
			if (stat == Bouncy.Down || stat == Bouncy.Up || stat == Bouncy.stun) {
				model.SetActive (false);
				effect.SetActive (true);
				this.gameObject.SetActive (false);
			}
		}
	}


}
