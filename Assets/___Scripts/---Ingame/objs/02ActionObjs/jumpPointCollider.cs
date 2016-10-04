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
			if(player.GetComponent<PlayerMove>().bounce == Bouncy.Down)
				_anim.SetTrigger ("jump");
		}
	}
}
