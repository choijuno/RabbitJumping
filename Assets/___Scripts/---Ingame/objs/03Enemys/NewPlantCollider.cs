using UnityEngine;
using System.Collections;

public class NewPlantCollider : MonoBehaviour {
	public GameObject Camera_ingame;
	public Animator plant_ani;
	public GameObject plantDeadPoint;
	public GameObject plantModel;
	public GameObject plantModel_dead;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider player){
		if (player.CompareTag ("player")) {
			Camera_ingame = player.GetComponent<PlayerMove> ().Camera_ingame;
			Camera_ingame.GetComponent<GameCamera> ().viveCheck = true;
			plant ();
		}
	}

	void plant(){

	}
}
