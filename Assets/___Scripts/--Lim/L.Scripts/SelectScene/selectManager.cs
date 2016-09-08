using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class selectManager : MonoBehaviour {
    
    public GameObject ScrollPanel;
    public GameObject allBtnPanel;
    public GameObject ui_back_large;


    public Sprite clearSprite;
    public Sprite noneSprite; //자물쇠 이미지
    public Sprite newSprite; //새로열린 이미지
    public Sprite storeBtnClick;
    public Sprite myRoomBtnClick;
    public Sprite noneStoreBtn;
    public Sprite noneMyRoomBtn;

    public Canvas UiCanvas;

    public Button shopBtn;
    public Button MyBtn;
    public Button rangkingBtn;
    public Button tropyBtn;
    public Button setupBtn;

    public Button myRoomBtn;
    public Button storeBtn;
    //상점
    public GameObject storeAndRoom;
    public GameObject myRoom;
    public GameObject store;

    public Button storeRoomExit;

    public Button greenBoxBtn;
    public Button redBoxBtn;
    public Button blueBoxBtn;

    EventSystem eventSystem;

    public Text gameMoney;
    public Text bosukMoney;

    //마이룸.
    public GameObject cha_selectUi;
    public GameObject cha_btnAll;
    public GameObject empty_panel;
    public GameObject cha_scrollpanel;
    public Image[] chaArray = new Image[10];
    public Sprite[] nonImg = new Sprite[10];
    public Sprite[] okImg = new Sprite[10];
    public Button[] chaBtnArray = new Button[10];
    public GameObject[] cha_Array = new GameObject[3];

    //설정
    public GameObject setup;

    public Button setupExit;
    public Button backgroundMusicOn;
    public Button backgroundMusicOff;
    public Button hyogwaMusicOn;
    public Button hyogwaMusicOff;

    //골드 보석 상점
    public GameObject goldBosuk;
    public GameObject goldStore;
    public GameObject bosukStore;
    public Button goldbosukExitBtn;
    public Button goldBtn;
    public Button bosukBtn;

    public Button bosukRealBtn;
    public Button goldRealBtn;

    public Button bosuk1000won;
    public Button bosuk2500won;
    public Button bosuk32500won;

    public Button gold1000won;
    public Button gold2500won;
    public Button gold32500won;

    public Button GoogleBtnOn;
    public Button GoogleBtnOff;

    public Sprite nonGoldSprite;
    public Sprite GoldSprite;
    public Sprite nonBosukSprite;
    public Sprite BosukSprite;

    //스크롤저장
    public RectTransform scrollPanel;
    Vector2 test;

    void Start ()
    {
       
        if (ES2.Exists("scrollPanel"))
        {
            scrollPanel.localPosition = ES2.Load<Vector2>("scrollPanel");
            Debug.Log("====불로올 데이터====" + scrollPanel.localPosition);
        }
            
        selectInit();
    }
    void selectInit()
    {
        chaSetFalse();
        if (ES2.Exists("Money_Game"))
            gameMoney.text = DataSave._instance.getMoney_Game().ToString(); //돈 출력
        else
        {
            gameMoney.text = 5000.ToString("#,##0");
            DataSave._instance.setMoney_Game(5000);
        }

        if (ES2.Exists("bosuk_Game"))
            bosukMoney.text = DataSave._instance.getBosuk_Game().ToString(); //돈 출력
        else
            bosukMoney.text = 0.ToString("#,##0");

        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        eventSystem.pixelDragThreshold = (int)(0.5f * Screen.dpi / 2.54f);
     
        storeAndRoom.SetActive(false);


        //설정
        setup = UiCanvas.gameObject.transform.FindChild("setup").gameObject;
        
        backgroundMusicOn.onClick.AddListener(() => backgroundMusicGo(1));
        backgroundMusicOff.onClick.AddListener(() => backgroundMusicGo(0));
        
        GoogleBtnOn.onClick.AddListener(() => GoogleBtnFunc(0));
        GoogleBtnOff.onClick.AddListener(() => GoogleBtnFunc(1));

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
            

        if (!ES2.Exists("musicChk"))
            ES2.Save<bool>(true, "musicChk");

        if (ES2.Load<bool>("musicChk"))
        {
            backgroundMusicOff.gameObject.SetActive(false);
            backgroundMusicOn.gameObject.SetActive(true);
            MusicManager.instance.MusicSelect(true);
        }
        else
        {
            backgroundMusicOff.gameObject.SetActive(true);
            backgroundMusicOn.gameObject.SetActive(false);
            MusicManager.instance.MusicSelect(false);
        }
        
        hyogwaMusicOff.gameObject.SetActive(false);
        hyogwaMusicOn.gameObject.SetActive(true);
        
        setupExit.onClick.AddListener(setupExitBtnFunc);
        setup.SetActive(false);

        //골드 보석 상점
        goldBosuk = UiCanvas.gameObject.transform.FindChild("goldBosuk").gameObject;
        goldStore = goldBosuk.transform.FindChild("goldStore").gameObject;
        bosukStore = goldBosuk.transform.FindChild("bosukStore").gameObject;
        
        bosukRealBtn.onClick.AddListener(bosukRealBtnFunc);
        goldRealBtn.onClick.AddListener(goldRealBtnFunc);
        bosukBtn.onClick.AddListener(bosukBtnFunc);
        goldBtn.onClick.AddListener(goldBtnFunc);
        goldbosukExitBtn.onClick.AddListener(goldbosukExitBtnFunc);

        gold1000won.onClick.AddListener(() => goldBuy(1000));
        gold2500won.onClick.AddListener(() => goldBuy(2500));
        gold32500won.onClick.AddListener(() => goldBuy(32500));
        
        bosuk1000won.onClick.AddListener(() => bosukBuy(1000));
        bosuk2500won.onClick.AddListener(() => bosukBuy(2500));
        bosuk32500won.onClick.AddListener(() => bosukBuy(32500));

        goldBosuk.SetActive(false);

        //사운드 옵션
        myRoom = ui_back_large.transform.FindChild("MyRoom").gameObject;
        cha_selectUi = myRoom.transform.FindChild("cha_selectUi").gameObject;
        empty_panel = cha_selectUi.transform.GetChild(0).gameObject;
        cha_scrollpanel = empty_panel.transform.GetChild(0).gameObject;
        cha_btnAll = cha_scrollpanel.transform.FindChild("cha_btnAll").gameObject;

        for(int i = 0; i< cha_btnAll.transform.childCount; i++)
        {
            Button chaBtn_a = cha_btnAll.transform.GetChild(i).GetComponent<Button>();
            chaBtn_a.onClick.AddListener(() => chaFunc(chaBtn_a.name));
        }

        store = ui_back_large.transform.FindChild("Store").gameObject;

        storeRoomExit.onClick.AddListener(storeRoomExitFunc);
        
		if (!ES2.Exists("stageIndexCount"))
			ES2.Save<int>(0, "stageIndexCount");
		
		if (ES2.Exists ("stageIndexCount")) {
			for (int i = 0; i < ScrollPanel.transform.childCount - 1; i++) {
				Button stageBtn = ScrollPanel.transform.GetChild (i + 1).gameObject.GetComponent<Button> (); //버튼들
				Image stageImg = stageBtn.GetComponent<Image> ();
				Text stageText = stageBtn.transform.GetChild (3).GetComponent<Text> ();


				if (i > ES2.Load<int> ("stageIndexCount")) {
					stageText.text = "";
					stageImg.sprite = noneSprite;
					stageBtn.interactable = false;
				} else {
					stageText.text = (i + 1).ToString ();
					if (i == ES2.Load<int> ("stageIndexCount"))
						stageImg.sprite = newSprite;
					else
						stageImg.sprite = clearSprite;

					stageBtn.interactable = true;
				}
               

				for (int j = 0; j < 3; j++)
                {
					stageBtn.transform.GetChild (j).gameObject.SetActive (false);
				}
				stageBtn.onClick.AddListener (() => SceneGo (stageBtn.name));
			}
		}

        loadStar(); //별 로드
        loadNoneStage();
    }

    void loadStar()
    {
        string[] test = new string[3];
        GameObject[] star = new GameObject[3];
        Button stageBtn;
        if (ES2.Exists("stageIndexCount"))
        {
            for (int i = 0; i < ES2.Load<int>("stageIndexCount"); i++)
            {
                test = ES2.LoadArray<string>("ValueKey" + (i + 1));
                stageBtn = ScrollPanel.transform.GetChild(i + 1).gameObject.GetComponent<Button>();
                for (int j = 0; j < test.Length; j++)
                {
                    float[] stage = new float[3];
                    stage[j] = float.Parse(test[j]);

                    for (int k = 0; k < stage[1]; k++)
                    {
                        star[k] = stageBtn.transform.GetChild(k).gameObject;
                        star[k].SetActive(true);
                    }
                }
            }
        }
    }
    void loadNoneStage()
    {

    }
    void SceneGo(string name) //게임씬으로 넘기자
    {
        test = scrollPanel.localPosition;
        ES2.Save<Vector2>(test, "scrollPanel");
        Debug.Log("====처음벡터===" + ES2.Load<Vector2>("scrollPanel"));
        GameManager.TestNum = System.Convert.ToInt32(name);
        mainSceneManager.SceneIndex = 2;
        SceneManager.LoadScene(1);
    }
    public void shopBtnFunc() //상점 버튼 눌렀을때.
    {
        chaSetFalse();
        storeBtn.GetComponent<Image>().sprite = storeBtnClick;
        storeBtn.GetComponent<Image>().SetNativeSize();
        myRoomBtn.GetComponent<Image>().sprite = noneMyRoomBtn;
        myRoomBtn.GetComponent<Image>().SetNativeSize();
        storeAndRoom.SetActive(true);
        store.SetActive(true);
        myRoom.SetActive(false);
    }
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
        for(int i = 0; i < 10; i++)
        {
            if(ES2.Exists("character" + i.ToString()))
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
    public void greenBoxBtnFunc() //그린박스
    {
        float gameMoney = DataSave._instance.getMoney_Game();
        if (gameMoney < 1000)
            Debug.Log("돈이 부족합니다.");
        else
        {
            DataSave._instance.setMoney_GameMinus(1000);
            SceneManager.LoadScene(5);
        }
    }

    public void redBoxBtnFunc() //레드박스
    {
        float gameMoney = DataSave._instance.getMoney_Game();
        if (gameMoney < 2500)
            Debug.Log("돈이 부족합니다.");
        else
        {
            DataSave._instance.setMoney_GameMinus(2500);
            SceneManager.LoadScene(5);
        }
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
        for(int i = 0; i < cha_Array.Length; i++)
        {
            cha_Array[i].SetActive(false);
        }
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
                if(bosukCount < 1000)
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
        if(musicOnOff == 0)
        {
            Debug.Log("abc");
            backgroundMusicOn.gameObject.SetActive(true);
            backgroundMusicOff.gameObject.SetActive(false);
            MusicManager.instance.MusicSelect(true);

        }
        else if(musicOnOff == 1)
        {
            backgroundMusicOn.gameObject.SetActive(false);
            backgroundMusicOff.gameObject.SetActive(true);
            MusicManager.instance.MusicSelect(false);
        }
    }
    public void GoogleBtnFunc(int googleOnOff)
    {
        //0 == 로그인 , 1 == 비로그인.
        if(googleOnOff == 0)
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
