﻿using UnityEngine;
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
  
    void Start()
    {
        _SelectManager = GameObject.Find("selectManager").GetComponent<selectManager>();
    }

    public void Korean()
    {
        Debug.Log("=========한국어=========");
        ES2.Save<bool>(true, "Language");
        musicBackground.text = "배경음";
        musichyogwa.text = "효과음";
        tilt.text = "틸트";
        GoogleTxt.text = "구글";
        FacebookTxt.text = "페이스북";
        languageTxt.text = "언어";
        AdsView.text = "광고보기";
        Gold20000.text = "20000골드";
        Gold30000.text = "30000골드";
        Gold100000.text = "100000골드";
        Crystal50.text = "50보석";
        Crystal200.text = "200보석";
        crystal1000.text = "1000보석";
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
    }
    public void English()
    {
        Debug.Log("=========영어=========");
        ES2.Save<bool>(false, "Language");
        musicBackground.text = "BGM";
        musichyogwa.text = "Effect";
        tilt.text = "Tilt";
        GoogleTxt.text = "Google";
        FacebookTxt.text = "Facebook";
        languageTxt.text = "Language";
        AdsView.text = "Ad View";
        Gold20000.text = "20000Gold";
        Gold30000.text = "30000Gold";
        Gold100000.text = "100000Gold";
        Crystal50.text = "50Crystal";
        Crystal200.text = "200Crystal";
        crystal1000.text = "1000Crystal";
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
    }
}
