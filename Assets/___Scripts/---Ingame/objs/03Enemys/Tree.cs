using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

	public GameObject fruit;
	public float waitTime;
	float waitTime_in;
	public float attSpeed;
	float attSpeed_in;
	public float stunTime;
	float Xbase;
	float Ybase;
	// Use this for initialization
	void Start () {
		Xbase = fruit.transform.position.x;
		Ybase = fruit.transform.position.y;
		waitTime_in = waitTime;
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
				waitTime_in = waitTime;
				fruit.transform.position = new Vector3(Xbase, Ybase, 0);
				fruit.SetActive (true);
			}
		}
	}

	IEnumerator att(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			fruit.transform.position = new Vector3 (fruit.transform.position.x, fruit.transform.position.y - attSpeed_in, 0);
		}
	}

}
