using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour {
	public Animator GageBase;

	public GameObject OpenPanel_1;
	public GameObject OpenPanel_2;

	public GameObject ClosePanel_1;
	public GameObject ClosePanel_2;

	//pause
	public GameObject tiltOn;
	public GameObject tiltOff;

	//ingame
	public GameObject loadParent;
	public GameObject playerbody;
	Transform Find_ChildinParent;
	public Transform StartPos;


	public void UIGage_Close_btn() {
		GageBase.SetBool ("gageOpen", false);
		openClose ();
	}

	public void UIGage_Open_btn() {
		GageBase.SetBool ("gageOpen", true);
		openClose ();
	}

	public void pauseBtn() {
		MusicManager.instance.PlayOnShot ();
		Time.timeScale = 0.0001f;
		GameManager.pauseCheck = true;
		justOpen ();
		justClose ();
		if (GameManager.tiltCheck) {
			tiltOff.SetActive (false);
			tiltOn.SetActive (true);
		} else {
			tiltOn.SetActive (false);
			tiltOff.SetActive (true);

		}
	}

	public void pauseClose(){
		MusicManager.instance.PlayOnShot ();
		Time.timeScale = 1f;
		GameManager.pauseCheck = false;
		justOpen ();
		justClose ();
	}

	public void pauseReset(){
		MusicManager.instance.PlayOnShot ();
		Time.timeScale = 1f;
		GameManager.pauseCheck = false;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void pauseHome(){
		MusicManager.instance.PlayOnShot ();
		Time.timeScale = 1f;
		GameManager.pauseCheck = false;
        //Application.LoadLevel ("SelectScene");
        mainSceneManager.SceneIndex = 3;
        SceneManager.LoadScene(2);

	}

	public void Bgm_OnOff(){
		MusicManager.instance.PlayOnShot ();
		//ES2.Load<bool> ("musicChk")
		switch (this.name) {
		case "On":
			openClose ();
			//ClosePanel_2.SetActive (false);
			MusicManager.instance.MusicSelect (false);
			break;
		case "Off":
			openClose ();
			//OpenPanel_2.SetActive (true);
			MusicManager.instance.MusicSelect (true);
			break;
		}
	}

	public void Sound_OnOff(){
		MusicManager.instance.PlayOnShot ();
		switch (this.name) {
		case "On":
			openClose ();
			ES2.Save<bool> (false, "HyoGwaSound");
			GameManager.soundVolume = 0;
			break;
		case "Off":
			openClose ();
			ES2.Save<bool> (true, "HyoGwaSound");
			GameManager.soundVolume = 1;
			break;
		}
	}

	public void Tilt_OnOff(){
		MusicManager.instance.PlayOnShot ();
		switch (this.name) {
		case "On":
			GameManager.tiltCheck = false;
			ES2.Save<bool> (GameManager.tiltCheck, "tilt");
			openClose ();
			break;
		case "Off":
			GameManager.tiltCheck = true;
			ES2.Save<bool> (GameManager.tiltCheck, "tilt");
			openClose ();
			break;
		}
	}

	void openClose() {
		OpenPanel_1.SetActive (true);
		ClosePanel_1.SetActive (false);
	}

	void justOpen() {
		OpenPanel_1.SetActive (true);
	}
	void justClose() {
		ClosePanel_1.SetActive (false);
	}


	public void goHome_ingame(){
		Application.LoadLevel ("SelectScene");
	}

	public void replay_ingame(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void nextStage_ingame(){
        GameManager.TestNum += 1;
        if (ES2.Load<int>("stageIndexCount") < GameManager.TestNum)
        {
            Debug.Log("작다.");
            Vector2 test = ES2.Load<Vector2>("scrollPanel");
            Debug.Log("====벡터====" + test);
            float scroll_X;
            float scroll_Y;
            scroll_X = test.x - 200;
            scroll_Y = test.y;

            ES2.Save<Vector2>(new Vector2(scroll_X, scroll_Y), "scrollPanel");
            Debug.Log("=====마지막벡터=====" + ES2.Load<Vector2>("scrollPanel"));
        }
        Application.LoadLevel (Application.loadedLevel);
	}



	//Retry

	public void TestRetry_ingame(){
		GameManager.retry_count ++;
		GameManager.retry_Check = false;

	}

	public void Retry_ingame(){
		
	}

	public void NoRetry_ingame(){
		GameManager.retry_count --;
		GameManager.retry_Check = false;
	}
}
