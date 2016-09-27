using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateUnitClass : MonoBehaviour {

    public static bool CreateUnitChk = false;
    public GameObject mude;
    public GameObject CreateOk;
    public GameObject[] unit = new GameObject[2];
    public GameObject[] Egg = new GameObject[2];
    GameObject EggTemp;

    public GameObject effectWhite;

    Button CreateOkBtn;
    int randomIndex;
	void Start ()
    {
        mude.SetActive(false);
        CreateOk.SetActive(false);
        effectWhite.SetActive(false);
        CreateOkBtn = CreateOk.GetComponent<Button>();
        CreateOkBtn.onClick.AddListener(createOkBtnFunc);

        switch (selectManager.eggNumber)
        {
            case 0:
                EggTemp = Instantiate(Egg[0], new Vector3(0.18f, 0.87f, -5.21f), Quaternion.identity) as GameObject;
                break;
            case 1:
                EggTemp = Instantiate(Egg[1], new Vector3(0.18f, 0.87f, -5.21f), Quaternion.identity) as GameObject;
                break;
            case 2:
                EggTemp = Instantiate(Egg[0], new Vector3(0.18f, 0.87f, -5.21f), Quaternion.identity) as GameObject;
                break;
        }
        StartCoroutine(waitFireWork());
	}
    IEnumerator waitFireWork()
    {
        yield return new WaitForSeconds(3.2f);
        effectWhite.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        createUnitFunc();
        mude.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CreateOk.SetActive(true);
        effectWhite.SetActive(false);
    }
    void createOkBtnFunc()
    {
        Debug.Log("aa");
        CreateUnitChk = true;
        ES2.Save<int>(randomIndex, "character" + randomIndex.ToString());
        Destroy(EggTemp);
        SceneManager.LoadScene(2);
    }
    void createUnitFunc()
    {
        randomIndex = Random.Range(0, 10);
        Instantiate(unit[randomIndex], new Vector3(0.74f, -0.73f, 2.29f), Quaternion.identity);
    }
}
