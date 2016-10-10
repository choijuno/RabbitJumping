using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	public Transform soundPoint;

	// player position
	PlayerController playerController;
	MovePosition playerState;
	public float MoveSpeed;
	float movespeed;

	public GameObject midleBackGround;

	public Transform playerPosition;

	public bool viveCheck;
	Transform endPos;
	Vector3 viveRandom;
	float viveMax;
	float viveNum;
	float viveTime;

	float baseY;

	public float CameraCenterPosition;
	float CameraCenterPosition_in;
	public float waitTime_in;
	public float rideSpeed_in;
	public bool riding;
	public int direction;

	void Start () {
		CameraCenterPosition_in = CameraCenterPosition;
		baseY = 3.77f;

		transform.position = new Vector3 (playerPosition.transform.position.x + CameraCenterPosition, 5f, -10);
		movespeed = MoveSpeed;

		StartCoroutine ("lookCha");
	}


	void Update () {
		
		if (viveCheck) {
			viveMax = 0.4f;
			viveTime = 0.4f;
			StartCoroutine ("vive");
			viveCheck = false;
		}


	}


	IEnumerator lookCha(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			transform.position = new Vector3 (Mathf.Lerp(transform.position.x,playerPosition.transform.position.x + (CameraCenterPosition_in + viveNum), 0.1f),
				Mathf.Lerp(transform.position.y + viveNum, baseY, 0.1f), transform.position.z);

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
							if (!PlayerController.poisonCheck) {
								playerState = MovePosition.Left;
							} else {
								playerState = MovePosition.Right;
							}
						}
						if (hit.collider.gameObject.CompareTag ("right")) {
							if (!PlayerController.poisonCheck) {
								playerState = MovePosition.Right;
							} else {
								playerState = MovePosition.Left;
							}
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
					Debug.Log (direction);
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

	IEnumerator vive(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (viveTime >= 0) {
				viveMax -= Time.deltaTime;
				viveTime -= Time.deltaTime;
				viveNum = Random.Range (-viveMax, viveMax);
			} else {
				viveNum = 0;
				StopCoroutine ("vive");
			}

		}
	}



}
