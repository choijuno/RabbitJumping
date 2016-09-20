using UnityEngine;
using System.Collections;

public class clearCollider : MonoBehaviour {
	public GameObject ClearEffect;

	IEnumerator Clear(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (GameManager.gameSet == 1) {
				ClearEffect.SetActive (true);
				StopCoroutine ("Clear");
			}
		}
	}


	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("player")) {
			StartCoroutine ("Clear");
		}
	}
}
