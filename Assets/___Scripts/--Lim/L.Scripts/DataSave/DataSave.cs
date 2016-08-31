using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DataSave : MonoBehaviour {

    public static DataSave _instance;

    public Text Bosuk_Game;

    public void setMoney_Game(float game_Money) //돈저장
    {
        if(ES2.Exists("Money_Game"))
            ES2.Save<float>(ES2.Load<float>("Money_Game") + game_Money, "Money_Game");
        else
            ES2.Save<float>(game_Money, "Money_Game");
    }

    public void setMoney_GameMinus(float game_Money) //돈저장
    {
        if (ES2.Exists("Money_Game"))
            ES2.Save<float>(ES2.Load<float>("Money_Game") - game_Money, "Money_Game");
        else
            ES2.Save<float>(game_Money, "Money_Game");
    }
    public void setBosuk_Game(float bosuk_Money) //보석저장.
    {
        if (ES2.Exists("bosuk_Game"))
        {
            ES2.Save<float>(ES2.Load<float>("bosuk_Game") + bosuk_Money, "bosuk_Game");
            Bosuk_Game.text = ES2.Load<float>("bosuk_Game").ToString();
        }
        else
        {
            ES2.Save<float>(bosuk_Money, "bosuk_Game");
            Bosuk_Game.text = ES2.Load<float>("bosuk_Game").ToString();
        }
            
    }
    public void setBosuk_GameMinus(float bosuk_Money) //보석저장.
    {
        if (ES2.Exists("bosuk_Game"))
        {
            ES2.Save<float>(ES2.Load<float>("bosuk_Game") - bosuk_Money, "bosuk_Game");
            Bosuk_Game.text = ES2.Load<float>("bosuk_Game").ToString();
        }
        else
        {
            ES2.Save<float>(bosuk_Money, "bosuk_Game");
            Bosuk_Game.text = ES2.Load<float>("bosuk_Game").ToString();
        }
    }
    public float getBosuk_Game()
    {
        return ES2.Load<float>("bosuk_Game");
    }

    public float getMoney_Game()
    {
        return ES2.Load<float>("Money_Game");
    }

    public void setStar_Count(int star_Count) //별 저장
    {
        if (ES2.Exists("Star_Count"))
            ES2.Save<int>(ES2.Load<int>("Star_Count") + star_Count, "Star_Count");
        else
            ES2.Save<int>(star_Count, "Star_Count");

        GoogleManager.GetInstance.ReportScoreLeaderBoard(ES2.Load<int>("Star_Count"));
    }

    public float getStar_Count()
    {
        if (ES2.Exists("Star_Count"))
            return ES2.Load<int>("Star_Count");
        else
            return -1;
    }

    struct stageData
    {
        private float stageIndex;
        private float starCount;
        private float stageRecord;
        
        public stageData(float stageIndex, float starCount, float stageRecord)
        {
            this.stageIndex = stageIndex;
            this.starCount = starCount;
            this.stageRecord = stageRecord;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            DestroyImmediate(this.gameObject);
        }
    }
    public void saveData(float stageIndex, float starCount, float stageRecord)
    {
        Debug.Log("-------stageindex-----" + stageIndex);

        //stageData sd = new stageData(stageIndex, starCount, stageRecord);
        //ES2.Save(sd, "valueKeytest");

        string[] test = new string[3];
        test[0] = stageIndex.ToString();
        test[1] = starCount.ToString();
        test[2] = stageRecord.ToString();

        ES2.Save(test, "ValueKey" + stageIndex.ToString());


        if (ES2.Exists("stageCount"))
        {
            if (ES2.Load<float>("stageCount") < stageIndex)
            {
                ES2.Save(stageIndex, "stageCount");
            }
        }
        else
        {
            ES2.Save(stageIndex, "stageCount");
        }
    }
}
