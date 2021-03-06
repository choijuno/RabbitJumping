﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoryStop : MonoBehaviour {
	public GameObject ani1;
	public GameObject ani2;
	public GameObject ani3;
	public GameObject ani4;
	public GameObject ani5;
	public GameObject ani6;
	public GameObject ani7;
	public GameObject ani8;
	public GameObject ani9;

	public GameObject[] anis;

	int i;
	// Use this for initialization
	void Awake () {
        //StartCoroutine ("Ani");
        MusicManager.instance.MusicSelect(false);
    }
	
	IEnumerator Ani() {
		yield return new WaitForSeconds (0.5f);
		ani1.SetActive (true);


		yield return new WaitForSeconds (2f);
		ani2.SetActive (true);


		yield return new WaitForSeconds (0.5f);
		ani3.SetActive (true);


		yield return new WaitForSeconds (1f);
		ani4.SetActive (true);


		yield return new WaitForSeconds (1f);
		ani5.SetActive (true);


		yield return new WaitForSeconds (1f);
		ani6.SetActive (true);


		yield return new WaitForSeconds (0.5f);
		ani7.SetActive (true);


		yield return new WaitForSeconds (1f);
		ani8.SetActive (true);


		yield return new WaitForSeconds (0.5f);
		ani9.SetActive (true);


	}

	void Update() {
		
		if (Input.GetMouseButtonDown(0)) {

			if (i >= 8) {
				Next ();
				return;
			}

			i++;
			anis [i].SetActive (true);


		}

	}

	void Next(){
		Debug.Log ("next");
        GameManager.TestNum = 1;
        mainSceneManager.SceneIndex = 2;
        StartCoroutine(waitLoadingScene());
    }
    IEnumerator waitLoadingScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }
}
