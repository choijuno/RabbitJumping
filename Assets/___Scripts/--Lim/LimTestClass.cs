using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LimTestClass : MonoBehaviour {

    Button Achievement;
    Button leaderBoard;

    Button CloudSave;
    Button CloudLoad;

    Button BoardSave;
    Button ScoreSave;

    Button FriendLoading;

    Button LoadLeaderBoard;

    Button itemBuyBtn;
    Text CloudText;
    Text ScoreTxt;
	// Use this for initialization
	void Start ()
    {
        Debug.Log("로그인");

        CloudSave = GameObject.Find("CloudSave").GetComponent<Button>();
        CloudSave.onClick.AddListener(CloudSaveFunc);
        CloudLoad = GameObject.Find("CloudLoad").GetComponent<Button>();
        CloudLoad.onClick.AddListener(CloudLoadFunc);
        CloudText = GameObject.Find("CloudText").GetComponent<Text>();
        Achievement = GameObject.Find("Achievement").GetComponent<Button>();
        Achievement.onClick.AddListener(ShowAchievementFunc);
        leaderBoard = GameObject.Find("LeaderBoard").GetComponent<Button>();
        leaderBoard.onClick.AddListener(ShowleaderBoardFunc);

        BoardSave = GameObject.Find("BoardSave").GetComponent<Button>();
        BoardSave.onClick.AddListener(leaderBoardSave);
        ScoreSave = GameObject.Find("ScoreSave").GetComponent<Button>();
        ScoreSave.onClick.AddListener(ScoreSaveFunc);

        LoadLeaderBoard = GameObject.Find("LoadLeaderBoard").GetComponent<Button>();
        LoadLeaderBoard.onClick.AddListener(LoadLeaderBoardFunc);

        ScoreTxt = ScoreSave.GetComponentInChildren<Text>();

        FriendLoading = GameObject.Find("FriendLoading").GetComponent<Button>();
        FriendLoading.onClick.AddListener(FriendLoadingFunc);


        itemBuyBtn = GameObject.Find("ItemBuy").GetComponent<Button>();
        itemBuyBtn.onClick.AddListener(itemBuyGo);
        if (!Social.localUser.authenticated)
        {
            GoogleManager.GetInstance.InitializeGPGS();
            GoogleManager.GetInstance.LoginGPGS();
        }
    }
    void CloudSaveFunc()
    {
        GoogleManager.GetInstance.SaveToCloud();
    }
    void CloudLoadFunc()
    {
        GoogleManager.GetInstance.LoadFromCloud();
    }
    void ShowAchievementFunc()
    {
        GoogleManager.GetInstance.ShowAchievement();
    }
    void ShowleaderBoardFunc()
    {
        GoogleManager.GetInstance.ShowLeaderboard();
    }
    void leaderBoardSave()
    {
        GoogleManager.GetInstance.ReportScoreLeaderBoard(PlayerPrefs.GetInt("LeaderSave"));
    }
    void ScoreSaveFunc()
    {
        int randindex = Random.Range(10, 20);
        PlayerPrefs.SetInt("LeaderSave", randindex);
        ScoreTxt.text = randindex.ToString();
    }
    void FriendLoadingFunc()
    {
        GoogleManager.GetInstance.LoadingFriends();
    }
    void LoadLeaderBoardFunc()
    {
        GoogleManager.GetInstance.LeaderBoardLoadScores();
    }
    void itemBuyGo()
    {
        SceneManager.LoadScene(2);
    }
}
