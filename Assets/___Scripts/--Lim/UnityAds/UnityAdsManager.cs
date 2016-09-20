﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class UnityAdsManager : MonoBehaviour {
    
    ShowOptions _ShowOpt = new ShowOptions();

    selectManager SelectManager;
    void Start ()
    {
        SelectManager = GameObject.Find("selectManager").GetComponent<selectManager>();
        Advertisement.Initialize("124147", true); // true인이유는 테스트광고를 시청하겠다.
        _ShowOpt.resultCallback = OnAdsShowResultCallBack;

    }
    void OnAdsShowResultCallBack(ShowResult result) //광고 보기후 호출되는 콜백함수. finished 다봤다는의미.
    {
        if (result == ShowResult.Finished)
            SelectManager.blueBoxBtnFunc();
    }
    /*
    void UpdateButton() //광고 준비가 되있다면 활성화.
    {
        unityAds.interactable = Advertisement.IsReady();
        unityAds.GetComponentInChildren<Text>().text = "보상형광고 테스트입니다! \r\nTest = " + _test.ToString();
    }
    */
    public void unityAdsFunc()
    {
        Advertisement.Show(null, _ShowOpt);
    }
}
