using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class save : MonoBehaviour {

    public Text tt;

	// Use this for initialization
	void Start () {
        
       ES2.Save(123, Application.dataPath+"/Resources/stage/myFile.bytes");

	}
	
	// Update is called once per frame
	void Update () {

        ES2Settings settings = new ES2Settings();
        settings.saveLocation = ES2Settings.SaveLocation.Resources;

        int myInt = ES2.Load<int>("/myFile.bytes?tag=myInt", settings);

        tt.text = myInt + "";
	
	}
}
