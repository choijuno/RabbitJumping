using UnityEngine;
using System.Collections;
using SimpleJSON;

public class JsonParsing : MonoBehaviour {

    public static JsonParsing _instance;

    public TextAsset Json;
    string strJsonData;

	void Start ()
    {
        strJsonData = Json.text;
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            DestroyImmediate(this.gameObject);
        }
    }
    public int starJsonData(float stageIndex, float gameRecord)
    {
        var N = JSON.Parse(Json.text);
        int starCount = 0;
        for (int i = 0; i < N.Count; i++)
        {
            if(stageIndex == N[i][0].AsFloat)
            {
                if(gameRecord >= N[i][3].AsFloat)
                {
                    starCount = 3;
                }else if(gameRecord >= N[i][2].AsFloat)
                {
                    starCount = 2;
                }else if(gameRecord >= N[i][1].AsFloat)
                {
                    starCount = 1;
                }
            }
        }
        return starCount;
    }
}
