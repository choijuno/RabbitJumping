﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.Unity;

public partial class selectManager : MonoBehaviour
{
    public static int eggNumber = 0;

    public void MyBtnFunc() //인벤토리 버튼 눌렀을때.
    {
        MusicManager.instance.PlayOnShot();
        chaSetFalse();
        myRoomBtn.GetComponent<Image>().sprite = myRoomBtnClick;
        //myRoomBtn.GetComponent<Image>().SetNativeSize();
        storeBtn.GetComponent<Image>().sprite = noneStoreBtn;
        //storeBtn.GetComponent<Image>().SetNativeSize();
        storeAndRoom.SetActive(true);
        store.SetActive(false);
        myRoom.SetActive(true);
        for (int i = 0; i < nonImg.Length; i++)
        {
            if (ES2.Exists("character" + i.ToString()))
            {
                chaArray[i].sprite = okImg[i];
                chaBtnArray[i].interactable = true;
            }
            else
            {
                chaArray[i].sprite = nonImg[i];
                chaBtnArray[i].interactable = false;
            }
        }

        if (ES2.Exists("rabbit"))
        {
            chaSetFalse();
            preCharacter = Instantiate(cha_Array[ES2.Load<int>("rabbit")], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
        }
        else
        {
            chaSetFalse();
            preCharacter = Instantiate(cha_Array[0], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
        }
    }
    public void shopBtnFunc() //상점 버튼 눌렀을때.
    {
        MusicManager.instance.PlayOnShot();
        chaSetFalse();
        goldDraw.SetActive(true);
        bosukDraw.SetActive(true);
        adDraw.SetActive(true);
        storeBtn.GetComponent<Image>().sprite = storeBtnClick;
        myRoomBtn.GetComponent<Image>().sprite = noneMyRoomBtn;
        storeAndRoom.SetActive(true);
        store.SetActive(true);
        myRoom.SetActive(false);
    }

    public void setupBtnFunc() //설정 버튼 눌렀을때.
    {
        MusicManager.instance.PlayOnShot();
        chaSetFalse();
        setup.SetActive(true);

        /* 중국서비스
        if (!Social.localUser.authenticated)
        {
            GoogleBtnOff.gameObject.SetActive(true); //로그인 안되있으면.
            GoogleBtnOn.gameObject.SetActive(false);
        }
        else
        {
            GoogleBtnOn.gameObject.SetActive(true); //로그인 되있으면.
            GoogleBtnOff.gameObject.SetActive(false);
        }

        if (!FB.IsLoggedIn)
        {
            FaceBookOn.gameObject.SetActive(false);
            FaceBookOff.gameObject.SetActive(true);
        }
        else
        {
            FaceBookOn.gameObject.SetActive(true);
            FaceBookOff.gameObject.SetActive(false);
        }
        */
    }
    public void setupExitBtnFunc()
    {
        MusicManager.instance.PlayOnShot();
        chaSetFalse();
        setup.SetActive(false);
    }
    public void storeRoomExitFunc() //상점 닫기
    {
        MusicManager.instance.PlayOnShot();
        chaSetFalse();
        storeAndRoom.SetActive(false);
    }
    public void rangkingBtnFunc() //랭킹 버튼 눌렀을때.
    {
        chaSetFalse();
        GoogleManager.GetInstance.ShowLeaderboard();
    }
    public void tropyBtnFunc() //업적 버튼 눌렀을때.
    {
        chaSetFalse();
        GoogleManager.GetInstance.ShowAchievement();
    }
    public void bosukBtnFunc()
    {
        MusicManager.instance.PlayOnShot();
        goldBosuk.SetActive(true);
        goldBtn.image.sprite = GoldSprite;
        bosukBtn.image.sprite = nonBosukSprite;

        goldStore.SetActive(false);
        bosukStore.SetActive(true);
    }
    public void goldBtnFunc()
    {
        MusicManager.instance.PlayOnShot();
        goldBosuk.SetActive(true);
        goldBtn.image.sprite = nonGoldSprite;
        bosukBtn.image.sprite = BosukSprite;

        goldStore.SetActive(true);
        bosukStore.SetActive(false);
    }
    public void goldbosukExitBtnFunc()
    {
        MusicManager.instance.PlayOnShot();
        goldBosuk.SetActive(false);
    }
    public void bosukRealBtnFunc()
    {
        MusicManager.instance.PlayOnShot();
        bosukBtnFunc();
    }
    public void goldRealBtnFunc()
    {
        MusicManager.instance.PlayOnShot();
        goldBtnFunc();
    }

    public void bosukBuy(int won)
    {
        switch (won)
        {
            case 1000:
                InappManager.Instance.Buy20000Bosuk();
                break;
            case 2500:
                InappManager.Instance.Buy30000Bosuk();
                break;
            case 32500:
                InappManager.Instance.Buy100000Bosuk();
                break;
        }
    }
    public void goldBuy(int bosuk)
    {
        MusicManager.instance.PlayOnShot();
        float bosukCount = DataSave._instance.getBosuk_Game();
        switch (bosuk)
        {
            case 1000:
                if (bosukCount < 10)
                {
                    BosukPopUp.SetActive(true);
                }
                else
                {
                    DataSave._instance.setBosuk_GameMinus(10);
                    DataSave._instance.setMoney_Game(2000);
                    gameMoney.text = DataSave._instance.getMoney_Game().ToString();
                    bosukMoney.text = DataSave._instance.getBosuk_Game().ToString();
                }
                break;
            case 2500:
                if (bosukCount < 20)
                {
                    BosukPopUp.SetActive(true);
                }
                else
                {
                    DataSave._instance.setBosuk_GameMinus(20);
                    DataSave._instance.setMoney_Game(5000);
                    gameMoney.text = DataSave._instance.getMoney_Game().ToString();
                    bosukMoney.text = DataSave._instance.getBosuk_Game().ToString();
                }
                break;
            case 32500:
                if (bosukCount < 50)
                {
                    BosukPopUp.SetActive(true);
                }
                else
                {
                    DataSave._instance.setBosuk_GameMinus(50);
                    DataSave._instance.setMoney_Game(15000);
                    gameMoney.text = DataSave._instance.getMoney_Game().ToString();
                    bosukMoney.text = DataSave._instance.getBosuk_Game().ToString();
                }
                break;
        }
    }
    public void backgroundMusicGo(int musicOnOff) //사운드
    {
        if (musicOnOff == 0)
        {
            backgroundMusicOn.gameObject.SetActive(true);
            backgroundMusicOff.gameObject.SetActive(false);
            MusicManager.instance.MusicSelect(true);

        }
        else if (musicOnOff == 1)
        {
            backgroundMusicOn.gameObject.SetActive(false);
            backgroundMusicOff.gameObject.SetActive(true);
            MusicManager.instance.MusicSelect(false);
        }
    }
    public void tilt(bool tiltOnOff)
    {
        MusicManager.instance.PlayOnShot();
        if (tiltOnOff)
        {
            ES2.Save<bool>(false, "tilt");
            tiltOn.gameObject.SetActive(false);
            tiltOff.gameObject.SetActive(true);
        }
        else
        {
            ES2.Save<bool>(true, "tilt");
            tiltOn.gameObject.SetActive(true);
            tiltOff.gameObject.SetActive(false);
        }
    }
    public void HyoGwaSound(bool hyoGwaSound)
    {
        if (hyoGwaSound)
        {
            ES2.Save<bool>(false, "HyoGwaSound");
            hyogwaMusicOff.gameObject.SetActive(true);
            hyogwaMusicOn.gameObject.SetActive(false);
        }else
        {
            ES2.Save<bool>(true, "HyoGwaSound");
            hyogwaMusicOff.gameObject.SetActive(false);
            hyogwaMusicOn.gameObject.SetActive(true);
        }
    }
    public void greenBoxBtnFunc() //그린박스
    {
        MusicManager.instance.PlayOnShot();
        float gameMoney_ = DataSave._instance.getMoney_Game();
        if (gameMoney_ < 1000)
        {
            //돈이 부족하면
            goldPopUp.SetActive(true);
        }
        else
        {
            eggNumber = 0;
            DataSave._instance.setMoney_GameMinus(1000);
            gameMoney.text = DataSave._instance.getMoney_Game().ToString();
            SceneManager.LoadScene(5);
        }
    }

    public void redBoxBtnFunc() //레드박스
    {
        MusicManager.instance.PlayOnShot();
        /*
        float gameMoney_ = DataSave._instance.getBosuk_Game();

        if (gameMoney_ < 10)
        {
            //보석이 부족할때..
            BosukPopUp.SetActive(true);
        }
        else
        {
            eggNumber = 1;
            DataSave._instance.setBosuk_GameMinus(10);
            bosukMoney.text = DataSave._instance.getBosuk_Game().ToString();
            SceneManager.LoadScene(5);
        }*/
        float gameMoney_ = DataSave._instance.getMoney_Game();
        if (gameMoney_ < 2000)
        {
            //돈이 부족하면
            goldPopUp.SetActive(true);
        }
        else
        {
            eggNumber = 1;
            DataSave._instance.setMoney_GameMinus(2000);
            gameMoney.text = DataSave._instance.getMoney_Game().ToString();
            SceneManager.LoadScene(5);
        }
    }
    public void blueBoxBtnFunc() //광고 박스
    {
        /*
        eggNumber = 2;
        SceneManager.LoadScene(5);
        */
        float gameMoney_ = DataSave._instance.getMoney_Game();
        if (gameMoney_ < 3000)
        {
            //돈이 부족하면
            goldPopUp.SetActive(true);
        }
        else
        {
            eggNumber = 2;
            DataSave._instance.setMoney_GameMinus(3000);
            gameMoney.text = DataSave._instance.getMoney_Game().ToString();
            SceneManager.LoadScene(5);
        }
    }
    
    public void chaFunc(string cha_index)
    {
        int cha_test = int.Parse(cha_index);
        MusicManager.instance.PlayOnShot();
        switch (cha_test)
        {
            case 0:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[0], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;

                ES2.Save<int>(0, "rabbit");
                break;
            case 1:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[1], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(1, "rabbit");
                break;
            case 2:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[2], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(2, "rabbit");
                break;
            case 3:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[3], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(3, "rabbit");
                break;
            case 4:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[4], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(4, "rabbit");
                break;
            case 5:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[5], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(5, "rabbit");
                break;
            case 6:
                chaSetFalse();
                preCharacter =Instantiate(cha_Array[6], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(6, "rabbit");
                break;
            case 7:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[7], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(7, "rabbit");
                break;
            case 8:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[8], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(8, "rabbit");
                break;
            case 9:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[9], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(9, "rabbit");
                break;
            case 10:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[10], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(10, "rabbit");
                break;
            case 11:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[11], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(11, "rabbit");
                break;
            case 12:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[12], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(12, "rabbit");
                break;
            case 13:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[13], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(13, "rabbit");
                break;
            case 14:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[14], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(14, "rabbit");
                break;
            case 15:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[15], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(15, "rabbit");
                break;
            case 16:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[16], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(16, "rabbit");
                break;
            case 17:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[17], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(17, "rabbit");
                break;
            case 18:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[18], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(18, "rabbit");
                break;
            case 19:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[19], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(19, "rabbit");
                break;
            case 20:
                chaSetFalse();
                preCharacter = Instantiate(cha_Array[20], new Vector3(-24.19f, -2.93f, -163.23f), Quaternion.identity) as GameObject;
                ES2.Save<int>(20, "rabbit");
                break;
        }
    }
    public void chaSetFalse()
    {
        goldDraw.SetActive(false);
        bosukDraw.SetActive(false);
        adDraw.SetActive(false);
        Destroy(preCharacter);
    }
    public void GoogleBtnFunc(int googleOnOff)
    {
        MusicManager.instance.PlayOnShot();
        //0 == 로그인 , 1 == 비로그인.
        if (googleOnOff == 0)
        {
            GoogleManager.GetInstance.LogoutGPGS();
            GoogleBtnOff.gameObject.SetActive(true);
            GoogleBtnOn.gameObject.SetActive(false);
        }
        else
        {
            GoogleManager.GetInstance.LoginGPGS();
            GoogleBtnOn.gameObject.SetActive(true);
            GoogleBtnOff.gameObject.SetActive(false);
        }
    }
    public void GoldYes()
    {
        goldBtnFunc();
        storeRoomExitFunc();
        goldPopUp.SetActive(false);
    }
    public void GoldNo()
    {
        goldPopUp.SetActive(false);
    }
    public void BosukYes()
    {
        bosukBtnFunc();
        storeRoomExitFunc();
        BosukPopUp.SetActive(false);
    }
    public void BosukNo()
    {
        BosukPopUp.SetActive(false);
    }
    public void GameExitNo()
    {
        GameOutPopUp.SetActive(false);
    }
    public void GameExitYes()
    {
        Application.Quit();
    }
    public void Bosukview()
    {
        bosukMoney.text = DataSave._instance.getBosuk_Game().ToString();
    }
    
}
