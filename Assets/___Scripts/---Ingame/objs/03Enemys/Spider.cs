using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {
	public bool allStop = true;

	public bool radarCheck;

	public float upSpeed;
	float upSpeed_in;
	public float downSpeed;
	float downSpeed_in;
	public float range;
	float range_in;
	public float HwaitTime;
	float HwaitTime_in;
	public float LwaitTime;
	float LwaitTime_in;

	float basePos_y;


	void Start () {
		StartCoroutine ("Reset");
		upSpeed_in = upSpeed * 0.001f;
		downSpeed_in = downSpeed * 0.001f;
		range_in = range;
		basePos_y = transform.position.y;
		HwaitTime_in = HwaitTime;
		LwaitTime_in = LwaitTime;

		switch (name.Substring (6, 1)) {
		case "3":
			transform.position = new Vector3 (transform.position.x, basePos_y - range_in, transform.position.z);
			break;
		}
	}

	IEnumerator Reset(){
		while (true) {
			yield return new WaitForSeconds (0.006f);


			if (!allStop) {
				StartCoroutine ("ready");
				StopCoroutine ("Reset");
			}
		}

	}



	IEnumerator ready(){
		SpiderReset ();
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (!allStop) {
				if (radarCheck) {
					switch (name.Substring (6, 1)) {
					case "1":
						StartCoroutine ("patern1");
						StopCoroutine ("ready");
						break;
					case "2":
						StartCoroutine ("patern2_1");
						StopCoroutine ("ready");
						break;
					case "3":
						StartCoroutine ("patern3");
						StopCoroutine ("ready");
						break;
					}
				}
			}


		}
	}

	IEnumerator patern1(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			if (!allStop) {
				if (transform.position.y >= basePos_y - range_in) {
					transform.position = new Vector3 (transform.position.x, transform.position.y - downSpeed_in, transform.position.z);
				}
			} else {
				StopCoroutine ("patern1");
				StartCoroutine ("ready");
			}
		}
		
	}


	IEnumerator patern2_1(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			if (!allStop) {
				if (transform.position.y >= basePos_y - range_in) {
					transform.position = new Vector3 (transform.position.x, transform.position.y - downSpeed_in, transform.position.z);
				} else {
					transform.position = new Vector3 (transform.position.x, basePos_y - range_in, transform.position.z);
					StartCoroutine ("Lwait");
					StopCoroutine ("patern2_1");
				}
			} else {
				StopCoroutine ("patern2_1");
				StartCoroutine ("ready");
			}
		}
	}

	IEnumerator Lwait(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			if (!allStop) {
				LwaitTime_in = LwaitTime_in - Time.deltaTime;
				if (LwaitTime_in < 0) {
					LwaitTime_in = LwaitTime;
					StartCoroutine ("patern2_2");
					StopCoroutine ("Lwait");
				}
			} else {
				StopCoroutine ("patern2_2");
				StartCoroutine ("ready");
			}
		}
	}

	IEnumerator patern2_2(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			if (!allStop) {
				if (transform.position.y <= basePos_y) {
					transform.position = new Vector3 (transform.position.x, transform.position.y + upSpeed_in, transform.position.z);
				} else {
					transform.position = new Vector3 (transform.position.x, basePos_y, transform.position.z);
					StartCoroutine ("patern2_1");
					StopCoroutine ("patern2_2");
				}
			} else {
				StopCoroutine ("patern2_2");
				StartCoroutine ("ready");
			}
		}
	}

	IEnumerator Hwait(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			if (!allStop) {
				HwaitTime_in = HwaitTime_in - Time.deltaTime;
				if (HwaitTime_in < 0) {
					HwaitTime_in = HwaitTime;
					StartCoroutine ("patern2_1");
					StopCoroutine ("Hwait");
				}
			} else {
				StopCoroutine ("Hwait");
				StartCoroutine ("ready");
			}
		}
	}

	IEnumerator patern3(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			if (!allStop) {
				if (transform.position.y <= basePos_y) {
					transform.position = new Vector3 (transform.position.x, transform.position.y + upSpeed_in, transform.position.z);
				}
			} else {
				StopCoroutine ("patern3");
				StartCoroutine ("ready");
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
