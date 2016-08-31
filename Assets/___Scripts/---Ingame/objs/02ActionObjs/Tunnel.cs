using UnityEngine;
using System.Collections;

public class Tunnel : MonoBehaviour {
	public GameObject exit;
	public bool moveCheck;

	public float waitTime;
	public float Speed;
	public float exitTime;

	public int direction;

	// Use this for initialization
	void Start () {
		if (exit.transform.position.x <= this.transform.position.x) {
			//left
			direction = 1;
		} else {
			//right
			direction = 2;
		}
	}

	IEnumerator wait(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (moveCheck) {

			}
		}
	}


}


