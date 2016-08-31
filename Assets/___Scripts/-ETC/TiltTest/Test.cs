using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Test : MonoBehaviour {
	public Text xtestNum_txt;
	public Text ytestNum_txt;
	public Text ztestNum_txt;

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		xtestNum_txt.text = "" + Input.acceleration.x;
		ytestNum_txt.text = "" + Input.acceleration.y;
		ztestNum_txt.text = "" + Input.acceleration.z;




	}
}
