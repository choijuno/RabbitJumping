using UnityEngine;
using System.Collections;

public class Monkey : MonoBehaviour {

	public Animator monkey;
	public GameObject banana;
	public GameObject banana_image;
	public GameObject hand;
	public float waitTime;
	float waitTime_in;
	public float attSpeed;
	float attSpeed_in;
	float turnSpeed_in;
	// Use this for initialization
	void Start () {
		waitTime_in = waitTime;

		attSpeed_in = attSpeed;
		turnSpeed_in = attSpeed_in / 4;
		attSpeed_in = attSpeed * 0.001f;

		StartCoroutine ("wait");
		StartCoroutine ("att");
	}
	
	// Update is called once per frame
	IEnumerator wait(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			waitTime_in = waitTime_in - Time.deltaTime;

			if (waitTime_in <= 0) {
				monkey.SetTrigger ("attack");
				waitTime_in = waitTime;
				Invoke ("attack", 0.6f);

			}
		}
	}

	IEnumerator att(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			banana.transform.position = new Vector3 (banana.transform.position.x - attSpeed_in, banana.transform.position.y - attSpeed_in, banana.transform.position.z);
			banana_image.transform.Rotate (0, 0, turnSpeed_in);

		}
	}

	void attack(){
		banana.SetActive (true);
		banana.transform.position = hand.transform.position;
	}


}
