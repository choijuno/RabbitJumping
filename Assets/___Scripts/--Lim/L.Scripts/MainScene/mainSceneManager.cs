using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainSceneManager : MonoBehaviour {

    public static int SceneIndex = 0; // 1은 메인에서 넘길때, 2는 셀렉트에서 넘길때, 3은 게임에서 넘길때
    Button startBtn;
    Button faceBookBtn;

    public Image Panel;
    public Text GoogleLogin;
    void Start ()
    {
        startBtn = GameObject.Find("startBtn").GetComponent<Button>();
        startBtn.onClick.AddListener(startBtnFunc);

        
        //faceBookBtn = GameObject.Find("googleLogin").GetComponent<Button>();
        //faceBookBtn.onClick.AddListener(faceBookBtnFunc);

       /* if (!ES2.Exists("Language"))
        {
            ES2.Save<bool>(true, "Language");
            GoogleLogin.text = "구글 로그인 하기";
        }*/

        /*if (ES2.Load<bool>("Language"))
        {
            GoogleLogin.text = "구글 로그인 하기";
        }else
        {
            GoogleLogin.text = "Google Login";
        }*/

        Panel.gameObject.SetActive(false);

       /* GoogleManager.GetInstance.InitializeGPGS();

        if (!Social.localUser.authenticated)
            GoogleManager.GetInstance.LoginGPGS();
            */
    }
	void startBtnFunc()
    {
        SceneIndex = 1;
        SceneManager.LoadScene(3);
    }
    
    void faceBookBtnFunc() //구글
    {
        GoogleManager.GetInstance.InitializeGPGS();

        if(!Social.localUser.authenticated)
            GoogleManager.GetInstance.LoginGPGS();
    }
}
