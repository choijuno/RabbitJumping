using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	
	// player position
	PlayerController playerController;
	MovePosition playerState;
	public float MoveSpeed;
	float movespeed;

	public GameObject midleBackGround;
	public Transform playerPosition;
	public float CameraCenterPosition;

	public float waitTime_in;
	public float rideSpeed_in;
	public bool riding;
	public int direction;

	void Start () {
		transform.position = new Vector3 (playerPosition.transform.position.x + CameraCenterPosition, 0, -10);
		movespeed = MoveSpeed;
	}


	void Update () {
		
		// camera move(lerp)
		transform.position = new Vector3 (Mathf.Lerp(transform.position.x,playerPosition.transform.position.x + CameraCenterPosition ,0.1f), transform.position.y, transform.position.z);

		if (GameManager.tiltCheck) {
			if (Input.acceleration.x < -0.08f)
				playerState = MovePosition.Left;
			if (Input.acceleration.x > 0.08f)
				playerState = MovePosition.Right;
			if (Input.acceleration.x >= -0.079999f && Input.acceleration.x <= 0.079999f) {
				playerState = MovePosition.Stay;
			}
		} else {
			
			if (Input.GetMouseButton (0)) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) {

					if (hit.collider.gameObject.CompareTag ("left")) {
						playerState = MovePosition.Left;
					}
					if (hit.collider.gameObject.CompareTag ("right")) {
						playerState = MovePosition.Right;
					}

				}

			} else {
				playerState = MovePosition.Stay;
			}
		}




		switch (playerState) {
		case MovePosition.Stay:
			movespeed = 0;
			break;
		case MovePosition.Left:
			movespeed = Mathf.Abs(MoveSpeed);
			break;
		case MovePosition.Right:
			movespeed = -MoveSpeed;
			break;
		}
		if (GameManager.gameSet == 0) {
			if (!riding) {
				
				if (direction != 0) {
					midleBackGround.transform.position = new Vector3 (midleBackGround.transform.position.x + movespeed * 0.001f, midleBackGround.transform.position.y, midleBackGround.transform.position.z);
				}

			} else {
				waitTime_in = waitTime_in - Time.deltaTime;
				if (waitTime_in <= 0) {
					switch (direction) {
					case 0: //stop
						
						break;
					case 1: //left
						midleBackGround.transform.position = new Vector3 (midleBackGround.transform.position.x + rideSpeed_in * 0.1f, midleBackGround.transform.position.y, midleBackGround.transform.position.z);
						break;
					case 2: //right
						midleBackGround.transform.position = new Vector3 (midleBackGround.transform.position.x - rideSpeed_in * 0.1f, midleBackGround.transform.position.y, midleBackGround.transform.position.z);
						break;
					}
				}
			}
		}
	}


}
