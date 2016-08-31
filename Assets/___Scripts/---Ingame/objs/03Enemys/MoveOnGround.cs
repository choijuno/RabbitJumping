using UnityEngine;
using System.Collections;

public class MoveOnGround : MonoBehaviour {

	public Animator thisAni;

	public MovePosition move;

	Transform image;
	public float waitTime;
	float waitTime_in;
	public float Speed;
	float Speed_in;

	// Use this for initialization
	void Start () {
		waitTime_in = waitTime;
		Speed_in = Speed * 0.001f;

		if (this.gameObject.name.Substring (0, 7) == "1040012") {
			if (move == MovePosition.Left) {
				thisAni.SetTrigger ("move");
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		switch (move) {
		case MovePosition.Left:
			transform.position = new Vector3 (transform.position.x - Speed_in, transform.position.y, transform.position.z);
			break;
		case MovePosition.Right:
			transform.position = new Vector3 (transform.position.x + Speed_in, transform.position.y, transform.position.z);			
			break;
		case MovePosition.Stay:
			
			break;
		}
	}



}
