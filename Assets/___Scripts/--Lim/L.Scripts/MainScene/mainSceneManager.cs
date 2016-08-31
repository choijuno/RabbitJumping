using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainSceneManager : MonoBehaviour {

    public static int SceneIndex = 0; // 1은 메인에서 넘길때, 2는 셀렉트에서 넘길때, 3은 게임에서 넘길때
    Button startBtn;
    Button faceBookBtn;
    Button guestLoginBtn;

    public Image Panel;
    void Start ()
    {
        startBtn = GameObject.Find("startBtn").GetComponent<Button>();
        startBtn.onClick.AddListener(startBtnFunc);

        faceBookBtn = GameObject.Find("googleLogin").GetComponent<Button>();
        faceBookBtn.onClick.AddListener(faceBookBtnFunc);

        guestLoginBtn = GameObject.Find("guestLogin").GetComponent<Button>();
        guestLoginBtn.onClick.AddListener(guestLoginFunc);

        Panel.gameObject.SetActive(false);

        GoogleManager.GetInstance.InitializeGPGS();

        if (!Social.localUser.authenticated)
            GoogleManager.GetInstance.LoginGPGS();
    }
	void startBtnFunc()
    {
        SceneIndex = 1;
        Panel.gameObject.SetActive(true);
        StartCoroutine(waitLoad());
    }
    IEnumerator waitLoad()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(2);
    }
    void faceBookBtnFunc() //구글
    {
        GoogleManager.GetInstance.InitializeGPGS();

        if(!Social.localUser.authenticated)
            GoogleManager.GetInstance.LoginGPGS();
    }
    void guestLoginFunc() //게스트 로그인
    {
        SceneManager.LoadScene(2);
    }
}
