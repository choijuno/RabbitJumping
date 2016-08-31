using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateUnitClass : MonoBehaviour {
    
    public GameObject fireWorkParticle;
    public GameObject mude;
    public GameObject CreateOk;
    public GameObject[] unit = new GameObject[2];

    Button CreateOkBtn;
    int randomIndex;
	void Start ()
    {
        mude.SetActive(false);
        CreateOk.SetActive(false);

        CreateOkBtn = CreateOk.GetComponent<Button>();
        CreateOkBtn.onClick.AddListener(createOkBtnFunc);
        StartCoroutine(waitFireWork());
	}
    IEnumerator waitFireWork()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(fireWorkParticle, new Vector3(-2f, 3.2f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        mude.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        createUnitFunc();
        yield return new WaitForSeconds(0.5f);
        CreateOk.SetActive(true);
    }
    void createOkBtnFunc()
    {
        ES2.Save<int>(randomIndex, "character" + randomIndex.ToString());
        SceneManager.LoadScene(2);
    }
    void createUnitFunc()
    {
        randomIndex = Random.Range(0, 2);
        Instantiate(unit[randomIndex], new Vector3(0.55f, -0.4f, 2.29f), Quaternion.identity);
    }
}
