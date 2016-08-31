using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadManager : MonoBehaviour {
	



	public Text DataPathTest;
	public Text DataPathTest2;


	public int stageNum;
	public string stageNum_string;
	public Text inputNum_txt;

	public bool loadCheck;
	public GameObject playerBody;
	public GameObject Edit;
	public Transform loadParent;

	public GameObject basePos;
	public GameObject StartPos;





	//01Ground
	public GameObject ground;
	public GameObject ground_2;
	public GameObject breakground;
	public GameObject wood;

	//02ActionObj
	public GameObject jumpPoint;
	public GameObject moleTunnel;
	public GameObject moleTunnel_2;

	//03StayEnemy
	public GameObject fish;
	public GameObject monkey;
	public GameObject plant;
	public GameObject rhino;
	public GameObject tree;
	public GameObject tree_R;

	//04MoveEnemy
	public GameObject hedgedog;
	public GameObject crocodile;
	public GameObject spider_1;
	public GameObject spider_2;
	public GameObject spider_3;

	//05Items
	public GameObject gold;

	//06Hurddle
	public GameObject cloud;
	public GameObject cloud_2;
	public GameObject hive;
	public GameObject poison;

	//07Riding
	public GameObject elephant;
	public GameObject hawk;

	//08Cage
	public GameObject Cage;

	//99Pos
	public GameObject ClearPoint;

	GameObject _tmp;

	string[] objPos_dataArr;
	string[] objPos_component_dataArr;

	float[] objPos_dataIn;
	float[] objPos_component_dataIn;


	// Use this for initialization
	void Awake () {
		



		if (Application.loadedLevelName == "Edit") {
			
			stageNum = 0;
		} else {
			if (Application.loadedLevelName == "TestGame"){
				stageNum = GameManager.TestNum;
			}

		}
		//load
		loadCheck = true;



	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "Edit") {
			if (inputNum_txt.text != "") {
				stageNum = int.Parse (inputNum_txt.text);
				//Debug.Log (stageNum);
			} else {
				//stageNum = 0;
			}
		}
		//Debug.Log (stageNum_string);

		//stageNum = int.Parse(stageNum_txt.text);


		if (loadCheck) {

			ES2Settings settings = new ES2Settings();
			settings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			//Debug.Log ("jar:file://" + Application.dataPath + "/stage/stage" + stageNum + "/_obj.txt");
			if (ES2.Exists ("/stage/stage" + stageNum + "/_obj.bytes", settings)) {
				
				//DataPathTest.text = "DataPath : " + Application.dataPath + "/resoureces/stage/stage" + stageNum + "/_obj.txt";
				//DataPathTest2.text = "DataPath2 : " + "jar:file://" + Application.dataPath + "!/Assets/" + "resoureces/stage/stage" + stageNum + "/_obj.txt";

				//DataPathTest.text = "1";

				int k = 0;

				Debug.Log ("Load " + stageNum + "Stage.");
				string[] objPos_dataArr = ES2.Load<string> ("/stage/stage" + stageNum + "/_obj.bytes", settings).Split (',');
				string[] objPos_component_dataArr = ES2.Load<string> ("/stage/stage" + stageNum + "/_obj_Component.bytes", settings).Split (',');

				objPos_dataIn = new float[objPos_dataArr.Length];
				objPos_component_dataIn = new float[objPos_component_dataArr.Length];

				for (int j = 0; j < objPos_dataArr.Length; j++) {
					objPos_dataIn [j] = System.Convert.ToSingle (objPos_dataArr [j]);
				}

				for (int j = 0; j < objPos_component_dataArr.Length; j++) {
					objPos_component_dataIn [j] = System.Convert.ToSingle (objPos_component_dataArr [j]);
				}

				for (int i = 0; i < objPos_dataArr.Length; i += 3) {
					Debug.Log ("!!!!");
					switch ("" + objPos_dataIn [i]) {
					//99Pos
					case "1990011":
						_tmp = (GameObject)Instantiate (StartPos, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						playerBody.GetComponent<PlayerMove> ().StartPos = _tmp.gameObject.transform;
						break;

				//01Ground
					case "1010011":
						_tmp = (GameObject)Instantiate (ground, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1010012":
						_tmp = (GameObject)Instantiate (ground_2, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1010021":
						_tmp = (GameObject)Instantiate (breakground, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						if (objPos_component_dataIn [k + 1] == 1)
							_tmp.GetComponent<objMovement> ().havehp = true;
						_tmp.GetComponent<objMovement> ().standHp = (int)objPos_component_dataIn [k + 2];
						_tmp.GetComponent<objMovement> ().angryHp = (int)objPos_component_dataIn [k + 3];
						if (objPos_component_dataIn [k + 4] == 1)
							_tmp.GetComponent<objMovement> ().move = true;
						_tmp.GetComponent<objMovement> ().L_Speed = objPos_component_dataIn [k + 5];
						_tmp.GetComponent<objMovement> ().R_Speed = objPos_component_dataIn [k + 6];
						_tmp.GetComponent<objMovement> ().L_Range = objPos_component_dataIn [k + 7];
						_tmp.GetComponent<objMovement> ().R_Range = objPos_component_dataIn [k + 8];
						if (objPos_component_dataIn [k + 9] == 1)
							_tmp.GetComponent<objMovement> ().jump = true;
						_tmp.GetComponent<objMovement> ().waitTime_jump = objPos_component_dataIn [k + 10];
						_tmp.GetComponent<objMovement> ().maxHeight = objPos_component_dataIn [k + 11];
						_tmp.GetComponent<objMovement> ().U_Speed = objPos_component_dataIn [k + 12];
						_tmp.GetComponent<objMovement> ().U_Lerp = objPos_component_dataIn [k + 13];
						_tmp.GetComponent<objMovement> ().down_Lerp = objPos_component_dataIn [k + 14];
						break;
					case "1010031":
						_tmp = (GameObject)Instantiate (wood, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;

				//02ActionObj
					case "1020011":
						_tmp = (GameObject)Instantiate (jumpPoint, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1020021":
						_tmp = (GameObject)Instantiate (moleTunnel, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1020022":
						_tmp = (GameObject)Instantiate (moleTunnel_2, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;

				//03Stayenemy
					case "1030012":
					case "1030011":
						_tmp = (GameObject)Instantiate (fish, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						if (objPos_component_dataIn [k + 1] == 1)
							_tmp.GetComponent<objMovement> ().havehp = true;
						_tmp.GetComponent<objMovement> ().standHp = (int)objPos_component_dataIn [k + 2];
						_tmp.GetComponent<objMovement> ().angryHp = (int)objPos_component_dataIn [k + 3];
						if (objPos_component_dataIn [k + 4] == 1)
							_tmp.GetComponent<objMovement> ().move = true;
						_tmp.GetComponent<objMovement> ().L_Speed = objPos_component_dataIn [k + 5];
						_tmp.GetComponent<objMovement> ().R_Speed = objPos_component_dataIn [k + 6];
						_tmp.GetComponent<objMovement> ().L_Range = objPos_component_dataIn [k + 7];
						_tmp.GetComponent<objMovement> ().R_Range = objPos_component_dataIn [k + 8];
						if (objPos_component_dataIn [k + 9] == 1)
							_tmp.GetComponent<objMovement> ().jump = true;
						_tmp.GetComponent<objMovement> ().waitTime_jump = objPos_component_dataIn [k + 10];
						_tmp.GetComponent<objMovement> ().maxHeight = objPos_component_dataIn [k + 11];
						_tmp.GetComponent<objMovement> ().U_Speed = objPos_component_dataIn [k + 12];
						_tmp.GetComponent<objMovement> ().U_Lerp = objPos_component_dataIn [k + 13];
						_tmp.GetComponent<objMovement> ().down_Lerp = objPos_component_dataIn [k + 14];
						break;
					case "1030021":
						_tmp = (GameObject)Instantiate (monkey, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1030031":
						_tmp = (GameObject)Instantiate (plant, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1030041":
						_tmp = (GameObject)Instantiate (rhino, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						if (objPos_component_dataIn [k + 1] == 1)
							_tmp.GetComponent<objMovement> ().havehp = true;
						_tmp.GetComponent<objMovement> ().standHp = (int)objPos_component_dataIn [k + 2];
						_tmp.GetComponent<objMovement> ().angryHp = (int)objPos_component_dataIn [k + 3];
						if (objPos_component_dataIn [k + 4] == 1)
							_tmp.GetComponent<objMovement> ().move = true;
						_tmp.GetComponent<objMovement> ().L_Speed = objPos_component_dataIn [k + 5];
						_tmp.GetComponent<objMovement> ().R_Speed = objPos_component_dataIn [k + 6];
						_tmp.GetComponent<objMovement> ().L_Range = objPos_component_dataIn [k + 7];
						_tmp.GetComponent<objMovement> ().R_Range = objPos_component_dataIn [k + 8];
						if (objPos_component_dataIn [k + 9] == 1)
							_tmp.GetComponent<objMovement> ().jump = true;
						_tmp.GetComponent<objMovement> ().waitTime_jump = objPos_component_dataIn [k + 10];
						_tmp.GetComponent<objMovement> ().maxHeight = objPos_component_dataIn [k + 11];
						_tmp.GetComponent<objMovement> ().U_Speed = objPos_component_dataIn [k + 12];
						_tmp.GetComponent<objMovement> ().U_Lerp = objPos_component_dataIn [k + 13];
						_tmp.GetComponent<objMovement> ().down_Lerp = objPos_component_dataIn [k + 14];
						break;
					case "1030051":
						_tmp = (GameObject)Instantiate (tree, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1030052":
						_tmp = (GameObject)Instantiate (tree_R, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;

				//04Moveenemy
					case "1040011":
						_tmp = (GameObject)Instantiate (hedgedog, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1040021":
						_tmp = (GameObject)Instantiate (crocodile, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						if (objPos_component_dataIn [k + 1] == 1)
							_tmp.GetComponent<objMovement> ().havehp = true;
						_tmp.GetComponent<objMovement> ().standHp = (int)objPos_component_dataIn [k + 2];
						_tmp.GetComponent<objMovement> ().angryHp = (int)objPos_component_dataIn [k + 3];
						if (objPos_component_dataIn [k + 4] == 1)
							_tmp.GetComponent<objMovement> ().move = true;
						_tmp.GetComponent<objMovement> ().L_Speed = objPos_component_dataIn [k + 5];
						_tmp.GetComponent<objMovement> ().R_Speed = objPos_component_dataIn [k + 6];
						_tmp.GetComponent<objMovement> ().L_Range = objPos_component_dataIn [k + 7];
						_tmp.GetComponent<objMovement> ().R_Range = objPos_component_dataIn [k + 8];
						if (objPos_component_dataIn [k + 9] == 1)
							_tmp.GetComponent<objMovement> ().jump = true;
						_tmp.GetComponent<objMovement> ().waitTime_jump = objPos_component_dataIn [k + 10];
						_tmp.GetComponent<objMovement> ().maxHeight = objPos_component_dataIn [k + 11];
						_tmp.GetComponent<objMovement> ().U_Speed = objPos_component_dataIn [k + 12];
						_tmp.GetComponent<objMovement> ().U_Lerp = objPos_component_dataIn [k + 13];
						_tmp.GetComponent<objMovement> ().down_Lerp = objPos_component_dataIn [k + 14];
						break;
					case "1040031":
						_tmp = (GameObject)Instantiate (spider_1, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1040032":
						_tmp = (GameObject)Instantiate (spider_2, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1040033":
						_tmp = (GameObject)Instantiate (spider_3, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
				//05Item
					case "1050011":
						_tmp = (GameObject)Instantiate (gold, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
				//06Hurddle
					case "1060011":
						_tmp = (GameObject)Instantiate (cloud, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1060012":
						_tmp = (GameObject)Instantiate (cloud_2, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1060021":
						_tmp = (GameObject)Instantiate (hive, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1060031":
						_tmp = (GameObject)Instantiate (poison, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;

				//07Riding
					case "1070011":
						_tmp = (GameObject)Instantiate (elephant, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					case "1070021":
						_tmp = (GameObject)Instantiate (hawk, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;

				//08Cage
					case "1080011":
						_tmp = (GameObject)Instantiate (Cage, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;

					case "1990021":
						_tmp = (GameObject)Instantiate (ClearPoint, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
						break;
					}



					_tmp.transform.parent = loadParent.transform;

					k+=15;
				}

				loadCheck = false;
				/*
		if (!Edit.GetComponent<EditSetting> ().LoadManager) {
			loadParent.gameObject.SetActive (false);
			this.gameObject.SetActive (false);
		}
*/
			} else {
				Debug.Log ("No Save File");
				//_tmp = (GameObject)Instantiate (StartPos, new Vector3 (objPos_dataIn [i + 1], objPos_dataIn [i + 2], 0), Quaternion.identity) as GameObject;
				//playerBody.GetComponent<PlayerMove> ().StartPos = _tmp.gameObject.transform;
				loadCheck = false;
			}
				

		}



	}



}
