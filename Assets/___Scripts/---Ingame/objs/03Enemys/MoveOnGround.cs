using UnityEngine;
using System.Collections;

public class MoveOnGround : MonoBehaviour {


	public bool Stop;


	public Animator thisAni;

	public MovePosition move;
	MovePosition baseMove;

	public GameObject hedgehog_model;

	Transform image;
	public float waitTime;
	float waitTime_in;
	public float Speed;
	float Speed_in;

	// Use this for initialization
	void Start () {
		baseMove = move;

		if (Application.loadedLevelName == "Edit") {
			Stop = true;
		}


		waitTime_in = waitTime;
		Speed_in = Speed * 0.001f;

		if (this.gameObject.name.Substring (0, 7) == "1040012") {
			if (move == MovePosition.Left) {
				thisAni.SetTrigger ("move");
			}
		}
			
		StartCoroutine ("MoveReset");

	}



	IEnumerator MoveReset(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			hedgehog_model.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			move = baseMove;

			if (!Stop) {
				StartCoroutine ("MoveStart");
				StopCoroutine ("MoveReset");
			}

		}
	}

	IEnumerator MoveStart(){
		while (true) {
			yield return new WaitForSeconds (0.006f);


			switch (move) {
			case MovePosition.Left:
				hedgehog_model.transform.position = new Vector3 (hedgehog_model.transform.position.x - Speed_in, hedgehog_model.transform.position.y, hedgehog_model.transform.position.z);
				break;
			case MovePosition.Right:
				hedgehog_model.transform.position = new Vector3 (hedgehog_model.transform.position.x + Speed_in, hedgehog_model.transform.position.y, hedgehog_model.transform.position.z);			
				break;
			case MovePosition.Stay:

				break;
			}


			if (Stop) {
				StartCoroutine ("MoveReset");
				StopCoroutine ("MoveStart");
			}
		}
	}



}
