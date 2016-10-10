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


	void Start () {
		waitTime_in = waitTime;
		runSpeed_in = runSpeed * 0.001f;
		runTime_in = runTime;
		boostTime_in = boostTime;
	}

	void Update () {
		//Debug.Log (runSpeed_in);

		if (stat == hawkStatus.wait) {
			stat = hawkStatus.not;
			hawkCollider.SetActive (false);
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
				attCollider.SetActive (false);
			}

		}
	}

	IEnumerator away(){
		waitTime_in = 5;
		while (true) {
			yield return new WaitForSeconds (0.006f);
			transform.position = new Vector3 (transform.position.x + runSpeed_in * 2.5f, transform.position.y, transform.position.z);

			waitTime_in = waitTime_in - Time.deltaTime;
			if (waitTime_in <= 0) {
				this.gameObject.SetActive (false);
			}

		}
	}

	IEnumerator boost() {
		
		while (true) {
			yield return new WaitForSeconds (0.006f);

			boostTime_in = boostTime_in - Time.deltaTime;
			if (boostTime_in <= 0) {
				runSpeed_in = runSpeed_in * 2.5f;
				StopCoroutine ("boost");
			}
		}

	}
}
