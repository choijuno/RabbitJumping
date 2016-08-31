using UnityEngine;
using System.Collections;

public class clearCollider : MonoBehaviour {
	public GameObject ClearEffect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("player")) {
			ClearEffect.SetActive (true);
		}
	}
}
