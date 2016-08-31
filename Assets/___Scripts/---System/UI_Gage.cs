using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Gage : MonoBehaviour {

	//fillamount
	public Image gage_bar;



	//Gage_Objects
	public GameObject gage_Character;
	public GameObject finish_ground;

	//Objects in
	public Transform loadParent;


	//inGame_Player
	public GameObject playerBody;

	//GageStart
	public GameObject StartPos;
	//

	public float Pos;
	public float EndPos;

	float chaPos;
	float chaMax = 564f;

	public GameObject chaStart;
	float chaStartPos;
	public GameObject chaEnd;
	float chaEndPos;



	// Use this for initialization
	void Start () {
		EndPos = 20f;

		StartCoroutine ("posSet");


	}

	IEnumerator posSet(){
		float timer = 1f;
		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (timer <= 0) {
				
				foreach (Transform child in loadParent) {
					if (child.name.Substring (0, 7) == "1990021") {
						EndPos = child.transform.position.x + 6.08f;
					}

				}

				StopCoroutine ("posSet");
			} else {
				timer = timer - Time.deltaTime;
			}

		}
	}



	// Update is called once per frame
	void Update () {
		
		gage_bar.fillAmount = (playerBody.transform.position.x + 7.58f)  / EndPos;
		chaPos = gage_bar.fillAmount * chaMax;
		gage_Character.transform.localPosition = new Vector3 (StartPos.transform.localPosition.x + chaPos -6f , gage_Character.transform.localPosition.y, gage_Character.transform.localPosition.z );
	}
}
