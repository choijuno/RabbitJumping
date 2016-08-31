using UnityEngine;
using System.Collections;

public class objMovement : MonoBehaviour {
	public bool allStop=true;

	objMove objmove = objMove.wait;
	MovePosition movePos = MovePosition.Left;
	//Stat
	public bool havehp;
	public int standHp;
	int standHp_in;

	public int angryHp;
	int angryHp_in;


	//movement
	public bool move;
	public Transform turn_image;
	public float L_Speed;
	float L_Speed_in;
	public float R_Speed;
	float R_Speed_in;

	public float L_Range;
	float L_Range_in;
	public float R_Range;
	float R_Range_in;
	float baseX_in;


	//Attack_Jump;
	public bool jump;
	public Transform jump_image;
	public float waitTime_jump;
	float waitTime_jump_in;

	public float maxHeight;
	float maxHeight_in;
	float baseHeight_in;

	public float U_Speed;
	float U_Speed_in;

	public float U_Lerp;
	float U_Lerp_in;

	public float down_Lerp;
	float down_Lerp_in;



	//_etc


	void Start(){
		if (Application.loadedLevelName == "Edit") {
			allStop = true;
		}
	}
	// Use this for initialization


	void Awake () {
		//basePos = transform;
		//transform.position = basePos.position;
		switch (this.gameObject.name.Substring (0, 7)) {

		case "1990011":
			break;

			//01Ground
		case "1010011":
			StartCoroutine ("enemyM_reset");
			break;
		case "1010012":
			StartCoroutine ("enemyM_reset");
			break;
		case "1010021":
			StartCoroutine ("enemyM_reset");
			break;
		case "1010031":
			StartCoroutine ("enemyM_reset");
			break;

			//02ActionObj
		case "1020011":

			break;

			//03Stayenemy
		case "1030011":
			StartCoroutine ("enemyM_reset");
			break;
		case "1030012":
			StartCoroutine ("enemyM_reset");
			break;
		case "1030021":
			break;
		case "1030031":
			break;
		case "1030041":
			break;

			//04Moveenemy
		case "1040011":
			break;
		case "1040021":
			StartCoroutine ("enemyM_reset");
			break;
		case "1040031":
			break;
		case "1040032":
			break;
		case "1040033":
			break;
			//05Item

			//06Hurddle
		case "1060011":
			break;
		case "1060021":
			break;
		case "1060031":
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



	IEnumerator enemyM_reset(){


		while (true) {
			yield return new WaitForSeconds (0.006f);
			jump_reset ();
			move_reset ();
			if (!allStop) {

				StartCoroutine ("enemyM");
				StopCoroutine ("enemyM_reset");
			}

		}
	}



	IEnumerator enemyM(){
		


		while (true) {
			yield return new WaitForSeconds (0.006f);

			if (!allStop) {
				Attack_jump ();
				MoveLR ();
			} else {
				//jump
				if (jump) {
					jump_image.transform.rotation = Quaternion.Euler (0, 0, 0);
					transform.position = new Vector3 (transform.position.x, baseHeight_in, transform.position.z);
					objmove = objMove.wait;
				}
				//move
				if (move) {
					if (this.name.Substring (0, 7) == "1040021") {
						transform.localScale = new Vector3 (2, 1, 1);
					}
					transform.position = new Vector3 (baseX_in, transform.position.y, transform.position.z);
					movePos = MovePosition.Left;
				}
				//etc
				StartCoroutine ("enemyM_reset");
				StopCoroutine ("enemyM");
			}

		}
	}





	//jump
	void Attack_jump(){
		if (jump) {
			switch (objmove) {

			case objMove.wait:
				mwait ();
				break;

			case objMove.up:
				mup ();
				break;

			case objMove.down:
				mdown ();
				break;

			}
		}
	}

	void mwait(){
		if (gameObject.name.Substring (0, 7) == "1030011")
			jump_image.transform.rotation = Quaternion.Euler (0, 0, 0);

		if (waitTime_jump_in >= 0) {
			waitTime_jump_in -= Time.deltaTime;
			//Debug.Log (waitTime_in);

			if (waitTime_jump_in < 0) {
				waitTime_jump_in = waitTime_jump;
				objmove = objMove.up;
				if (gameObject.name.Substring (0, 7) == "1030011")
				jump_image.transform.rotation = Quaternion.Euler (0, 0, -70);

			}
		}
	}

	void mup(){
		//fish_image.transform.Rotate (0, 0, -10);

		U_Lerp_in = Mathf.Lerp (U_Lerp_in, 0, 0.9f);

		if (U_Lerp_in <= 0) {
			U_Lerp_in = 0;
		}

		transform.position = new Vector3 (transform.position.x, transform.position.y + (U_Speed_in * Time.deltaTime) + U_Lerp_in, transform.position.z);

		if (transform.position.y >= maxHeight_in) {
			down_Lerp_in = 0;
			objmove = objMove.down;
			if (gameObject.name.Substring (0, 7) == "1030011")
			jump_image.transform.rotation = Quaternion.Euler (0, 0, 90);
			transform.position = new Vector3 (transform.position.x, maxHeight_in, transform.position.z);
		}
	}

	void mdown(){
		//fish_image.transform.rotation = new Quaternion (0,0,90,0);

		down_Lerp_in = Mathf.Lerp (down_Lerp_in, down_Lerp * 2f, 0.001f);

		if (down_Lerp_in >= down_Lerp * 0.1f) {
			down_Lerp_in = down_Lerp * 0.1f;
		}

		transform.position = new Vector3 (transform.position.x, transform.position.y - down_Lerp_in * 0.1f, transform.position.z);

		if (transform.position.y <= baseHeight_in) {
			transform.position = new Vector3 (transform.position.x, baseHeight_in, transform.position.z);

			U_Lerp_in = U_Lerp * 0.1f;
			objmove = objMove.wait;
		}
	}

	void jump_reset() {
		waitTime_jump_in = waitTime_jump;
		maxHeight_in = transform.position.y + maxHeight;
		baseHeight_in = transform.position.y;
		U_Speed_in = U_Speed;
		U_Lerp_in = U_Lerp * 0.1f;
		down_Lerp_in = down_Lerp * 0.1f;
	}






	//move
	void MoveLR(){
		if (move) {
			switch (movePos) {
			case MovePosition.Left:
				if (transform.position.x >= L_Range_in) {
					transform.position = new Vector3 (transform.position.x - L_Speed_in, transform.position.y, transform.position.z);
				} else {
					transform.position = new Vector3 (L_Range_in, transform.position.y, transform.position.z);
					if (this.name.Substring(0,7)=="1040021") {
						transform.localScale = new Vector3 (-2, 1, 1);
					}
					movePos = MovePosition.Right;
				}
				break;
			case MovePosition.Right:
				if (transform.position.x <= R_Range_in) {
					transform.position = new Vector3 (transform.position.x + R_Speed_in, transform.position.y, transform.position.z);
				} else {
					transform.position = new Vector3 (R_Range_in, transform.position.y, transform.position.z);
					if (this.name.Substring(0,7)=="1040021") {
						transform.localScale = new Vector3 (2, 1, 1);
					}
					movePos = MovePosition.Left;
				}
				break;
			}
		}
	}

	void move_reset() {
		baseX_in = transform.position.x;

		L_Speed_in = L_Speed * 0.001f;
		R_Speed_in = R_Speed * 0.001f;

		L_Range_in = transform.position.x - L_Range;
		R_Range_in = transform.position.x + R_Range;

	}


}
