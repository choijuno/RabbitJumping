using UnityEngine;
using System.Collections;

public class groundCheck : MonoBehaviour {
	public Transform thisChild;


	void OnTriggerEnter(Collider ground){
		if (ground.transform.parent.name.Substring (0, 3) == "101") {
			thisChild.transform.parent = ground.GetComponent<BumpColider> ().groundParent.transform;
			this.gameObject.SetActive (false);
		}
	}
}
