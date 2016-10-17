using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


public class GameManager : MonoBehaviour {
    
	public GameObject ingameCamera;

	public static bool pauseCheck;

	public static float soundVolume;

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
	public GameObject[] openHelp;
	public GameObject[] closeHelp;

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



	//new 
	public static float Record_time_d;
	int m_record_d;
	int s_record_d;
	public Text Record_time_d_txt;

	//timebar
	public Text time_bar_txt;
	public GameObject failBar;


	// end game Check
	public static int gameSet; //0:play 1:win 2:lose 3:wait 4:retry? 5:story

	public static int TestNum;

	public static bool tiltCheck;
	public GameObject tiltOn;
	public GameObject tiltOff;

	public GameObject soundOn;
	public GameObject soundOff;

	public GameObject hyogwaOn;
	public GameObject hyogwaOff;


	public AudioClip clear_star;
	public AudioClip clear_exit;

	public AudioClip failed_;


	//retry
	public static int retry_count;
	public static bool retry_Check;

	public GameObject retry_Panel;
	public Text retryStageNum_txt;
	public Text retryTime_txt;
	float retry_time;
	public GameObject loadParent;


	//ingame Language

	public GameObject[] Language_kr_img;
	public GameObject[] Language_eng_img;



    //Json

	JsonParsing JsonGo;
    int starCount = 0;


	//mission
	public static int helpTotal;
	void Start()
    {
		if (TestNum != 0) {
			JsonGo = GameObject.Find ("Json").GetComponent<JsonParsing> ();
			//downTime
			Record_time_d = JsonGo.starJsonData (TestNum);
			m_record_d = (int)(Record_time_d / 60) % 60;
			s_record_d = (int)Record_time_d % 60;
		} else {
			Record_time_d = 120;
			m_record_d = (int)(Record_time_d / 60) % 60;
			s_record_d = (int)Record_time_d % 60;
		}

		starCount = 0;

		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		if (Application.loadedLevelName == "TestGame") {
			
			JsonGo = GameObject.Find ("Json").GetComponent<JsonParsing> ();

			time_bar_txt.text = m_record_d + ":" + s_record_d.ToString("00");

			//backgroundmusic
			if (ES2.Exists ("musicChk")) {
				if (ES2.Load<bool> ("musicChk")) {
					soundOn.SetActive (true);
					soundOff.SetActive (false);
					MusicManager.instance.MusicSelect (true);
				} else {
					soundOn.SetActive (false);
					soundOff.SetActive (true);
					MusicManager.instance.MusicSelect (false);
				}
			} else {
				ES2.Save<bool> (true, "musicChk");
				soundOn.SetActive (true);
				soundOff.SetActive (false);
			}

			//hyogwa
			if (ES2.Exists ("HyoGwaSound")) {
				if (ES2.Load<bool> ("HyoGwaSound")) {
					soundVolume = 1;
					hyogwaOn.SetActive (true);
					hyogwaOff.SetActive (false);
				} else {
					soundVolume = 0f;
					hyogwaOn.SetActive (false);
					hyogwaOff.SetActive (true);
				}
			} else {
				ES2.Save<bool> (true, "HyoGwaSound");
				soundVolume = 1;
				hyogwaOn.SetActive (true);
				hyogwaOff.SetActive (false);
			}

			//tilt
			if (ES2.Exists ("tilt")) {
				if (ES2.Load<bool> ("tilt")) {
					tiltOn.SetActive (true);
					tiltOff.SetActive (false);
					tiltCheck = true;
				} else {
					tiltOn.SetActive (false);
					tiltOff.SetActive (true);
					tiltCheck = false;
				}
			} else {
				ES2.Save<bool> (false, "tilt");
				tiltOff.SetActive (true);
				tiltOn.SetActive (false);
			}

			//language
			if (ES2.Exists ("Language")) {
				if (ES2.Load<bool> ("Language")) {
					language_kr ();
				} else {
					language_eng ();
				}
			} else {
				ES2.Save<bool> (true, "Language");
				language_kr ();
			}



		}
    }

	void language_kr() {
		for (int i = 0; i < Language_kr_img.Length; i++) {
			Language_eng_img [i].SetActive (false);
			Language_kr_img [i].SetActive (true);
		}
	}

