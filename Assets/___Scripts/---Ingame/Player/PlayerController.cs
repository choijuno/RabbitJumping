﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {



	public GameObject deadbody;

	// status use

	public PlayerCC hiveCC = PlayerCC.not;
	public GameObject hive_bees;
	public float hiveTime;
	float hiveTime_in;
	public float hiveHp;
	float hiveHp_in;

	public GameObject poison_mush;
	public float poisonTime;
	float poisonTime_in;
	public static bool poisonCheck;


	MovePosition hiveState = MovePosition.Stay;
	MovePosition baseState = MovePosition.Right;
	public MovePosition playerState = MovePosition.Stay;
	GameSet gameset = GameSet.play;
	PlayerMove playerMove;
	// move, bounce speeds
	public float MoveSpeed = 0f;
	float movespeed;

	public bool moveStopCheck;

	void Start ()
    {
        poisonCheck = false;
    }


	void Update () {
		//Debug.Log (MoveSpeed);



		// raycast hit to gameObject in click point. change to MovePosition(status)
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
							if (!poisonCheck) {
								playerState = MovePosition.Left;
							} else {
								playerState = MovePosition.Right;
							}

						if (baseState == MovePosition.Right) {
							hiveHp_in--;
							baseState = MovePosition.Left;
						}

						}
						if (hit.collider.gameObject.CompareTag ("right")) {
							if (!poisonCheck) {
								playerState = MovePosition.Right;
							} else {
								playerState = MovePosition.Left;
							}

						if (baseState == MovePosition.Left) {
							hiveHp_in--;
							baseState = MovePosition.Right;
						}

						}



					}

				} else {
					playerState = MovePosition.Stay;
				}
			}
		



		// status equal change speed
		if (!GameManager.pauseCheck) {
			switch (playerState) {
			case MovePosition.Stay:
				movespeed = 0;
				break;
			case MovePosition.Left:
				if (GameManager.gameSet == 0) {
					deadbody.transform.rotation = new Quaternion (0, 180, 0, 0);
					if (!moveStopCheck) {
						movespeed = -MoveSpeed;
					} else {
						movespeed = 0;
					}
				}
				break;
			case MovePosition.Right:
				if (GameManager.gameSet == 0) {
					deadbody.transform.rotation = new Quaternion (0, 0, 0, 0);
					if (!moveStopCheck) {
						movespeed = Mathf.Abs (MoveSpeed);
					} else {
						movespeed = 0;
					}
				}
				break;
			}
		}

		// only run playing
		if (GameManager.gameSet == 0) {
			if (!GameManager.pauseCheck) {
				transform.position = new Vector3 (transform.position.x + movespeed * 0.01f, transform.position.y, transform.position.z);
			}
		}
	}


	IEnumerator hive(){
		hiveTime_in = hiveTime;
		hiveHp_in = hiveHp;

		while (true) {
			yield return new WaitForSeconds (0.006f);
			//Debug.Log (hiveHp_in);

			if (hiveHp_in <= 0) {
				hive_bees.SetActive (false);
				StopCoroutine ("hive");
			}

			hiveTime_in = hiveTime_in - Time.deltaTime;
			//Debug.Log (hiveTime_in);
			if (hiveTime_in <= 0) {
				GetComponent<PlayerMove>().deadBody.SetActive (false);
				GetComponent<PlayerMove>().deadEffect.SetActive (true);
				GetComponent<PlayerMove> ().bounce = Bouncy.Not;
				GameManager.gameSet = 2;
				gameset = GameSet.lose;
				GetComponent<PlayerMove> ().Invoke ("resetgame", 2f);
				StopCoroutine ("hive");
			}
		}
	}

	IEnumerator poison(){
		Debug.Log ("poison");

		poisonCheck = true;
		while (true) {
			yield return new WaitForSeconds (0.006f);

			poisonTime_in = poisonTime_in - Time.deltaTime;
			//Debug.Log (hiveTime_in);
			if (poisonTime_in <= 0) {
				poisonCheck = false;
				poison_mush.SetActive (false);
				StopCoroutine ("poison");
			}

		}
	}


	// onTrigger Grounds ?
	void OnTriggerEnter(Collider obj) {
		

		if (obj.CompareTag ("hive")) {
			if (GetComponent<PlayerMove> ().bounce == Bouncy.Down || GetComponent<PlayerMove> ().bounce == Bouncy.Up || GetComponent<PlayerMove> ().bounce == Bouncy.stun) {
				hiveCC = PlayerCC.bug;
				hive_bees.SetActive (true);
				//Destroy (obj.transform.parent.gameObject);
				StartCoroutine ("hive");
			}
		}

		if (obj.CompareTag ("poison")) {
			if (GetComponent<PlayerMove> ().bounce == Bouncy.Down || GetComponent<PlayerMove> ().bounce == Bouncy.Up || GetComponent<PlayerMove> ().bounce == Bouncy.stun) {
				poisonTime_in = poisonTime;
				hiveCC = PlayerCC.bug;
				poison_mush.SetActive (true);
				//Destroy (obj.transform.parent.gameObject);
				StartCoroutine ("poison");
			}
		}

		if (obj.CompareTag("dead")) {
			gameset = GameSet.lose;
		}

		if (obj.CompareTag("clear")) {
			//GameManager.gameSet = 1;
			//gameset = GameSet.win;
		}


	}


}
