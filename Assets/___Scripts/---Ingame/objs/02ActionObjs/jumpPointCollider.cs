using UnityEngine;
using System.Collections;

public class jumpPointCollider : MonoBehaviour {

	public GameObject mybody;
	public Animator _anim;

	void Start() {
		_anim = mybody.GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider player) {
		if (player.CompareTag ("player")) {
			_anim.SetTrigger ("jump");
		}
	}
}