	void language_eng() {
		for (int i = 0; i < Language_eng_img.Length; i++) {
			Language_kr_img [i].SetActive (false);
			Language_eng_img [i].SetActive (true);
		}
	}

	void Awake () {
		
		retry_count = 0;
		gameSet = 3;

		if (Application.loadedLevelName == "TestGame") {



			Money_ingame = 0;

			//upTime
			Record_time = 0;



			Record_help = 0;
			Record_help_Max = 0;

			StartCoroutine ("downTimeReady");

			if (Application.loadedLevelName == "TestGame") {
				
					StartCoroutine ("UICheck");

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
			case 1://win
				
				yield return new WaitForSeconds (1.1f);
				gameSet = 3;
				StopCoroutine ("UICheck");
				StartCoroutine ("gameResult_Clear");
				
				break;

			case 2://lose
				
				ingameCamera.GetComponent<GameCamera>().viveCheck = true;
				yield return new WaitForSeconds (1.7f);
				gameSet = 3;
				StopCoroutine ("UICheck");
				StartCoroutine ("gameResult_Failed");
				
				break;

			case 4://retry
				ingameCamera.GetComponent<GameCamera>().viveCheck = true;
				yield return new WaitForSeconds (1.7f);
				gameSet = 3;
				StopCoroutine ("UICheck");
				StartCoroutine ("gameResult_retry");
				break;
			}
				



			upTime ();
			//downTime ();
			Money_ingame_txt.text = Money_ingame.ToString("n0");

			//up time
			Record_time_txt.text = m_record.ToString("00") + " : " + s_record.ToString("00");

			//down time
			Record_time_d_txt.text = m_record_d.ToString("00") + " : " + s_record_d.ToString("00");


			Record_help_txt.text = Record_help.ToString();
			Record_help_Max_txt.text = Record_help_Max.ToString ();

			//new help

		}
	}

	void upTime() {
		if (m_record == 59 && s_record == 59) {

		} else {
			Record_time = Record_time + Time.deltaTime;
			m_record = (int)(Record_time / 60) % 60;
			s_record = (int)Record_time % 60;
		}
	}

	IEnumerator downTimeReady() {
		while (true) {
			if (gameSet == 0) {
				StartCoroutine ("downTime");
				StopCoroutine ("downTimeReady");
			} else {

			}
			yield return null;
		}
	}

	IEnumerator downTime() {
		while (true) {
			if (m_record_d == 0 && s_record_d == 0) {
				Record_time_d = 0;
				failBar.SetActive (true);
				StopCoroutine ("downTime");
			} else {
				Record_time_d = Record_time_d - Time.deltaTime;
				m_record_d = (int)(Record_time_d / 60) % 60;
				s_record_d = (int)Record_time_d % 60;
			}
			yield return null;
		}
	}

	IEnumerator gameResult_Clear(){
		starCount++;
		Debug.Log ("clear ++");
		while (true) {
			yield return new WaitForSeconds (0.006f);
			result_Panel.SetActive (true);
			result_Panel_Clear.SetActive (true);
			result (); //골드, 시간, 동물구함.
			ScoreCheck (); //점수체크.

			StartCoroutine("StarCheck_Effect");
			//StarCheck (); //별체크.

			StopCoroutine ("gameResult_Clear");
		}
	}


	IEnumerator gameResult_Failed(){
		while (true) {
			yield return new WaitForSeconds (0.006f);
			AudioSource.PlayClipAtPoint (failed_, ingameCamera.transform.position, GameManager.soundVolume);
			result_Panel.SetActive (true);
			result_Panel_Failed.SetActive (true);
			result ();

			ScoreCheck ();
			StopCoroutine ("gameResult_Failed");
		}
	}

	IEnumerator gameResult_retry(){
		retry_Panel.SetActive (true);
		retryStageNum_txt.text = "STAGE " + TestNum;
		retry_time = 10;

		while (true) {
			yield return new WaitForSeconds (0.006f);
			if (retry_count < 0) {
				retry_Panel.SetActive (false);
				StopCoroutine ("gameResult_retry");
				StartCoroutine ("gameResult_Failed");
			}

			if (retry_count == 1) {
				gameSet = 0;
				retry_Panel.SetActive (false);
				StartCoroutine ("UICheck");
				StopCoroutine ("gameResult_retry");
			}

			retry_time -= Time.deltaTime;
			retryTime_txt.text = "" + retry_time.ToString("###0");
			if (retry_time >= 0) {
				
			} else {
				retry_Panel.SetActive (false);
				StopCoroutine ("gameResult_retry");
				StartCoroutine ("gameResult_Failed");
			}

			//StopCoroutine ("gameResult_retry");
		}
	}

