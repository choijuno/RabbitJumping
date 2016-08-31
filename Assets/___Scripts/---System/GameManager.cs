using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


public class GameManager : MonoBehaviour {
    
	public static bool pauseCheck;

	public bool resultCheck;

	//UI
	public static int Money_ingame = 0;
	public Text Money_ingame_txt;

	public static float Record_time;
	int m_record;
	int s_record;
	public Text Record_time_txt;

	public static int Record_help_Max;
	public Text Record_help_Max_txt;
	public static int Record_help;
	public Text Record_help_txt;

	public GameObject result_Panel;
	public GameObject result_Panel_Clear;
	public GameObject[] Star;
	public GameObject result_Panel_Failed;

	public Text result_gold;
	public Text result_time;
	public Text result_help;
	public Text result_score;

	int Score_ingame;


	public GameObject pauseMenu;
	public GameObject Controller;



	// end game Check
	public static int gameSet; //0:play 1:win 2:lose 3:wait

	public static int TestNum;

	public static bool tiltCheck;
	public GameObject tiltOn;
	public GameObject tiltOff;


    //Json

    JsonParsing JsonGo;
    int starCount = 0;
	void Start()
    {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
        JsonGo = GameObject.Find("Json").GetComponent<JsonParsing>();

    }

	void Awake () {
		if (Application.loadedLevelName == "TestGame") {

			if (ES2.Exists ("tilt")) {
				tiltCheck = ES2.Load<bool> ("tilt");
			} else {

			}

			Money_ingame = 0;
			Record_time = 0;
			Record_help = 0;
			Record_help_Max = 0;
			gameSet = 3;
			if (Application.loadedLevelName == "TestGame") {
				StartCoroutine ("UICheck");
			}

			if (tiltCheck) {
				tiltOn.SetActive (true);
				tiltOff.SetActive (false);
			}
		}
	}
	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape)){
			if (Application.loadedLevelName != "TestGame") {
				Application.Quit ();
			} else {
				Time.timeScale = 1f;
				pauseCheck = false;
				pauseMenu.SetActive (true);
				Controller.SetActive (false);
			}

		}



    }

	IEnumerator UICheck(){
		while (true) {
			yield return new WaitForSeconds (0.006f);

			switch (gameSet) {
			case 1:
				gameSet = 3;
				StopCoroutine ("UICheck");
				StartCoroutine ("gameResult_Clear");
				
				break;

			case 2:
				gameSet = 3;
				StopCoroutine ("UICheck");
				StartCoroutine ("gameResult_Failed");
				
				break;
			}
				


			if (m_record == 59 && s_record == 59) {

			} else {
				Record_time = Record_time + Time.deltaTime;
				m_record = (int)(Record_time / 60) % 60;
				s_record = (int)Record_time % 60;
			}
			Money_ingame_txt.text = Money_ingame.ToString("n0");
			Record_time_txt.text = m_record.ToString("00") + " : " + s_record.ToString("00");
			Record_help_txt.text = Record_help.ToString();
			Record_help_Max_txt.text = Record_help_Max.ToString ();
		}
	}

	IEnumerator gameResult_Clear(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			result_Panel.SetActive (true);
			result_Panel_Clear.SetActive (true);
			result (); //골드, 시간, 동물구함.
			ScoreCheck (); //점수체크.
			StarCheck (); //별체크.
			StopCoroutine ("gameResult_Clear");
		}
	}

	IEnumerator gameResult_Failed(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			result_Panel.SetActive (true);
			result_Panel_Failed.SetActive (true);
			result ();
			ScoreCheck ();
			StopCoroutine ("gameResult_Failed");
		}
	}

	void result() {
		result_gold.text = Money_ingame.ToString("n0");
        DataSave._instance.setMoney_Game(Money_ingame);

		result_time.text = m_record.ToString("00") + ":" + s_record.ToString("00");
		result_help.text = Record_help +"/" +Record_help_Max;
	}

	void ScoreCheck() {
		Score_ingame += Record_help * 5000;
		Score_ingame += Money_ingame * 10;
		Score_ingame -= (int)Record_time * 20;
		if (Score_ingame < 0) {
			Score_ingame = 0;
		}
		result_score.text = Score_ingame.ToString ("n0");
        
	}

	void StarCheck() {
        starCount = JsonGo.starJsonData(TestNum, Score_ingame);
        switch (starCount)
        {
            case 1:
                Star[0].SetActive(true);
                break;
            case 2:
                Star[0].SetActive(true);
                Star[1].SetActive(true);
                break;
            case 3:
                Star[0].SetActive(true);
                Star[1].SetActive(true);
                Star[2].SetActive(true);
                break;
        }

        DataSave._instance.saveData(GameManager.TestNum, starCount, Score_ingame);
        DataSave._instance.setStar_Count(starCount);
    }
}
