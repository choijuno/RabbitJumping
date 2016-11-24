using UnityEngine;
using System.Collections;

public class hiveCheck : MonoBehaviour {
	public Transform thisChild;

	void Start(){
		
	}

	void OnTriggerEnter(Collider ground){

		if (Application.loadedLevelName != "Edit") {
			if (ground.transform.parent.name.Substring (0, 7) == "1060021") {
				ground.transform.parent.transform.parent = transform.parent.transform;
				this.gameObject.SetActive (false);
			}

		}
	}
}
