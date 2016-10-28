using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LanguageClass : MonoBehaviour
{
    selectManager _SelectManager;

    public Text musicBackground;
    public Text musichyogwa;
    public Text tilt;
    public Text GoogleTxt;
    public Text FacebookTxt;
    public Text languageTxt;
    public Text AdsView;
    public Text Gold20000;
    public Text Gold30000;
    public Text Gold100000;
    public Text Crystal50;
    public Text Crystal200;
    public Text crystal1000;
    public Text bike_helmet;
    public Text football_player;
    public Text mustach_rabbit;
    public Text dead_rabbit;
    public Text black_ninja;
    public Text white_ninja;
    public Text carpenter;
    public Text police_officer;
    public Text santa_rabbit;
    public Text snow_rabbit;
    public Text baseball_player;

    public Text bosukBuy;
    public Text GoldBuy;
    public Text GameExit;
    public Text BosukBuyOk;
    public Text BosukBuyNo;
    public Text GoldBuyOk;
    public Text GoldBuyNo;
    public Text GameExitOk;
    public Text GameExitNo;

    public GameObject[] EnglishImg;
    public GameObject[] KoreanImg;
    void Start()
    {
        _SelectManager = GameObject.Find("selectManager").GetComponent<selectManager>();
        if (!ES2.Exists("Language"))
        {
            ES2.Save<bool>(true, "Language");
            KoreanTxt();
          
        }
        if (ES2.Load<bool>("Language"))
        {
            KoreanTxt();
        }
        else
        {
            EnglishTxt();
            
        }
    }
    void KoreanTxt()
    {
        for (int i = 0; i < KoreanImg.Length; i++)
        {
            KoreanImg[i].SetActive(true);
            EnglishImg[i].SetActive(false);
        }
        musicBackground.text = "배경음";
        musichyogwa.text = "효과음";
        tilt.text = "틸트";
        GoogleTxt.text = "구글";
        FacebookTxt.text = "페이스북";
        languageTxt.text = "언어";
        AdsView.text = "광고보기";
        Gold20000.text = "2,000 골드";
        Gold30000.text = "5,000 골드";
        Gold100000.text = "15,000 골드";
        Crystal50.text = "20 보석";
        Crystal200.text = "80 보석";
        crystal1000.text = "150 보석";
        bike_helmet.text = "오토바이 핼멧";
        football_player.text = "미식축구선수";
        mustach_rabbit.text = "수염";
        dead_rabbit.text = "죽음의 사도";
        black_ninja.text = "검은닌자";
        white_ninja.text = "하얀닌자";
        carpenter.text = "목공";
        police_officer.text = "경찰관";
        santa_rabbit.text = "산타";
        snow_rabbit.text = "스노우 레빗";
        baseball_player.text = "야구선수";

        bosukBuy.text = "보석이 부족합니다. \n충전 하시겠습니까?";
        GoldBuy.text = "골드가 부족합니다. \n충전 하시겠습니까?";

        GameExit.text = "정말 종료 하시겠습니까?";
        BosukBuyOk.text = "예";
        BosukBuyNo.text = "아니오";
        GoldBuyOk.text = "예";
        GoldBuyNo.text = "아니오";
        GameExitOk.text = "예";
        GameExitNo.text = "아니오";
}
    void EnglishTxt()
    {
        for (int i = 0; i < KoreanImg.Length; i++)
        {
            KoreanImg[i].SetActive(false);
            EnglishImg[i].SetActive(true);
        }
        musicBackground.text = "BGM";
        musichyogwa.text = "Effect";
        tilt.text = "TILT";
        GoogleTxt.text = "Google";
        FacebookTxt.text = "Facebook";
        languageTxt.text = "Language";
        AdsView.text = "Ad View";
        Gold20000.text = "2,000 Gold";
        Gold30000.text = "5,000 Gold";
        Gold100000.text = "15,000 Gold";
        Crystal50.text = "20 Crystal";
        Crystal200.text = "80 Crystal";
        crystal1000.text = "150 Crystal";
        bike_helmet.text = "Bike Rabbit";
        football_player.text = "Football Rabbit";
        mustach_rabbit.text = "mustache Rabbit";
        dead_rabbit.text = "Dead Rabbit";
        black_ninja.text = "Black Ninja";
        white_ninja.text = "White Ninja";
        carpenter.text = "Carpenter Rabbit";
        police_officer.text = "Police Rabbit";
        santa_rabbit.text = "Santa Rabbit";
        snow_rabbit.text = "Snow Rabbit";
        baseball_player.text = "Baseball Rabbit";


        bosukBuy.text = "Crystal is low. \nWould you like to fill ? ";
        GoldBuy.text = "Gold is low. \nWould you like to fill ? ";

        GameExit.text = "Are you sure you want to quit?";
        BosukBuyOk.text = "Yes";
        BosukBuyNo.text = "No";
        GoldBuyOk.text = "Yes";
        GoldBuyNo.text = "No";
        GameExitOk.text = "Yes";
        GameExitNo.text = "No";
    }
    public void Korean()
    {
        ES2.Save<bool>(true, "Language");
        KoreanTxt();
    }
    public void English()
    {
        ES2.Save<bool>(false, "Language");
        EnglishTxt();
    }
}
