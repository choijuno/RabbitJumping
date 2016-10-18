using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        for (int i = 0; i < 10; i++)
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
                    return;
                }
                else
                {
                    DataSave._instance.setBosuk_GameMinus(10);
                    DataSave._instance.setMoney_Game(2000);
                    gameMoney.text = DataSave._instance.getMoney_Game().ToString();
                }
                break;
            case 2500:
                if (bosukCount < 20)
                {
                    return;
                }
                else
                {
                    DataSave._instance.setBosuk_GameMinus(20);
                    DataSave._instance.setMoney_Game(5000);
                    gameMoney.text = DataSave._instance.getMoney_Game().ToString();
                }
                break;
            case 32500:
                if (bosukCount < 50)
                {
                    return;
                }
                else
                {
                    DataSave._instance.setBosuk_GameMinus(50);
                    DataSave._instance.setMoney_Game(15000);
                    gameMoney.text = DataSave._instance.getMoney_Game().ToString();
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
        DataSave._instance.setDraw(1);
        int DrawCount = DataSave._instance.getDraw();

        if(DrawCount == 10)
            Social.ReportProgress(GPGS.achievement_draw1, 100.0f, (bool success) => { });
        else if(DrawCount == 30)
            Social.ReportProgress(GPGS.achievement_draw2, 100.0f, (bool success) => { });
        else if(DrawCount == 50)
            Social.ReportProgress(GPGS.achievement_draw3, 100.0f, (bool success) => { });

        MusicManager.instance.PlayOnShot();
        float gameMoney_ = DataSave._instance.getMoney_Game();
        if (gameMoney_ < 1000)
        {
            //돈이 부족하면
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
        DataSave._instance.setDraw(1);

        int DrawCount = DataSave._instance.getDraw();
        if (DrawCount >= 10)
            Social.ReportProgress(GPGS.achievement_draw1, 100.0f, (bool success) => { });
        else if (DrawCount >= 30)
            Social.ReportProgress(GPGS.achievement_draw2, 100.0f, (bool success) => { });
        else if (DrawCount >= 50)
            Social.ReportProgress(GPGS.achievement_draw3, 100.0f, (bool success) => { });
        MusicManager.instance.PlayOnShot();

        float gameMoney_ = DataSave._instance.getBosuk_Game();

        if (gameMoney_ < 10)
        {
            //보석이 부족할때..
        }
        else
        {
            eggNumber = 1;
            DataSave._instance.setBosuk_GameMinus(10);
            gameMoney.text = DataSave._instance.getBosuk_Game().ToString();
            SceneManager.LoadScene(5);
        }
    }
    public void blueBoxBtnFunc() //광고 박스
    {
        DataSave._instance.setDraw(1);
        int DrawCount = DataSave._instance.getDraw();

        if (DrawCount >= 10)
            Social.ReportProgress(GPGS.achievement_draw1, 100.0f, (bool success) => { });
        else if (DrawCount >= 30)
            Social.ReportProgress(GPGS.achievement_draw2, 100.0f, (bool success) => { });
        else if (DrawCount >= 50)
            Social.ReportProgress(GPGS.achievement_draw3, 100.0f, (bool success) => { });
        eggNumber = 2;
        SceneManager.LoadScene(5);
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
}
