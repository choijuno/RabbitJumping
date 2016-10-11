using UnityEngine;
using System.Collections;

public class Elephant : MonoBehaviour {
	

	public elephantStatus stat = elephantStatus.stay;

	public GameObject Camera_ingame;

	public Animator thisAni;

	public GameObject Pos;
	public GameObject elephantCollider;
	public GameObject attCollider;
	public float waitTime;
	float waitTime_in;
	public float runSpeed;
	float runSpeed_in;
	public float runTime;
	float runTime_in;

	//new
	BoxCollider mybody;
	BoxCollider attColliderBox;
	public GameObject model;

	//reset
	float basePosX;

	void Start () {
		Invoke ("resetTime", 2f);
		mybody = elephantCollider.GetComponent<BoxCollider> ();
		attColliderBox = attCollider.GetComponent<BoxCollider> ();
		basePosX = transform.position.x;
		waitTime_in = waitTime;
		runSpeed_in = runSpeed * 0.001f;
		runTime_in = runTime;
	}

	void resetTime(){
		if (GameManager.retry_Check) {

			StartCoroutine ("resetCheck");
		}
	}

	IEnumerator resetCheck(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (GameManager.retry_count >= 1) {
				transform.position = new Vector3 (basePosX, transform.position.y, transform.position.z);
				waitTime_in = waitTime;
				runSpeed_in = runSpeed * 0.001f;
				runTime_in = runTime;
				model.SetActive (false);
				model.SetActive (true);
				mybody.enabled = true;
				attColliderBox.enabled = true;
				stat = elephantStatus.stay;
				StopCoroutine ("resetCheck");
			}

		}
	}

	void Update () {
		if (stat == elephantStatus.wait) {

			thisAni.SetTrigger ("wait");
			stat = elephantStatus.not;
			mybody.enabled = false;
			StartCoroutine ("wait");

		}
	}

	IEnumerator wait(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			waitTime_in = waitTime_in - Time.deltaTime;
			if (waitTime_in <= 0) {
				waitTime_in = 0;
				thisAni.SetTrigger ("run");
				StartCoroutine ("att");
				StopCoroutine ("wait");
			}
		}
	}

	IEnumerator att(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			transform.position = new Vector3 (transform.position.x + runSpeed_in, transform.position.y, transform.position.z);
			runTime_in = runTime_in - Time.deltaTime;
			if (runTime_in <= 0) {
				runTime_in = 0;
				StartCoroutine ("away");
				StopCoroutine ("att");
				attColliderBox.enabled = false;
			}

		}
	}

	IEnumerator away(){
		waitTime_in = 5;
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (GameManager.retry_count > 0) {
				model.SetActive (false);
				StopCoroutine ("away");
			}

			transform.position = new Vector3 (transform.position.x + runSpeed_in * 1.5f, transform.position.y, transform.position.z);

			waitTime_in = waitTime_in - Time.deltaTime;
			if (waitTime_in <= 0) {
				model.SetActive (false);
				StopCoroutine ("away");
			}

		}
	}


}
