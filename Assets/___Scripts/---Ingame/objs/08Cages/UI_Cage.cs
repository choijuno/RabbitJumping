using UnityEngine;
using System.Collections;

public class UI_Cage : MonoBehaviour {

	public GameObject ingameCamera;
	public GameObject model;



	void Start () {
		if (Application.loadedLevelName == "TestGame") {
			ingameCamera = GameObject.Find ("Main Camera");
			PosSet ();
		}


	}

	void PosSet() {
		transform.parent = null;
		transform.parent = ingameCamera.GetComponent<GameCamera> ().UI_Cage_pos [GameManager.UI_help_index].transform;
		GameManager.UI_help_index++;
		transform.localPosition = new Vector3 (0, 0, 0);
		Invoke ("hideoff", 1f);
	}

	void hideoff() {
		model.SetActive (true);
	}
}
