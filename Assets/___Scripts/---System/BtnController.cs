using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour {

	public GameObject OpenPanel_1;
	public GameObject OpenPanel_2;

	public GameObject ClosePanel_1;
	public GameObject ClosePanel_2;

	//pause
	public GameObject tiltOn;
	public GameObject tiltOff;

	public void pauseBtn() {
		Time.timeScale = 0.0001f;
		GameManager.pauseCheck = true;
		justOpen ();
		justClose ();
		if (GameManager.tiltCheck) {
			tiltOn.SetActive (true);
		} else {
			tiltOff.SetActive (true);

		}
	}

	public void pauseClose(){
		Time.timeScale = 1f;
		GameManager.pauseCheck = false;
		justOpen ();
		justClose ();
	}

	public void pauseReset(){
		Time.timeScale = 1f;
		GameManager.pauseCheck = false;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void pauseHome(){
		Time.timeScale = 1f;
		GameManager.pauseCheck = false;
        //Application.LoadLevel ("SelectScene");
        mainSceneManager.SceneIndex = 3;
        SceneManager.LoadScene(1);

	}

	public void Bgm_OnOff(){
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
		switch (this.name) {
		case "On":
			openClose ();
			break;
		case "Off":
			openClose ();
			break;
		}
	}

	public void Tilt_OnOff(){
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
        if(ES2.Load<int>("stageIndexCount") < GameManager.TestNum)
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
}
