using UnityEngine;
using System.Collections;

public class treeCheck : MonoBehaviour {
	public Transform thisChild;

	void Start(){
		
	}

	void OnTriggerEnter(Collider ground){

		if (Application.loadedLevelName != "Edit") {
			Debug.Log ("!!");
			if (ground.transform.parent.name.Substring (0, 6) == "109001") {
				thisChild.transform.parent = ground.GetComponent<BumpColider> ().groundParent.transform;
				this.gameObject.SetActive (false);
			}

		}
	}
}
