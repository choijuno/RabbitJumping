using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class selectManager : MonoBehaviour {
    
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

    public GameObject goldDraw;
    public GameObject bosukDraw;
    public GameObject adDraw;

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
    public Button tiltOn;
    public Button tiltOff;

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


    //캐릭터
    GameObject rabbit;
    Button greenBoxBtnOk;
    Button redBoxBtnOk;
    Button blueBoxBtnOk;

    GameObject preCharacter;
    int randomCharacter;
    Animator animator;
    void Start ()
    {
		/*
        for(int i = 0; i < 5; i++)
        {
            ES2.Delete("character" + i.ToString());
        }
        ES2.Delete("rabbit");
*/
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
        tiltOn.onClick.AddListener(() => tilt(true));
        tiltOff.onClick.AddListener(() => tilt(false));
        hyogwaMusicOn.onClick.AddListener(() => HyoGwaSound(true));
        hyogwaMusicOff.onClick.AddListener(() => HyoGwaSound(false));

        if (!ES2.Exists("tilt"))
        {
            ES2.Save<bool>(false, "tilt");
            tiltOff.gameObject.SetActive(true);
            tiltOn.gameObject.SetActive(false);
        }
        if (ES2.Load<bool>("tilt"))
        {
            tiltOn.gameObject.SetActive(true);
            tiltOff.gameObject.SetActive(false);
        }
        else
        {
            tiltOn.gameObject.SetActive(false);
            tiltOff.gameObject.SetActive(true);
        }

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

        if (!ES2.Exists("HyoGwaSound"))
            ES2.Save<bool>(true, "HyoGwaSound");

        if (ES2.Load<bool>("HyoGwaSound"))
        {
            hyogwaMusicOff.gameObject.SetActive(false);
            hyogwaMusicOn.gameObject.SetActive(true);
        }
        else
        {
            hyogwaMusicOff.gameObject.SetActive(true);
            hyogwaMusicOn.gameObject.SetActive(false);
        }

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

        Debug.Log(cha_scrollpanel.transform.childCount);
        for(int i = 0; i< cha_scrollpanel.transform.childCount; i++)
        {
            Button chaBtn_a = cha_scrollpanel.transform.GetChild(i).GetComponent<Button>();
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
        if(CreateUnitClass.CreateUnitChk == true)
        {
            MyBtnFunc();
            CreateUnitClass.CreateUnitChk = false;
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
                Debug.Log("test===" + test);
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
        MusicManager.instance.PlayOnShot();
        test = scrollPanel.localPosition;
        ES2.Save<Vector2>(test, "scrollPanel");
        Debug.Log("====처음벡터===" + ES2.Load<Vector2>("scrollPanel"));
        GameManager.TestNum = System.Convert.ToInt32(name);
        mainSceneManager.SceneIndex = 2;
        StartCoroutine(waitLoadingScene());
    }
    IEnumerator waitLoadingScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }
}
