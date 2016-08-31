using UnityEngine;
using System.Collections;

public class TunnelExitCollider : MonoBehaviour {

	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("player")) {

		}
	}
}
