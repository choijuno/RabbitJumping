using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	// ui win, lose texts
	//public GameObject gameover;
	//public GameObject gameclear;

	// status use

	//public Animation _anim;
	//public AnimationClip Jumping2;
	//public AnimationClip Jumping3;
	public int TestSkinNum;
	public GameObject[] ChaSkin;

	public GameObject gameManager;
	public Animator _anim;

	public GameObject SoundBase;
	public AudioClip bumpSound;
	public AudioClip bumpSound2;
	public AudioClip deadSound;
	public AudioClip goldSound;




	public GameObject loadManager;
	public Transform FindChild_inParent;
	public Transform StartPos;

	public PlayerCC rainCC = PlayerCC.not;

	public Bouncy bounce = Bouncy.Down;
	public float readyTime;

	public GameObject deadEffect;
	public GameObject waterDead;
	public GameObject deadBody;

	public GameObject bumpEffect;
	public GameObject waterEffect;


	//MaxHeight
	public float MaxHeight;
	float MaxHeight_in;

	//up
	public float UpKey = 1111111;

	public float UpLerp;
	float UpLerp_in;
	public float UpBounceSpeed;
	float UpBounceSpeed_in;

	//down
	public float DownKey = 1111111;

	public float DownLerp;
	float DownLerp_in;
	//public float DownBounceSpeed;
	//float DownBounceSpeed_in;

	public GameObject Camera_ingame;
	public GameObject backGround_inCamera;
	public Transform backGround_basePos;
	float rideTime_in;

	float warpTime_in;
	float warpSpeed_in;
	float warpexitTime_in;
	Transform warpHeight;
	Transform warpX;

	float stunTime_in;
	bool stunGround;

	void Awake () {
		Camera_ingame.GetComponent<GameCamera> ().direction = 1;
		//_anim = deadBody.GetComponent<Animation> ();
		if (ES2.Exists ("rabbit")) {
			TestSkinNum = ES2.Load<int> ("rabbit");
		} else {
			TestSkinNum = 0;
		}
			
		switch (TestSkinNum) {
		case 0:
			ChaSkin [0].SetActive (true);
			_anim = ChaSkin[0].GetComponent<Animator>();
			break;
		case 1:
			ChaSkin [1].SetActive (true);
			_anim = ChaSkin[1].GetComponent<Animator>();
			break;
		case 2:
			ChaSkin [2].SetActive (true);
			_anim = ChaSkin[2].GetComponent<Animator>();
			break;
		}

		transform.position = StartPos.position;
		bounce = Bouncy.Ready;

		//RESET height
		MaxHeight_in = transform.position.y + MaxHeight;

		//up
		UpLerp_in = UpLerp * 0.1f;
		UpBounceSpeed_in = UpBounceSpeed;

		//down
		DownLerp_in = DownLerp * 0.1f;
		//DownBounceSpeed_in = -DownBounceSpeed;

		StartCoroutine ("GameReady");


	}

	IEnumerator GameReady(){
		
		while (true) {
			
			yield return new WaitForSeconds (readyTime);
			_anim.SetTrigger ("JumpStart");
			bounce = Bouncy.Up;
			_anim.SetTrigger ("Up");
			GameManager.gameSet = 0;
			//Debug.Log (bounce);
			StopCoroutine ("GameReady");

		}
	}




	void Update () {

		if (!GameManager.pauseCheck) {
			// status equal to bounce
			if (bounce != Bouncy.Not) {
				switch (bounce) {

				case Bouncy.Down:
					BDown ();
					break;

				case Bouncy.Up:
					BUp ();
					break;

				case Bouncy.stun:
					BStun ();
					break;

				case Bouncy.ride:
					Bride ();
					break;

				case Bouncy.warp:
					Bwarp ();
					break;

				case Bouncy.warpexit:
					Bwarpexit ();
					break;

				}
				

				if (transform.position.y >= MaxHeight_in) {
					bumpEffect.SetActive (false);
					waterEffect.SetActive (false);
					DownLerp_in = 0;
					bounce = Bouncy.Down;
					transform.position = new Vector3 (transform.position.x, MaxHeight_in, transform.position.z);

					_anim.SetBool ("DropCheck", true);
					_anim.SetTrigger ("Down");
					//_anim.clip = Jumping3;
					//Debug.Log(_anim.clip);
					//_anim.Play ();

				}

			} else {
				BNot ();
			}
		}

	}

	void BDown(){
		//Debug.Log ("Down : " + DownLerp_in);

		DownLerp_in = Mathf.Lerp (DownLerp_in, DownLerp * 0.5f, 0.001f);

		if (DownLerp_in >= DownLerp * 0.1f) {
			DownLerp_in = DownLerp * 0.1f;
		}

		transform.position = new Vector3 (transform.position.x, transform.position.y - DownLerp_in * 0.1f, transform.position.z);
	}

	void BUp(){
		//Debug.Log ("Up : " + lerpSpeed);
		UpLerp_in = Mathf.Lerp (UpLerp_in, 0, 0.1f);


		if (UpLerp_in <= 0) {
			UpLerp_in = 0;
		}

		transform.position = new Vector3 (transform.position.x, transform.position.y + (UpBounceSpeed_in * Time.deltaTime) + UpLerp_in, transform.position.z);
	}

	void BNot(){
		//transform.position = new Vector3 (transform.position.x, transform.position.y - 0.1f * Time.deltaTime, transform.position.z);
	}

	void BStun(){
		
		if (stunTime_in >= 0) {
			stunTime_in = stunTime_in - Time.deltaTime;
		} else {
			UpLerp_in = UpLerp * 0.1f;
			bounce = Bouncy.Up;
			_anim.SetBool ("StunCheck", false);

			MaxHeight_in = transform.position.y + MaxHeight;
			GetComponent<PlayerController> ().moveStopCheck = false;
			Camera_ingame.GetComponent<GameCamera> ().direction = 1;
		}

		if (stunGround) {



		} else {

			DownLerp_in = Mathf.Lerp (DownLerp_in, DownLerp * 0.5f, 0.001f);

			if (DownLerp_in >= DownLerp * 0.1f) {
				DownLerp_in = DownLerp * 0.1f;
			}

			transform.position = new Vector3 (transform.position.x, transform.position.y - DownLerp_in * 0.1f, transform.position.z);

		}

	}

	void Bride() {
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, 0);
		rideTime_in = rideTime_in - Time.deltaTime;
		if (rideTime_in <= 0) {
			GetComponent<PlayerController> ().moveStopCheck = false;
			Camera_ingame.GetComponent<GameCamera> ().riding = false;
			transform.parent = null;
			UpLerp_in = UpLerp * 0.1f;
			bounce = Bouncy.Up;
		}
	}

	void Bwarp() {
		transform.position = new Vector3 (transform.position.x, warpHeight.position.y, 0);




		if (warpTime_in <= 0) {

			if (warpX.position.x >= warpHeight.position.x) {
				UpLerp_in = Mathf.Lerp (UpLerp_in, 0, 0.1f);

				if (UpLerp_in <= 0) {
					UpLerp_in = 0;
				}

				warpTime_in = 0;
				transform.position = new Vector3 (transform.position.x - (warpSpeed_in * Time.deltaTime) - UpLerp_in, transform.position.y, transform.position.z);
			} else {
				UpLerp_in = Mathf.Lerp (UpLerp_in, 0, 0.1f);

				if (UpLerp_in <= 0) {
					UpLerp_in = 0;
				}

				warpTime_in = 0;
				transform.position = new Vector3 (transform.position.x + (warpSpeed_in * Time.deltaTime) + UpLerp_in, transform.position.y, transform.position.z);
			}
			


		} else {
			warpTime_in = warpTime_in - Time.deltaTime;
		}
	}

	void Bwarpexit() {

		if (warpexitTime_in <= 0) {
			warpexitTime_in = 0;
			GetComponent<PlayerController> ().moveStopCheck = false;
			Camera_ingame.GetComponent<GameCamera> ().direction = 1;
			deadBody.SetActive (true);
			_anim.SetTrigger ("JumpStart");
			_anim.SetTrigger ("Up");


			UpLerp_in = Mathf.Lerp (UpLerp_in, 0, 0.1f);


			if (UpLerp_in <= 0) {
				UpLerp_in = 0;
			}

			transform.position = new Vector3 (transform.position.x, transform.position.y + (UpBounceSpeed_in * Time.deltaTime) + UpLerp_in, transform.position.z);

			if (transform.position.y >= MaxHeight_in) {
				bumpEffect.SetActive (false);
				waterEffect.SetActive (false);
				DownLerp_in = 0;
				bounce = Bouncy.Down;
				transform.position = new Vector3 (transform.position.x, MaxHeight_in, transform.position.z);
			}

		} else {
			warpexitTime_in = warpexitTime_in - Time.deltaTime;
		}
	}

    // onTrigger Grounds ?
    void OnTriggerEnter(Collider obj)
    {
        //Debug.Log (obj.name);
        switch (bounce)
        {

            case Bouncy.Wait:

                if (obj.CompareTag("rain")) {
				_anim.SetBool("RainCheck",true);
                    rainCC = PlayerCC.heavy;
                }

                break;

			case Bouncy.Down:
				if (obj.CompareTag ("gold")) {
				GameManager.Money_ingame += 10;
				AudioSource.PlayClipAtPoint(goldSound, SoundBase.transform.position);
				}

                if (obj.CompareTag("warp"))
                {
					_anim.SetBool ("DropCheck", false);
                    MaxHeight_in = 5;
                    GetComponent<PlayerController>().moveStopCheck = true;
                    deadBody.SetActive(false);
					Camera_ingame.GetComponent<GameCamera> ().direction = obj.transform.parent.GetComponent<Tunnel> ().direction;
                    Camera_ingame.GetComponent<GameCamera>().waitTime_in = obj.transform.parent.GetComponent<Tunnel>().waitTime;
                    Camera_ingame.GetComponent<GameCamera>().rideSpeed_in = obj.transform.parent.GetComponent<Tunnel>().Speed * 0.001f;
                    Camera_ingame.GetComponent<GameCamera>().riding = true;
                    warpTime_in = obj.transform.parent.GetComponent<Tunnel>().waitTime;
                    warpSpeed_in = obj.transform.parent.GetComponent<Tunnel>().Speed * 0.1f;
                    warpexitTime_in = obj.transform.parent.GetComponent<Tunnel>().exitTime;
                    warpX = obj.transform;
                    bounce = Bouncy.warp;

                    UpLerp_in = UpLerp * 0.1f;

                    warpHeight = obj.transform.parent.GetComponent<Tunnel>().exit.transform;

                }

                if (obj.CompareTag("rain"))
                {
				_anim.SetBool("RainCheck",true);
                    rainCC = PlayerCC.heavy;
                }

                if (obj.CompareTag("ride"))
                {
				GetComponent<PlayerController>().moveStopCheck = true;
				_anim.SetBool ("DropCheck", false);
                    switch (obj.name.Substring(0, 4))
                    {
                        case "elep":
                            if (bounce == Bouncy.Down)
                            {
                                bounce = Bouncy.ride;
                                MaxHeight_in = transform.position.y + MaxHeight * 0.3f;
                                Camera_ingame.GetComponent<GameCamera>().riding = true;
                                Camera_ingame.GetComponent<GameCamera>().waitTime_in = obj.transform.parent.GetComponent<Elephant>().waitTime;
                                Camera_ingame.GetComponent<GameCamera>().rideSpeed_in = obj.transform.parent.GetComponent<Elephant>().runSpeed * 0.001f;
                                rideTime_in = obj.transform.parent.GetComponent<Elephant>().runTime;
                                transform.parent = obj.transform.parent.transform;
                                transform.localPosition = new Vector3(obj.transform.parent.GetComponent<Elephant>().Pos.transform.localPosition.x, obj.transform.parent.GetComponent<Elephant>().Pos.transform.localPosition.y, 0);
                                obj.transform.parent.GetComponent<Elephant>().stat = elephantStatus.wait;
                            }
                            break;
                        case "hawk":
                            bounce = Bouncy.ride;
                            MaxHeight_in = transform.position.y + MaxHeight * 0.3f;
                            Camera_ingame.GetComponent<GameCamera>().riding = true;
                            Camera_ingame.GetComponent<GameCamera>().waitTime_in = obj.transform.parent.GetComponent<Hawk>().waitTime;
                            Camera_ingame.GetComponent<GameCamera>().rideSpeed_in = obj.transform.parent.GetComponent<Hawk>().runSpeed * 0.001f;
                            rideTime_in = obj.transform.parent.GetComponent<Hawk>().runTime;
                            transform.parent = obj.transform.parent.transform;
                            transform.localPosition = new Vector3(obj.transform.parent.GetComponent<Hawk>().Pos.transform.localPosition.x, obj.transform.parent.GetComponent<Hawk>().Pos.transform.localPosition.y, 0);
                            obj.transform.parent.GetComponent<Hawk>().stat = hawkStatus.wait;
                            break;
                    }
                }

                if (obj.CompareTag("jump"))
                {
				
                    if (!obj.CompareTag("Untagged"))
                    {
                        if (GameManager.gameSet == 0)
                            bumped();
                    }
                    UpLerp_in = UpLerp * 0.15f;
                    bounce = Bouncy.Up;
				MaxHeight_in = transform.position.y + MaxHeight * 1.5f;
				_anim.SetBool ("DropCheck", false);
				_anim.SetBool("PowerDown",true);
				//_anim.SetTrigger ("BumpJump");
				_anim.SetTrigger ("Up");

				//_anim.clip = Jumping2;
				//_anim.Play ();
                }

                if (obj.CompareTag("ground"))
			{
                    if (!obj.CompareTag("Untagged"))
                    {
                        if (GameManager.gameSet == 0)
                            bumped();
                    }

                    switch (rainCC)
                    {
                        case PlayerCC.not:
                            //Debug.Log ("not!");
                            UpLerp_in = UpLerp * 0.1f;
                            bounce = Bouncy.Up;
                            MaxHeight_in = transform.position.y + MaxHeight;
                            break;
                        case PlayerCC.heavy:
                            //Debug.Log ("heavy!");
                            UpLerp_in = (UpLerp * 0.1f) / 2;
                            bounce = Bouncy.Up;
                            MaxHeight_in = transform.position.y + (MaxHeight / 2);
                            break;
                        case PlayerCC.high:
                            break;
                        case PlayerCC.reverse:
                            break;
                        case PlayerCC.bug:
                            break;
                        case PlayerCC.horizon:
                            break;
                        case PlayerCC.riding:
                            break;
				}

				_anim.SetBool ("DropCheck", false);
				_anim.SetBool("PowerDown",false);
				//_anim.SetTrigger ("BumpJump");
				_anim.SetTrigger ("Up");
				//_anim.clip = Jumping2;
				//_anim.Play ();
                }

                if (obj.name == "water")
                {
                    AudioSource.PlayClipAtPoint(deadSound, SoundBase.transform.position);
					waterbumped ();
				//waterDead.SetActive(true);

                }

				if (obj.CompareTag("stun"))
				{
				Debug.Log ("DownStun");
				_anim.SetBool ("StunCheck", true);
				_anim.SetTrigger ("DownStun");
				stunGround = false;
				GetComponent<PlayerController>().moveStopCheck = true;
				Camera_ingame.GetComponent<GameCamera> ().direction = 0;
				stunTime_in = obj.transform.parent.GetComponent<Tree_R> ().stunTime;
					DownLerp_in = 0;
					bounce = Bouncy.stun;

				}

                if (obj.CompareTag("dead"))
                {
                    GameManager.gameSet = 2;
                    Debug.Log(bounce);
                    //gameover.SetActive (true);
                    deadBody.SetActive(false);
                    deadEffect.SetActive(true);
                    Invoke("resetgame", 2f);
                    bounce = Bouncy.Not;
                }

                if (obj.CompareTag("clear"))
                {

				GameManager.gameSet = 1;
                    //게임클리어.
                    /*if (GameManager.TestNum == 001)
                    {
                        Social.ReportProgress(GPGS.achievement_test1, 100.0f, (bool success) =>{});
                    }else if(GameManager.TestNum == 002)
                    {
                        Social.ReportProgress(GPGS.achievement_test1, 100.0f, (bool success) => {});
                    }else if(GameManager.TestNum == 003)
                    {
                        Social.ReportProgress(GPGS.achievement_test1, 100.0f, (bool success) => {});
                    }*/

				
                    //gameclear.SetActive (true);
				if (Application.loadedLevelName == "Edit") {
                    Invoke("resetgame", 2f);
				}
                    bounce = Bouncy.Not;
				//_anim.SetTrigger ("BumpJump");
					_anim.SetBool ("DropCheck", false);
					_anim.SetTrigger ("GameSet");
                }
                break;
            case Bouncy.Up:
				if (obj.CompareTag ("gold")) {
				GameManager.Money_ingame += 10;
				AudioSource.PlayClipAtPoint(goldSound, SoundBase.transform.position);
				Debug.Log ("Upgold");
				}

                if (obj.CompareTag("rain"))
                {
				_anim.SetBool("RainCheck",true);
                    rainCC = PlayerCC.heavy;
                }

                if (obj.CompareTag("ride"))
                {
                    GetComponent<PlayerController>().moveStopCheck = true;
                    switch (obj.name.Substring(0, 4))
                    {
                        case "elep":
                            if (bounce == Bouncy.Down)
                            {
                                bounce = Bouncy.ride;
                                MaxHeight_in = transform.position.y + MaxHeight * 0.3f;
                                Camera_ingame.GetComponent<GameCamera>().riding = true;
                                Camera_ingame.GetComponent<GameCamera>().waitTime_in = obj.transform.parent.GetComponent<Elephant>().waitTime;
                                Camera_ingame.GetComponent<GameCamera>().rideSpeed_in = obj.transform.parent.GetComponent<Elephant>().runSpeed * 0.001f;
                                rideTime_in = obj.transform.parent.GetComponent<Elephant>().runTime;
                                transform.parent = obj.transform.parent.transform;
                                transform.localPosition = new Vector3(obj.transform.parent.GetComponent<Elephant>().Pos.transform.localPosition.x, obj.transform.parent.GetComponent<Elephant>().Pos.transform.localPosition.y, 0);
                                obj.transform.parent.GetComponent<Elephant>().stat = elephantStatus.wait;
                            }
                            break;
                        case "hawk":
                            bounce = Bouncy.ride;
                            MaxHeight_in = transform.position.y + MaxHeight * 0.3f;
                            Camera_ingame.GetComponent<GameCamera>().riding = true;
                            Camera_ingame.GetComponent<GameCamera>().waitTime_in = obj.transform.parent.GetComponent<Hawk>().waitTime;
                            Camera_ingame.GetComponent<GameCamera>().rideSpeed_in = obj.transform.parent.GetComponent<Hawk>().runSpeed * 0.001f;
                            rideTime_in = obj.transform.parent.GetComponent<Hawk>().runTime;
                            transform.parent = obj.transform.parent.transform;
                            transform.localPosition = new Vector3(obj.transform.parent.GetComponent<Hawk>().Pos.transform.localPosition.x, obj.transform.parent.GetComponent<Hawk>().Pos.transform.localPosition.y, 0);
                            obj.transform.parent.GetComponent<Hawk>().stat = hawkStatus.wait;
                            break;
                    }
                }

				if (obj.CompareTag("stun"))
				{
				Debug.Log ("UpStun");
				stunGround = false;
				_anim.SetBool ("StunCheck", true);
					GetComponent<PlayerController>().moveStopCheck = true;
					Camera_ingame.GetComponent<GameCamera> ().direction = 0;
					stunTime_in = obj.transform.parent.GetComponent<Tree_R> ().stunTime;
					DownLerp_in = 0;
					bounce = Bouncy.stun;

				}

                if (obj.CompareTag("dead"))
                {
                    GameManager.gameSet = 2;
                    Debug.Log(bounce);
                    //gameover.SetActive (true);
                    deadBody.SetActive(false);
                    deadEffect.SetActive(true);
                    Invoke("resetgame", 2f);
                    bounce = Bouncy.Not;
                }

                break;
            case Bouncy.Not:

                if (obj.CompareTag("rain"))
                {
				_anim.SetBool("RainCheck",true);
                    rainCC = PlayerCC.heavy;
                }

                break;
			case Bouncy.stun:
				if (obj.CompareTag ("gold")) {
				GameManager.Money_ingame += 10;
				AudioSource.PlayClipAtPoint(goldSound, SoundBase.transform.position);
				}

				if (obj.CompareTag ("ground")) {
				stunGround = true;
				}

                if (obj.CompareTag("rain"))
                {
				_anim.SetBool("RainCheck",true);
                    rainCC = PlayerCC.heavy;
                }

				if (obj.CompareTag("dead"))
				{
					GameManager.gameSet = 2;
					Debug.Log(bounce);
					//gameover.SetActive (true);
					deadBody.SetActive(false);
					deadEffect.SetActive(true);
					Invoke("resetgame", 2f);
					bounce = Bouncy.Not;
				}

                break;
            case Bouncy.Ready:

                if (obj.CompareTag("rain"))
                {
				_anim.SetBool("RainCheck",true);
                    rainCC = PlayerCC.heavy;
                }

                break;
            case Bouncy.ride:
				
				if (obj.CompareTag ("gold")) {
					GameManager.Money_ingame += 10;
					AudioSource.PlayClipAtPoint(goldSound, SoundBase.transform.position);
				}

                if (obj.CompareTag("rain"))
                {
				_anim.SetBool("RainCheck",true);
                    rainCC = PlayerCC.heavy;
                }

                break;
            case Bouncy.warp:

                if (obj.CompareTag("warpexit"))
                {
					Camera_ingame.GetComponent<GameCamera> ().direction = 0;
                    warpexitTime_in = obj.transform.parent.transform.parent.GetComponent<Tunnel>().exitTime;

                    Camera_ingame.GetComponent<GameCamera>().riding = false;
                    MaxHeight_in = transform.position.y + MaxHeight;
                    UpBounceSpeed_in = UpBounceSpeed;
                    UpLerp_in = UpLerp * 0.1f;
                    bounce = Bouncy.warpexit;
				//_anim.SetTrigger ("BumpJump");
				//_anim.clip = Jumping2;
				//_anim.Play ();
                }

                break;
            case Bouncy.warpexit:
				
                if (obj.CompareTag("rain"))
                {
				_anim.SetBool("RainCheck",true);
                    rainCC = PlayerCC.heavy;
                }

                break;

        }



    }

    void OnTriggerExit(Collider obj) {

		if (obj.CompareTag("rain")) {
			_anim.SetBool("RainCheck",false);
			rainCC = PlayerCC.not;
			/*

			//RESET height
			MaxHeight_in = transform.position.y + MaxHeight;

			//up
			UpLerp_in = UpLerp * 0.1f;
			UpBounceSpeed_in = UpBounceSpeed;
			*/
		}



	}


	// hit Ground Effect
	void bumped() {
		int rand = Random.Range (0, 10);

			AudioSource.PlayClipAtPoint (bumpSound, SoundBase.transform.position);

			bumpEffect.transform.position = this.transform.position;
			bumpEffect.SetActive (true);

		switch (rand) {
		case 0:
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
			break;
		case 8:
		case 9:
			//AudioSource.PlayClipAtPoint (bumpSound2, SoundBase.transform.position);
			break;
		default:
			break;
		}
	}

	void waterbumped() {

		waterEffect.transform.position = this.transform.position;
		waterEffect.SetActive (true);

	}


	//resetGame
	public void resetgame(){
		


		if (Application.loadedLevelName != "Edit") {

			GameManager.gameSet = 2;

		} else {
			GetComponent<PlayerController> ().moveStopCheck = false;

			backGround_inCamera.transform.position = new Vector3 (backGround_basePos.transform.position.x, backGround_basePos.transform.position.y, backGround_basePos.transform.position.z);
			FindChild_inParent = FindChild_inParent.GetComponentInChildren<Transform>();
			foreach (Transform child in FindChild_inParent) {
				foreach (Transform child_inChild in child) {
					
					switch (child.name.Substring (0, 7)) {
					case "1000":
						break;
					case "1001":
						break;
					case "1010021":
						if (child_inChild.name.Substring (0, 4) == "brea") {
							child_inChild.GetComponent<objColider> ().reset = true;
						}
						break;
					case "2000":
						break;
					case "1040021":
						if (child_inChild.name.Substring (0, 5) == "croco") {
							child_inChild.GetComponent<objColider> ().reset = true;
						}
						break;

					}
				}

			}
			//loadManager.GetComponent<LoadManager> ().loadCheck = true;
			transform.position = StartPos.position;
			bounce = Bouncy.Ready;
			deadBody.SetActive(true);
			deadEffect.SetActive(false);
			waterDead.SetActive (false);
			//RESET height
			MaxHeight_in = transform.position.y + MaxHeight;

			//up
			UpLerp_in = UpLerp * 0.1f;
			UpBounceSpeed_in = UpBounceSpeed;

			//down
			DownLerp_in = DownLerp * 0.1f;
			//DownBounceSpeed_in = -DownBounceSpeed;

			StartCoroutine ("GameReady");
		}
	}
}
