using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DataSave : MonoBehaviour {

    public static DataSave _instance;
    public void setSkin(int skinCount) //스킨카운트
    {
        if (ES2.Exists("Skin"))
            ES2.Save<int>(ES2.Load<int>("Skin") + skinCount, "Skin");
        else
            ES2.Save<int>(skinCount, "Skin");
    }
    public int getSkin()
    {
        return ES2.Load<int>("Skin");
    }
    public void setDraw(int DrawCount) //뽑기 카운트
    {
        if (ES2.Exists("Draw"))
            ES2.Save<int>(ES2.Load<int>("Draw") + DrawCount, "Draw");
        else
            ES2.Save<int>(DrawCount, "Draw");
    }
    public int getDraw()
    {
        return ES2.Load<int>("Draw");
    }
    public void setTimer(float timer) // 시간 카운트
    {
        if (ES2.Exists("timer"))
            ES2.Save<float>(ES2.Load<float>("timer") + timer, "timer");
        else
            ES2.Save<float>(timer, "timer");
    }
    public float getTimer()
    {
        return ES2.Load<int>("timer");
    }
    public void setAnimal(int animalCount) //동물 카운트
    {
        if (ES2.Exists("Animal"))
            ES2.Save<int>(ES2.Load<int>("Animal") + animalCount, "Animal");
        else
            ES2.Save<int>(animalCount, "Animal");
    }
    public int getAnimal()
    {
        return ES2.Load<int>("Animal");
    }

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
        }
        else
        {
            ES2.Save<float>(bosuk_Money, "bosuk_Game");
        }
            
    }
    public void setBosuk_GameMinus(float bosuk_Money) //보석저장.
    {
        if (ES2.Exists("bosuk_Game"))
        {
            ES2.Save<float>(ES2.Load<float>("bosuk_Game") - bosuk_Money, "bosuk_Game");
        }
        else
        {
            ES2.Save<float>(bosuk_Money, "bosuk_Game");
        }
    }
    public float getBosuk_Game() //보석 불러오기.
    {
        return ES2.Load<float>("bosuk_Game");
    }

    public float getMoney_Game() //돈 불러오기.
    {
        return ES2.Load<float>("Money_Game");
    }

    public void setStar_Count(int star_Count) //별 저장.
    {
        if (ES2.Exists("Star_Count"))
            ES2.Save<int>(ES2.Load<int>("Star_Count") + star_Count, "Star_Count");
        else
            ES2.Save<int>(star_Count, "Star_Count");
    }

	public float getStar_Count()
    {
        if (ES2.Exists("Star_Count"))
            return ES2.Load<int>("Star_Count");
        else
            return -1;
    }
    
    void Start()
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
    public void saveData(int stageIndex, float starCount, float stageRecord) // 스테이지, 별, 기록을 저장함.
    {

        //stageData sd = new stageData(stageIndex, starCount, stageRecord);
        //ES2.Save(sd, "valueKeytest");
        
        if(ES2.Exists("ValueKey" + stageIndex.ToString()))
        {
            string[] ExistsString = new string[3];
            ExistsString = ES2.LoadArray<string>("ValueKey" + stageIndex.ToString());

            if(starCount > float.Parse(ExistsString[1]))
            {
                string[] test = new string[3];
                test[0] = stageIndex.ToString();
                test[1] = starCount.ToString();
                test[2] = ExistsString[2];

                ES2.Save(test, "ValueKey" + stageIndex.ToString());
            }
            else if(starCount < float.Parse(ExistsString[1]))
            {
                return;
            }

            if(stageRecord > float.Parse(ExistsString[2]))
            {
                string[] test = new string[3];
                test[0] = stageIndex.ToString();
                test[1] = ExistsString[1];
                test[2] = stageRecord.ToString();

                ES2.Save(test, "ValueKey" + stageIndex.ToString());
            }
            else
            {
                return;
            }
        }
        else
        {
            string[] test = new string[3];
            test[0] = stageIndex.ToString();
            test[1] = starCount.ToString();
            test[2] = stageRecord.ToString();

            ES2.Save(test, "ValueKey" + stageIndex.ToString());

            if (ES2.Exists("stageIndexCount"))
            {
                if (ES2.Load<int>("stageIndexCount") < stageIndex)
                {
                    ES2.Save<int>(stageIndex, "stageIndexCount");
                }
            }
            else
            {
                ES2.Save<int>(stageIndex, "stageIndexCount");
            }
        }
       
    }
}
