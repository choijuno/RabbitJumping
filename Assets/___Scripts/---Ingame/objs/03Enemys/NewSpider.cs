using UnityEngine;
using System.Collections;

public class NewSpider : MonoBehaviour {
	public bool allStop = true;
	public GameObject rader_obj;

	public bool radarCheck;

	int pattern;

	public float upSpeed;
	float upSpeed_in;
	public float downSpeed;
	float downSpeed_in;


	public float range;
	float range_in;

	float Maxlow;

	public float HwaitTime;
	float HwaitTime_in;
	public float LwaitTime;
	float LwaitTime_in;

	float basePos_y;

	Vector3 basePos;


	void Start () {
		/*
		if (Application.loadedLevelName == "Edit") {
			allStop = true;
			StartCoroutine("wait");
		}
		*/

		//StartCoroutine ("Reset");
		StartCoroutine("findPlayer");

		upSpeed_in = upSpeed * 0.001f;
		downSpeed_in = downSpeed * 0.001f;

		basePos = transform.position;
		basePos_y = transform.position.y;

		HwaitTime_in = HwaitTime;
		LwaitTime_in = LwaitTime;

		switch (name.Substring (6, 1)) {
		case "1": //low
			Maxlow = 2;
			break;
		case "2": //mid
			Maxlow = 4;
			break;
		case "3": //high
			Maxlow = 6;
			break;
		}

		switch (name.Substring (5, 1)) {
		case "4":
			transform.position = new Vector3 (transform.position.x, basePos_y - range_in, transform.position.z);
			pattern = 1;
			break;
		case "5":
			transform.position = new Vector3 (transform.position.x, basePos_y - range_in, transform.position.z);
			pattern = 2;
			break;
		case "6":
			transform.position = new Vector3 (transform.position.x, basePos_y - range_in, transform.position.z);
			pattern = 3;
			Maxlow = 10;
			break;
		}



	}





	IEnumerator wait(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (!allStop) {
				if (radarCheck) {
					StartCoroutine ("att");
					StopCoroutine ("wait");
				}
			} else {
				//reset
				transform.position = basePos;
				rader_obj.SetActive(true);
				radarCheck = false;
			}

		}
	}

	IEnumerator findPlayer(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
				if (radarCheck) {
					StartCoroutine ("att");
					StopCoroutine ("findPlayer");
				}

		}
	}

	IEnumerator att(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			switch (pattern) {
			case 1: //Down
				if (transform.position.y >= Maxlow) {
					transform.position = new Vector3 (transform.position.x, transform.position.y - downSpeed_in, transform.position.z);
				} else {
					transform.position = new Vector3 (transform.position.x, Maxlow, transform.position.z);
					StopCoroutine ("att");
				}

				break;

			case 2: //DownUp
				StartCoroutine("att_Down");
				StopCoroutine ("att");
				break;

			case 3: //Up
				if (transform.position.y <= Maxlow) {
					transform.position = new Vector3 (transform.position.x, transform.position.y + downSpeed_in, transform.position.z);
				} else {
					StopCoroutine ("att");
				}
				break;
			}

		}
	}


	IEnumerator att_Down(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (transform.position.y >= Maxlow) {
				transform.position = new Vector3 (transform.position.x, transform.position.y - downSpeed_in, transform.position.z);
			} else {
				transform.position = new Vector3 (transform.position.x, Maxlow, transform.position.z);
				StartCoroutine ("L_wait");
				StopCoroutine ("att_Down");
			}

		}
	}
	IEnumerator L_wait(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			LwaitTime_in = LwaitTime_in - Time.deltaTime;
			if (LwaitTime_in < 0) {
				LwaitTime_in = LwaitTime;
				StartCoroutine ("att_Up");
				StopCoroutine ("L_wait");
			}
		}
	}

	IEnumerator att_Up(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (transform.position.y <= basePos_y) {
				transform.position = new Vector3 (transform.position.x, transform.position.y + upSpeed_in, transform.position.z);
			} else {
				transform.position = new Vector3 (transform.position.x, basePos_y, transform.position.z);
				StartCoroutine ("H_wait");
				StopCoroutine ("att_Up");
			}

		}
	}
	IEnumerator H_wait(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			HwaitTime_in = HwaitTime_in - Time.deltaTime;
			if (HwaitTime_in < 0) {
				HwaitTime_in = HwaitTime;
				StartCoroutine ("att_Down");
				StopCoroutine ("H_wait");
			}
		}
	}













	void SpiderReset(){
		switch (name.Substring (6, 1)) {
		case "1":
			transform.position = new Vector3 (transform.position.x, basePos_y, transform.position.z);
			break;
		case "2":
			transform.position = new Vector3 (transform.position.x, basePos_y, transform.position.z);
			break;
		case "3":
			transform.position = new Vector3 (transform.position.x, basePos_y - range_in, transform.position.z);
			break;
		}
		upSpeed_in = upSpeed * 0.001f;
		downSpeed_in = downSpeed * 0.001f;
		range_in = range;
		HwaitTime_in = HwaitTime;
		LwaitTime_in = LwaitTime;
	}
}
