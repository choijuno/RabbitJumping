using UnityEngine;
using System.Collections;

public class StoryAni_Language : MonoBehaviour {

	public GameObject Kr_Touch;
	public GameObject Eng_Touch;

	void Start () {
		if (ES2.Exists ("Language")) {

			if (ES2.Load<bool> ("Language")) {
				Kr_Touch.SetActive (true);
			} else {
				Eng_Touch.SetActive (true);
			}

		} else {
			ES2.Save<bool> (true, "Language");
			Kr_Touch.SetActive (true);
		}
	}

}
