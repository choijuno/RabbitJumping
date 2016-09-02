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


	float chaPos;
	float chaMax = 564f;


	//inGame
	//Map_L
	float chaStartPos;
	//Map_R
	float chaEndPos;
	//clearGround
	float EndPos;


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
						EndPos = child.transform.position.x;
					}


					if (child.name.Substring (0, 7) == "1990051") {
						chaStartPos = Mathf.Abs (child.transform.position.x);
					}

					if (child.name.Substring (0, 7) == "1990052") {
						chaEndPos = child.transform.position.x;
					}


				}

				//clearGroundCheck
				gage_bar.fillAmount = (EndPos + chaStartPos)  / (chaEndPos + chaStartPos);
				chaPos = gage_bar.fillAmount * chaMax;
				finish_ground.transform.localPosition = new Vector3 (StartPos.transform.localPosition.x + chaPos -6f , finish_ground.transform.localPosition.y, finish_ground.transform.localPosition.z );


				StartCoroutine ("naviStart");
				gage_Character.SetActive (true);
				finish_ground.SetActive (true);
				StopCoroutine ("posSet");
			} else {
				timer = timer - Time.deltaTime;
			}

		}
	}

	IEnumerator naviStart(){
		

		while (true) {
			yield return new WaitForSeconds (0.006f);
			//Debug.Log ("endPos : " + EndPos + " // CharacterPos : " + (playerBody.transform.position.x + chaStartPos));
			gage_bar.fillAmount = (playerBody.transform.position.x + chaStartPos)  / (chaEndPos + chaStartPos);
			chaPos = gage_bar.fillAmount * chaMax;
			gage_Character.transform.localPosition = new Vector3 (StartPos.transform.localPosition.x + chaPos -6f , gage_Character.transform.localPosition.y, gage_Character.transform.localPosition.z );
		}
	}




}
