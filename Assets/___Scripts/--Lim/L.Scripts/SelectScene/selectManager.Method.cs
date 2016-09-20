using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public partial class selectManager : MonoBehaviour
{
    public void MyBtnFunc() //인벤토리 버튼 눌렀을때.
    {
        chaSetFalse();
        myRoomBtn.GetComponent<Image>().sprite = myRoomBtnClick;
        myRoomBtn.GetComponent<Image>().SetNativeSize();
        storeBtn.GetComponent<Image>().sprite = noneStoreBtn;
        storeBtn.GetComponent<Image>().SetNativeSize();
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
            cha_Array[ES2.Load<int>("rabbit")].SetActive(true);
        }
        else
        {
            chaSetFalse();
            cha_Array[0].SetActive(true);
        }
    }
    public void shopBtnFunc() //상점 버튼 눌렀을때.
    {
        chaSetFalse();
        goldDraw.SetActive(true);
        bosukDraw.SetActive(true);
        adDraw.SetActive(true);
        storeBtn.GetComponent<Image>().sprite = storeBtnClick;
        storeBtn.GetComponent<Image>().SetNativeSize();
        myRoomBtn.GetComponent<Image>().sprite = noneMyRoomBtn;
        myRoomBtn.GetComponent<Image>().SetNativeSize();
        storeAndRoom.SetActive(true);
        store.SetActive(true);
        myRoom.SetActive(false);
    }

    public void setupBtnFunc() //설정 버튼 눌렀을때.
    {
        chaSetFalse();
        setup.SetActive(true);
    }
    public void setupExitBtnFunc()
    {
        chaSetFalse();
        setup.SetActive(false);
    }
    public void storeRoomExitFunc() //상점 닫기
    {
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
        goldBosuk.SetActive(true);
        goldBtn.image.sprite = GoldSprite;
        bosukBtn.image.sprite = nonBosukSprite;

        goldBtn.image.SetNativeSize();
        bosukBtn.image.SetNativeSize();

        goldStore.SetActive(false);
        bosukStore.SetActive(true);
    }
    public void goldBtnFunc()
    {
        goldBosuk.SetActive(true);
        goldBtn.image.sprite = nonGoldSprite;
        bosukBtn.image.sprite = BosukSprite;

        goldBtn.image.SetNativeSize();
        bosukBtn.image.SetNativeSize();

        goldStore.SetActive(true);
        bosukStore.SetActive(false);
    }
    public void goldbosukExitBtnFunc()
    {
        goldBosuk.SetActive(false);
    }
    public void bosukRealBtnFunc()
    {
        bosukBtnFunc();
    }
    public void goldRealBtnFunc()
    {
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
        float bosukCount = DataSave._instance.getBosuk_Game();
        switch (bosuk)
        {
            case 1000:
                if (bosukCount < 1000)
                {
                    Debug.Log("====보석이 부족합니다=====");
                    return;
                }
                else
                {
                    DataSave._instance.setBosuk_GameMinus(1000);
                    DataSave._instance.setMoney_Game(20000);
                    gameMoney.text = DataSave._instance.getMoney_Game().ToString();
                }
                break;
            case 2500:
                if (bosukCount < 2000)
                {
                    Debug.Log("====보석이 부족합니다=====");
                    return;
                }
                else
                {
                    DataSave._instance.setBosuk_GameMinus(2500);
                    DataSave._instance.setMoney_Game(30000);
                    gameMoney.text = DataSave._instance.getMoney_Game().ToString();
                }
                break;
            case 32500:
                if (bosukCount < 32500)
                {
                    Debug.Log("====보석이 부족합니다=====");
                    return;
                }
                else
                {
                    DataSave._instance.setBosuk_GameMinus(32500);
                    DataSave._instance.setMoney_Game(100000);
                    gameMoney.text = DataSave._instance.getMoney_Game().ToString();
                }
                break;
        }
    }
    public void backgroundMusicGo(int musicOnOff) //사운드
    {
        if (musicOnOff == 0)
        {
            Debug.Log("abc");
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

    public void greenBoxBtnFunc() //그린박스
    {
        Debug.Log("몇번?");
        float gameMoney_ = DataSave._instance.getMoney_Game();
        if (gameMoney_ < 1000)
            Debug.Log("돈이 부족합니다.");
        else
        {
            DataSave._instance.setMoney_GameMinus(1000);
            animator = goldDraw.GetComponentInChildren<Animator>();
            animator.SetBool("Ok", true);
            gameMoney.text = DataSave._instance.getMoney_Game().ToString();
            greenBoxBtn.gameObject.SetActive(false);
            redBoxBtn.gameObject.SetActive(false);
            blueBoxBtn.gameObject.SetActive(false);
            StartCoroutine(ChaWaitTime(0));
        }
    }

    public void redBoxBtnFunc() //레드박스
    {
        float gameMoney_ = DataSave._instance.getMoney_Game();
        if (gameMoney_ < 2500)
            Debug.Log("돈이 부족합니다.");
        else
        {
            DataSave._instance.setMoney_GameMinus(2500);
            animator = bosukDraw.GetComponentInChildren<Animator>();
            animator.SetBool("Ok", true);
            gameMoney.text = DataSave._instance.getMoney_Game().ToString();
            greenBoxBtn.gameObject.SetActive(false);
            redBoxBtn.gameObject.SetActive(false);
            blueBoxBtn.gameObject.SetActive(false);
            StartCoroutine(ChaWaitTime(1));
        }
    }
    public void blueBoxBtnFunc() //광고 박스
    {
        animator = bosukDraw.GetComponentInChildren<Animator>();
        animator.SetBool("Ok", true);
        greenBoxBtn.gameObject.SetActive(false);
        redBoxBtn.gameObject.SetActive(false);
        blueBoxBtn.gameObject.SetActive(false);
        StartCoroutine(ChaWaitTime(2));
    }
    IEnumerator ChaWaitTime(int whereBox)
    {
        randomCharacter = Random.Range(2, 4);
        switch (whereBox)
        {
            case 0:
                yield return new WaitForSeconds(3.3f);
                rabbit = Instantiate(Resources.Load("character/0che_rabbit" + randomCharacter), new Vector3(-24.08f, -2.55f, -163.23f), Quaternion.identity) as GameObject;

                greenBoxBtnOk.gameObject.SetActive(true);
                break;
            case 1:
                yield return new WaitForSeconds(3.3f);
                rabbit = Instantiate(Resources.Load("character/0che_rabbit" + randomCharacter), new Vector3(-19.13f, -2.55f, -163.23f), Quaternion.identity) as GameObject;
                redBoxBtnOk.gameObject.SetActive(true);
                break;
            case 2:
                yield return new WaitForSeconds(3.3f);
                rabbit = Instantiate(Resources.Load("character/0che_rabbit" + randomCharacter), new Vector3(-14.38f, -2.55f, -163.23f), Quaternion.identity) as GameObject;
                blueBoxBtnOk.gameObject.SetActive(true);
                break;
        }
    }
    void OkSign(int characterIndex)
    {
        Destroy(rabbit);
        ES2.Save<int>(randomCharacter, "character" + randomCharacter.ToString());
        animator.SetBool("Ok", false);
        switch (characterIndex)
        {
            case 0:
                greenBoxBtnOk.gameObject.SetActive(false);
                break;
            case 1:
                redBoxBtnOk.gameObject.SetActive(false);
                break;
            case 2:
                blueBoxBtnOk.gameObject.SetActive(false);
                break;
        }
        greenBoxBtn.gameObject.SetActive(true);
        redBoxBtn.gameObject.SetActive(true);
        blueBoxBtn.gameObject.SetActive(true);
    }
    public void chaFunc(string cha_index)
    {
        int cha_test = int.Parse(cha_index);

        switch (cha_test)
        {
            case 0:
                chaSetFalse();
                cha_Array[0].SetActive(true);
                ES2.Save<int>(0, "rabbit");
                break;
            case 1:
                chaSetFalse();
                cha_Array[1].SetActive(true);
                ES2.Save<int>(1, "rabbit");
                break;
            case 2:
                chaSetFalse();
                cha_Array[2].SetActive(true);
                ES2.Save<int>(2, "rabbit");
                break;
        }
    }
    public void chaSetFalse()
    {
        goldDraw.SetActive(false);
        bosukDraw.SetActive(false);
        adDraw.SetActive(false);
        for (int i = 0; i < cha_Array.Length; i++)
        {
            cha_Array[i].SetActive(false);
        }
    }
    public void GoogleBtnFunc(int googleOnOff)
    {
        //0 == 로그인 , 1 == 비로그인.
        if (googleOnOff == 0)
        {
            Debug.Log("----로그인----");
            GoogleManager.GetInstance.LogoutGPGS();
            GoogleBtnOff.gameObject.SetActive(true);
            GoogleBtnOn.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("---비로그인----");
            GoogleManager.GetInstance.LoginGPGS();
            GoogleBtnOn.gameObject.SetActive(true);
            GoogleBtnOff.gameObject.SetActive(false);
        }
    }
}
