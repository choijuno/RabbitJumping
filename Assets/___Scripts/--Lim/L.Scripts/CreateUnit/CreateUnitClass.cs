using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateUnitClass : MonoBehaviour {

    public static bool CreateUnitChk = false;
	public Text GetIt;
    public GameObject mude;
    public GameObject CreateOk;
    public GameObject[] unit;
    public GameObject[] Egg = new GameObject[2];
    GameObject EggTemp;

    public GameObject effectWhite;

    Button CreateOkBtn;
    int randomIndex;
	void Start ()
    {
        DataSave._instance.setDraw(1);
        int DrawCount = DataSave._instance.getDraw();

        if (DrawCount == 10)
            Social.ReportProgress(GPGS.achievement_draw1, 100.0f, (bool success) => { });
        else if (DrawCount == 30)
            Social.ReportProgress(GPGS.achievement_draw2, 100.0f, (bool success) => { });
        else if (DrawCount == 50)
            Social.ReportProgress(GPGS.achievement_draw3, 100.0f, (bool success) => { });

        mude.SetActive(false);
        CreateOk.SetActive(false);
        effectWhite.SetActive(false);
        CreateOkBtn = CreateOk.GetComponent<Button>();
        CreateOkBtn.onClick.AddListener(createOkBtnFunc);

        switch (selectManager.eggNumber)
        {
            case 0:
                EggTemp = Instantiate(Egg[0], new Vector3(0, 0.87f, -5.21f), Quaternion.identity) as GameObject;
                break;
            case 1:
                EggTemp = Instantiate(Egg[1], new Vector3(0, 0.87f, -5.21f), Quaternion.identity) as GameObject;
                break;
            case 2:
                EggTemp = Instantiate(Egg[0], new Vector3(0, 0.87f, -5.21f), Quaternion.identity) as GameObject;
                break;
        }

		if (!ES2.Exists("Language"))
        {
            ES2.Save<bool>(false, "Language");
			GetIt.text = "Get It";
        }

        if (ES2.Load<bool>("Language"))
        {
			GetIt.text = "획득하기";
        }
        else
        {
			GetIt.text = "Get It";
        }

        StartCoroutine(waitFireWork());
	}
    IEnumerator waitFireWork()
    {
        yield return new WaitForSeconds(3.2f);
        effectWhite.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        switch (selectManager.eggNumber)
        {
            case 0:
                createUnitFunc();
                break;
            case 1:
                createUnitFunc1();
                break;
            case 2:
                createUnitFunc2();
                break;
        }
        mude.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CreateOk.SetActive(true);
        effectWhite.SetActive(false);
    }
    void createOkBtnFunc() //획득하기를 클릭한다면.
    {
        CreateUnitChk = true;
        ES2.Save<int>(randomIndex, "character" + randomIndex.ToString());

        /*
        for (int i = 0; i < 20; i++)
        {
            if(ES2.Exists("character" + i))
            {
                DataSave._instance.setSkin(1);
            }
        }
        int skinCount = DataSave._instance.getSkin();

        if(skinCount == 10)
        {
            Social.ReportProgress(GPGS.achievement_skin1, 100.0f, (bool success) => { });
        }
        else if(skinCount == 20)
        {
            Social.ReportProgress(GPGS.achievement_skin2, 100.0f, (bool success) => { });
        }
        */
        Destroy(EggTemp);
        SceneManager.LoadScene(3);
    }
    void createUnitFunc()
    {
        randomIndex = Random.Range(0, 10);
        Instantiate(unit[randomIndex], new Vector3(0f, -0.73f, 2.29f), Quaternion.identity);
    }
    void createUnitFunc1()
    {
        randomIndex = Random.Range(10, 15);
        Instantiate(unit[randomIndex], new Vector3(0f, -0.73f, 2.29f), Quaternion.identity);
    }
    void createUnitFunc2()
    {
        randomIndex = Random.Range(15, 20);
        Instantiate(unit[randomIndex], new Vector3(0f, -0.73f, 2.29f), Quaternion.identity);
    }
}
