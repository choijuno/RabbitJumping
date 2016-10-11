using UnityEngine;
using System.Collections;

public class poisonCollider : MonoBehaviour {
	BoxCollider mybody;
	public GameObject model;
	public GameObject effect;

	void Start(){
		mybody = GetComponent<BoxCollider> ();
		Invoke ("resetTime", 2f);
	}

	void resetTime(){
		if (GameManager.retry_Check) {

			StartCoroutine ("resetCheck");
		}
	}

	IEnumerator resetCheck(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (GameManager.retry_count >= 1) {
				model.SetActive (true);
				effect.SetActive (false);
				mybody.enabled = true;
				StopCoroutine ("resetCheck");
			}

		}
	}

	void OnTriggerEnter(Collider Player){
		if (Player.CompareTag ("player")) {
			Bouncy stat = Player.GetComponent<PlayerMove> ().bounce;
			if (stat == Bouncy.Down || stat == Bouncy.Up || stat == Bouncy.stun) {
				model.SetActive (false);
				effect.SetActive (true);
				mybody.enabled = false;
			}
		}
	}
}