	void result() {
		result_gold.text = Money_ingame.ToString("n0");
        DataSave._instance.setMoney_Game(Money_ingame);

        float gold = DataSave._instance.getMoney_Game();
        if(gold == 1000)
            Social.ReportProgress(GPGS.achievement_Gold1, 100.0f, (bool success) => { });
        else if(gold == 10000)
            Social.ReportProgress(GPGS.achievement_Gold2, 100.0f, (bool success) => { });
        else if(gold == 100000)
            Social.ReportProgress(GPGS.achievement_Gold3, 100.0f, (bool success) => { });
        else if(gold == 1000000)
            Social.ReportProgress(GPGS.achievement_Gold4, 100.0f, (bool success) => { });

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
        starCount = JsonGo.starJsonData(TestNum);
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

	IEnumerator StarCheck_Effect()
    {
		if (Record_help_Max == Record_help) {
			starCount++;
			Debug.Log ("help ++");
		}

		if (Record_time_d != 0) {
			starCount++;
			Debug.Log ("time ++");
			Debug.Log (starCount);
		}
		//starCount = JsonGo.starJsonData(TestNum);
        
		DataSave._instance.saveData(GameManager.TestNum, starCount, 0);
		DataSave._instance.setStar_Count(starCount);

        float starCountAch = DataSave._instance.getStar_Count();
        
        if(starCountAch == 50)
            Social.ReportProgress(GPGS.achievement_star1, 100.0f, (bool success) => { });
        else if(starCountAch == 100)
            Social.ReportProgress(GPGS.achievement_star2, 100.0f, (bool success) => { });
        else if(starCountAch == 200)
            Social.ReportProgress(GPGS.achievement_star3, 100.0f, (bool success) => { });
        else if(starCountAch == 300)
            Social.ReportProgress(GPGS.achievement_star4, 100.0f, (bool success) => { });

		if (helpTotal >= 10) {
			Social.ReportProgress(GPGS.achievement_animal1, 100.0f, (bool success) => { });
		} else if (helpTotal >= 50) {
			Social.ReportProgress(GPGS.achievement_animal2, 100.0f, (bool success) => { });
		} else if (helpTotal >= 100) {
			Social.ReportProgress(GPGS.achievement_animal3, 100.0f, (bool success) => { });
		}

		if (Record_time >= 300) {
			Social.ReportProgress(GPGS.achievement_timer1, 100.0f, (bool success) => { });
		} else if (Record_time >= 600) {
			Social.ReportProgress(GPGS.achievement_timer2, 100.0f, (bool success) => { });
		}

        switch (starCount)
			{
			case 1:
				yield return new WaitForSeconds (0.3f);
				Star [0].SetActive (true);
				AudioSource.PlayClipAtPoint (clear_star, ingameCamera.transform.position, GameManager.soundVolume);
				break;
			case 2:
				yield return new WaitForSeconds (0.3f);
				Star[0].SetActive(true);
				AudioSource.PlayClipAtPoint (clear_star, ingameCamera.transform.position, GameManager.soundVolume);

				yield return new WaitForSeconds (0.3f);
				Star[1].SetActive(true);
				AudioSource.PlayClipAtPoint (clear_star, ingameCamera.transform.position, GameManager.soundVolume);
				break;
			case 3:
				yield return new WaitForSeconds (0.3f);
				Star[0].SetActive(true);
				AudioSource.PlayClipAtPoint (clear_star, ingameCamera.transform.position, GameManager.soundVolume);
				yield return new WaitForSeconds (0.3f);
				Star[1].SetActive(true);
				AudioSource.PlayClipAtPoint (clear_star, ingameCamera.transform.position, GameManager.soundVolume);
				yield return new WaitForSeconds (0.3f);
				Star[2].SetActive(true);
				AudioSource.PlayClipAtPoint (clear_star, ingameCamera.transform.position, GameManager.soundVolume);
				break;
			}
			yield return new WaitForSeconds (0.3f);
			AudioSource.PlayClipAtPoint (clear_exit, ingameCamera.transform.position, GameManager.soundVolume);

	}


}
