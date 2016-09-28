using UnityEngine;
using System.Collections;

public class colliderTurnpoint : MonoBehaviour {
	public GameObject myparent;
	public MovePosition move;
	public Animator thisAni;
	// Use this for initialization
	void Start () {
		//move = transform.parent.GetComponent<MoveOnGround> ().move;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider turnpoint){
		//Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

		if(turnpoint.CompareTag("turnpoint")){
			Debug.Log ("!!!!");
			if (myparent.GetComponent<MoveOnGround> ().move == MovePosition.Left) {
				myparent.GetComponent<MoveOnGround> ().move = MovePosition.Right;
			} else {
				myparent.GetComponent<MoveOnGround> ().move = MovePosition.Left;
			}

		}


		if (turnpoint.CompareTag ("player")) {
			thisAni.SetTrigger ("attack");
		}
	}
}
