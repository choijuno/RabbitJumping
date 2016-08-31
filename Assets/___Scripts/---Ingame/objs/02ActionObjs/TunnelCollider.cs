using UnityEngine;
using System.Collections;

public class TunnelCollider : MonoBehaviour {

	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("player")) {

		}
	}
}
