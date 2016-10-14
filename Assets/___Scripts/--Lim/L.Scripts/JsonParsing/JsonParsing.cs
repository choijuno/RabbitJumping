using UnityEngine;
using System.Collections;
using SimpleJSON;

public class JsonParsing : MonoBehaviour {

    public static JsonParsing _instance;

    public TextAsset Json;

	void Awake ()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public int starJsonData(float stageIndex)
    {
        var N = JSON.Parse(Json.text);
        int timeCount = 0;

		Debug.Log ("N.count" + N.Count);

        for (int i = 0; i < N.Count; i++)
		{
            if(stageIndex == N[i][0].AsFloat)
            {
				
                timeCount = N[i][1].AsInt;
            }
        }
		Debug.Log ("timeCount" + timeCount);
        return timeCount;
    }
}
