using UnityEngine;
using System.Collections;

public class UI_Cage : MonoBehaviour {

	public GameObject ingameCamera;




	void Start () {
		ingameCamera = GameObject.Find ("Main Camera");

	}

}
