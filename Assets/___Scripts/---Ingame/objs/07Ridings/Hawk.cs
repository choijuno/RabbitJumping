using UnityEngine;
using System.Collections;

public class Hawk : MonoBehaviour {
	public GameObject Camera_ingame;

	public hawkStatus stat = hawkStatus.stay;

	public GameObject Pos;
	public GameObject hawkCollider;
	public GameObject attCollider;
	public float waitTime;
	float waitTime_in;
	public float runSpeed;
	float runSpeed_in;
	public float runTime;
	float runTime_in;
	public float boostTime;
	float boostTime_in;

	//new
	BoxCollider mybody;
	BoxCollider attColliderBox;
	public GameObject model;

	//reset
	float basePosX;


	void Start () {
		Invoke ("resetTime", 2f);
		mybody = hawkCollider.GetComponent<BoxCollider> ();
		attColliderBox = attCollider.GetComponent<BoxCollider> ();
		basePosX = transform.position.x;
		waitTime_in = waitTime;
		runSpeed_in = runSpeed * 0.001f;
		runTime_in = runTime;
		boostTime_in = boostTime;
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
				boostTime_in = boostTime;
				model.SetActive (false);
				model.SetActive (true);
				mybody.enabled = true;
				attColliderBox.enabled = true;
				stat = hawkStatus.stay;
				StopCoroutine ("resetCheck");
			}

		}
	}

	void Update () {
		//Debug.Log (runSpeed_in);

		if (stat == hawkStatus.wait) {
			
			stat = hawkStatus.not;
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
				StartCoroutine ("att");
				StopCoroutine ("wait");
			}
		}
	}

	IEnumerator att(){
		if (boostTime_in > 0) {
			StartCoroutine ("boost");
		}
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
			transform.position = new Vector3 (transform.position.x + runSpeed_in * 2.5f, transform.position.y, transform.position.z);

			if (GameManager.retry_count > 0) {
				model.SetActive (false);
				StopCoroutine ("away");
			}

			waitTime_in = waitTime_in - Time.deltaTime;
			if (waitTime_in <= 0) {
				model.SetActive (false);
				StopCoroutine ("away");
			}

		}
	}

	IEnumerator boost() {
		
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (GameManager.retry_count > 0) {
				StopCoroutine ("boost");
			}

			boostTime_in = boostTime_in - Time.deltaTime;


			if (boostTime_in <= 0) {
				runSpeed_in = runSpeed_in * 2.5f;
				StopCoroutine ("boost");
			}
		}

	}
}
