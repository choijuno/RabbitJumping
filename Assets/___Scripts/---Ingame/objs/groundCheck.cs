using UnityEngine;
using System.Collections;

public class groundCheck : MonoBehaviour {
	public Transform thisChild;

	void Start(){
		
	}

	void OnTriggerEnter(Collider ground){

		if (Application.loadedLevelName != "Edit") {
			if (ground.transform.parent.name.Substring (0, 3) == "101" && !ground.CompareTag("turnpoint")) {
				thisChild.transform.parent = ground.GetComponent<BumpColider> ().groundParent.transform;
				this.gameObject.SetActive (false);
			}

		}
	}
}
