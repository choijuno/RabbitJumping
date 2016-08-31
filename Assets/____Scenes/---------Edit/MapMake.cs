using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapMake : MonoBehaviour {
	public GameObject playerBody;
	public GameObject basePos;
	public GameObject loadManager;

	public static bool InputCheck;

	public GameObject OpenPanel;
	public GameObject ClosePanel;

	public GameObject CloseBtn;

	public GameObject editSetting;
	public Text Input_txt;
	public Text stageNum_txt;

	public Transform FindChild_inParent;
	public Transform FindChild_inChild;

	public GameObject Saving;
	public GameObject Save_Complate;


	float[] objPos_data;
	float[] objPos_Component_data;
	//float[] testSpeed;


	float[] _data;
	//float[] _datain;

	string _tmpStr;


	public void LoadStageBtn(){
		BtnOn ();
		CloseBtn.SetActive (false);
	}

	public void LoadStage(){
		DeleteMap ();
		loadManager.GetComponent<LoadManager> ().loadCheck = true;
		closePanel ();
		CloseBtn.SetActive (true);
		stageNum_txt.text = "Stage " + loadManager.GetComponent<LoadManager>().stageNum;

	}





	public void SaveStageBtn(){
		BtnOn ();
		CloseBtn.SetActive (false);
	}

	public void SaveStage(){
		saveEditorBtn ();
		closePanel ();
		CloseBtn.SetActive (true);
	}




	//delete!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	public void DeleteAllBtn(){
		BtnOn ();
		CloseBtn.SetActive (false);
	}

	public void DeleteAll(){
		DeleteMap2 ();
		closePanel ();
		CloseBtn.SetActive (true);
	}


	public void NoBtn(){
		CloseBtn.SetActive (true);
		closePanel ();
	}



	void closePanel(){
		ClosePanel.SetActive (false);
	}




	public void AnimationOnBtn(){

	}

	public void AnimationOffBtn(){

	}

	void BtnOn(){
		OpenPanel.SetActive (true);
	}
	void BtnOff(){
		OpenPanel.SetActive (false);
	}

	void DeleteMap2(){
		playerBody.GetComponent<PlayerMove> ().StartPos = basePos.transform;
		FindChild_inParent = FindChild_inParent.GetComponentInChildren<Transform>();
		foreach (Transform child in FindChild_inParent) {
			if (child.name == "1990011StartPos(Clone)") {

			} else {
				Destroy (child.gameObject);
			}
		}

	}

	void DeleteMap(){
		playerBody.GetComponent<PlayerMove> ().StartPos = basePos.transform;
		FindChild_inParent = FindChild_inParent.GetComponentInChildren<Transform>();
		foreach (Transform child in FindChild_inParent) {
			Destroy (child.gameObject);

		}

	}


	void saveEditorBtn(){
		//_tmpStr = null
		_tmpStr = "";

		//find_Child
		Find_Child ();

		//objPos = [Transform.childCount]
		objPos_data = new float[FindChild_inParent.childCount*3];

		objPos_Component_data = new float[FindChild_inParent.childCount*15];

		//_data = [Transform.childCount]
		_data = new float[FindChild_inParent.childCount];

		//objPos_data[i] = {num,pos.x,pos.y,...}
		obj_NumPos();



		for(int i = 0; i < objPos_data.Length; i++){
			
			_tmpStr = _tmpStr + objPos_data [i];
			//Debug.Log (i);
			if (i < objPos_data.Length -1) {
				_tmpStr = _tmpStr + ",";
			}
		}
		ES2.Save(_tmpStr,Application.dataPath + "/Resources/stage/stage"+ loadManager.GetComponent<LoadManager>().stageNum +"/_obj.bytes");

		//ES2.Save(_tmpStr,Application.dataPath + "/stage/stage"+ loadManager.GetComponent<LoadManager>().stageNum +"/_obj.txt");
		Debug.Log (_tmpStr);



		obj_Component ();
		_tmpStr = "";


		for(int i = 0; i < objPos_Component_data.Length; i++){

			_tmpStr = _tmpStr + objPos_Component_data [i];
			//Debug.Log (i);
			if (i < objPos_Component_data.Length -1) {
				_tmpStr = _tmpStr + ",";
			}
		}
		ES2.Save(_tmpStr,Application.dataPath + "/Resources/stage/stage"+ loadManager.GetComponent<LoadManager>().stageNum +"/_obj_Component.bytes");
		//ES2.Save(_tmpStr,Application.dataPath + "/stage/stage"+ loadManager.GetComponent<LoadManager>().stageNum +"/_obj_Component.txt");
		Debug.Log (_tmpStr);


	}




	void Find_Child(){
		FindChild_inParent = FindChild_inParent.GetComponentInChildren<Transform>();
		Debug.Log ("오브젝트 수 : " + FindChild_inParent.childCount);
	}

	void Find_Child_inChild(){
		FindChild_inChild = FindChild_inChild.GetComponentInChildren<Transform>();
	}

	void obj_NumPos(){
		int i = 0;
		foreach (Transform child in FindChild_inParent) {
			for (int j = 0; j < 3; j++) {
				
				switch (j) {
				case 0:
					objPos_data [i] = System.Convert.ToSingle (child.name.Substring (0, 7));
					break;
				case 1:
					objPos_data[i] = child.transform.position.x;
					break;
				case 2:
					objPos_data[i] = child.transform.position.y;
					break;
				}
				Debug.Log ("position : " + objPos_data[i]);
				i++;
			}

		}
	}


	void obj_Component(){
		int i = 0;
		foreach (Transform child in FindChild_inParent) {
			for (int j = 0; j < 15; j++) {

				switch (j) {
				case 0:
					objPos_Component_data[i] = System.Convert.ToSingle (child.name.Substring (0, 7));
					break;
				case 1:
					if (child.GetComponent<objMovement> ().havehp) {
						objPos_Component_data [i] = 1;
					} else {
						objPos_Component_data [i] = 0;
					}
					break;
				case 2:
					objPos_Component_data[i] = child.GetComponent<objMovement>().standHp;
					break;
				case 3:
					objPos_Component_data[i] = child.GetComponent<objMovement>().angryHp;
					break;
				case 4:
					if (child.GetComponent<objMovement> ().move) {
						objPos_Component_data [i] = 1;
					} else {
						objPos_Component_data [i] = 0;
					}
					break;
				case 5:
					objPos_Component_data[i] = child.GetComponent<objMovement>().L_Speed;
					break;
				case 6:
					objPos_Component_data[i] = child.GetComponent<objMovement>().R_Speed;
					break;
				case 7:
					objPos_Component_data[i] = child.GetComponent<objMovement>().L_Range;
					break;
				case 8:
					objPos_Component_data[i] = child.GetComponent<objMovement>().R_Range;
					break;
				case 9:
					if (child.GetComponent<objMovement> ().jump) {
						objPos_Component_data [i] = 1;
					} else {
						objPos_Component_data [i] = 0;
					}
					break;
				case 10:
					objPos_Component_data[i] = child.GetComponent<objMovement>().waitTime_jump;
					break;
				case 11:
					objPos_Component_data[i] = child.GetComponent<objMovement>().maxHeight;
					break;
				case 12:
					objPos_Component_data[i] = child.GetComponent<objMovement>().U_Speed;
					break;
				case 13:
					objPos_Component_data[i] = child.GetComponent<objMovement>().U_Lerp;
					break;
				case 14:
					objPos_Component_data[i] = child.GetComponent<objMovement>().down_Lerp;
					break;
				}
				i++;
			}

		}
	}




	public void Open_Btn() {
		OpenPanel.SetActive (true);
		ClosePanel.SetActive (false);

		switch (this.name) {
		case "close_btn":
			CloseBtn.SetActive (true);
			break;
		case "OptionC_btn":
			CloseBtn.SetActive (false);
			break;
		case "SaveC_btn":
			CloseBtn.SetActive (false);
			break;
		}


	}


	public void Ani_Btn() {
		OpenPanel.SetActive (true);
		ClosePanel.SetActive (false);

		switch (this.name) {
		case "On_btn":
			obj_AniOn ();
			OpenPanel.SetActive (true);
			ClosePanel.SetActive (false);
			break;
		case "Off_btn":
			obj_AniOff ();
			OpenPanel.SetActive (true);
			ClosePanel.SetActive (false);
			break;
		}


	}

	void obj_AniOn(){
		Debug.Log ("all Stop!");
			foreach (Transform child in loadManager.transform) {
			
			switch (child.name.Substring(0,7)) {

			case "1990011":
				break;

				//01Ground
			case "1010011":
				child.GetComponent<objMovement> ().allStop = false;
				break;
			case "1010012":
				child.GetComponent<objMovement> ().allStop = false;
				break;
			case "1010021":
				child.GetComponent<objMovement> ().allStop = false;
				break;
			case "1010031":
				child.GetComponent<objMovement> ().allStop = false;
				break;

				//02ActionObj
			case "1020011":
				
				break;

				//03Stayenemy
			case "1030011":
				child.GetComponent<objMovement> ().allStop = false;
				break;
			case "1030021":
				child.GetComponent<objMovement> ().allStop = false;
				break;
			case "1030031":
				child.GetComponent<objMovement> ().allStop = false;
				break;
			case "1030041":
				child.GetComponent<objMovement> ().allStop = false;
				break;

				//04Moveenemy
			case "1040011":
				break;
			case "1040021":
				child.GetComponent<objMovement> ().allStop = false;
				break;
			case "1040031":
				child.GetComponent<Spider> ().allStop = false;
				break;
			case "1040032":
				child.GetComponent<Spider> ().allStop = false;
				break;
			case "1040033":
				child.GetComponent<Spider> ().allStop = false;
				break;
				//05Item

				//06Hurddle
			case "1060011":
				child.GetComponent<objMovement> ().allStop = false;
				break;
			case "1060021":
				child.GetComponent<objMovement> ().allStop = false;
				break;
			case "1060031":
				child.GetComponent<objMovement> ().allStop = false;
				break;

				//07Riding
			case "1070011":
				break;
			case "1070021":
				break;

				//08Cage
			case "1080011":
				break;
			}

			}
	}

	void obj_AniOff(){
		foreach (Transform child in loadManager.transform) {

				switch (child.name.Substring(0,7)) {

				case "1990011":
					break;

					//01Ground
				case "1010011":
					child.GetComponent<objMovement> ().allStop = false;
					break;
				case "1010012":
					child.GetComponent<objMovement> ().allStop = false;
					break;
				case "1010021":
					child.GetComponent<objMovement> ().allStop = false;
					break;
				case "1010031":
					child.GetComponent<objMovement> ().allStop = false;
					break;

					//02ActionObj
				case "1020011":

					break;

					//03Stayenemy
				case "1030011":
					child.GetComponent<objMovement> ().allStop = true;
					break;
				case "1030021":
					child.GetComponent<objMovement> ().allStop = true;
					break;
				case "1030031":
					child.GetComponent<objMovement> ().allStop = true;
					break;
				case "1030041":
					child.GetComponent<objMovement> ().allStop = true;
					break;

					//04Moveenemy
				case "1040011":
					break;
				case "1040021":
					child.GetComponent<objMovement> ().allStop = true;
					break;
				case "1040031":
				child.GetComponent<Spider> ().allStop = true;
					break;
				case "1040032":
				child.GetComponent<Spider> ().allStop = true;
					break;
				case "1040033":
				child.GetComponent<Spider> ().allStop = true;
					break;
					//05Item

					//06Hurddle
				case "1060011":
					child.GetComponent<objMovement> ().allStop = true;
					break;
				case "1060021":
					child.GetComponent<objMovement> ().allStop = true;
					break;
				case "1060031":
					child.GetComponent<objMovement> ().allStop = true;
					break;

					//07Riding
				case "1070011":
					break;
				case "1070021":
					break;

					//08Cage
				case "1080011":
					break;

			}

		}
	}



}
