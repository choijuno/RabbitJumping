using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class GoogleManager : GoogleSingleton<GoogleManager> {


    [Serializable]
    public class TestScore
    {
        public int test;
    }

    TestScore TS = new TestScore();
    Text CloudText;


    RawImage testfriendImg;
    void Start()
    {
        //testfriendImg = GameObject.Find("testfriendImg").GetComponent<RawImage>();
        DontDestroyOnLoad(this);
    }
    public bool bLogin
    {
        get;
        set;
    }
    
    public void InitializeGPGS() //초기화.
    {
        TS.test = 100;
        bLogin = false;
        //CloudText = GameObject.Find("CloudText").GetComponent<Text>();
        //PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();
    }
    public bool CheckLogin()//로그인 상태확인.
    {
        return Social.localUser.authenticated;
    }
    public void SaveToCloud()//클라우드 세이브.
    {
        if (!Social.localUser.authenticated) //로그인 안되있으면 리턴.
        {
            LoginGPGS();

            return;
        }

        CloudText.text = "첫번째 세이브";
        OpenSavedGame("SaveTest", true);
    }
    void OpenSavedGame(string filename, bool bSave)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        if (bSave)
        {
            CloudText.text = "두번째 세이브";
            Debug.Log("========> 두번째 저장");
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToSave);
        }
        else
        {
            Debug.Log("=======> 두번째 로드");
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToRead);
        } 
    }
    void OnSavedGameOpenedToSave(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            /*BinaryFormatter b = new BinaryFormatter();
            MemoryStream m = new MemoryStream();
            b.Serialize(m, TS.test);
            byte[] bytes = m.GetBuffer();
            */

            byte[] bytes = ObjectToByteArraySerialize(TS.test);
            

            //파일이 준비되었으므로, 실제 게임 저장을 수행.
            //저장할데이터바이트배열에 저장하실 바이트 배열을 지정합니다.
            SaveGame(game, bytes, DateTime.Now.TimeOfDay);
        }
        else
        {
            CloudText.text = "실패";
            //파일 열기에 실패
        }
    }
    void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);

        SavedGameMetadataUpdate updateMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updateMetadata, savedData, OnSavedGameWritten);
    }
    void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            CloudText.text = "완료";
            //저장완료.
        }
        else
        {
            //저장실패.
            CloudText.text = "실패";
        }
    }

    public void LoadFromCloud() //클라우드 로그시키기.
    {
        if (!Social.localUser.authenticated) //로그인 안되있으면 리턴.
        {

            LoginGPGS();

            return;
        }

        OpenSavedGame("SaveTest", false);
     
    }
    void OnSavedGameOpenedToRead(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            Debug.Log("======로드 성공");
            CloudText.text = "성공";
            LoadGameData(game);
        }
        else
        {
            Debug.Log("======로드 실패");
            CloudText.text = "실패";
            //파일열기 실패 오류메세지.
        }
    }
    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSaveGameDataRead);
    }
    void OnSaveGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            //데이터 읽기에 성공했습니다.
            //data 배열을 복구해서 적절하게 사용.
            int test = Deserialize<int>(data);
            Debug.Log("=====2=> : " + test);
        }
        else
        {
            CloudText.text = "실패";
            //읽기 실패 했다. 오류출력.
        }
    }
    public void ReportScoreLeaderBoard(int score) //리더보드에 스코어 저장.
    {
        if (!Social.localUser.authenticated)
        {
            LoginGPGS();
            return;
        }
        Social.ReportScore(score, GPGS.LeaderBoardTest, (bool success) =>
        {
            if (success)
            {
                Debug.Log("리더보드 성공");
            }
            else
            {
                Debug.Log("리더보드 실패");
            }
        });
    }
    public void LeaderBoardLoadScores() //리더보드 스코어 가져오는것.
    {
        if (!Social.localUser.authenticated)
        {
            LoginGPGS();
            return;
        }
        ILeaderboard lb = Social.CreateLeaderboard();
        lb.id = GPGS.LeaderBoardTest;
        lb.LoadScores(ok =>
        {
            if (ok)
            {
                LoadUsersAndDisplay(lb);
            }
            else
            {
                Debug.Log("Error retrieving leaderboardi");
            }
        });
    }
    internal void LoadUsersAndDisplay(ILeaderboard lb) //리더보드 디스플레이
    {
        // get the user ids
        List<string> userIds = new List<string>();

       
        foreach (IScore score in lb.scores)
        {
            Debug.Log("lb.score === " + score.value);
            Debug.Log("Social.localUser ==== " + Social.localUser.id);
            Debug.Log("score.userId ==== " + score.userID);
            userIds.Add(score.userID);
        }
        // load the profiles and display (or in this case, log)
        Social.LoadUsers(userIds.ToArray(), (users) =>
        {
            string status = "Leaderboard loading: " + lb.title + " count = " +
                lb.scores.Length;
            foreach (IScore score in lb.scores)
            {
                IUserProfile user = FindUser(users, score.userID);
                status += "\n" + score.formattedValue + " by " +
                    (string)(
                        (user != null) ? user.userName : "**unk_" + score.userID + "**");
            }

            Debug.Log(status);
        });
    }
    IUserProfile FindUser(IUserProfile[] users, string userID)
    {
        foreach (IUserProfile user in users)
            if (user.id == userID) return user;
        return null;
    }
    public void LoadingFriends() //친구들 불러오는곳
    {
        if (!Social.localUser.authenticated)
        {
            LoginGPGS();
            return;
        }
        PlayGamesPlatform.Instance.GetServerAuthCode((status,callback) =>
        {
            if(CommonStatusCodes.Success == status)
            {
                Debug.Log("서버 성공 :" + callback);
            }
            else
            {
                Debug.Log("에러 메일 : " + status);
            }
        });
        /* 유저 email
        PlayGamesPlatform.Instance.GetUserEmail((status, email) =>
        {
            if (status == CommonStatusCodes.Success)
            {
                Debug.Log("주소는 : " + email);
            }
            else
            {
                Debug.Log("에러 메일 : " + status);
            }
        });*/

        Social.localUser.LoadFriends((ok) =>
        {
            foreach (IUserProfile p in Social.localUser.friends)
            {
                //이름, 아이디 등등 가져올 수 있다.
                testfriendImg.texture = p.image;
            }
        });
    }
    public byte[] ObjectToByteArraySerialize(object obj) //모든 오브젝트를 바이트배열로 저장합니다. 클라우드 세이브.
    {
        using (var memoryStream = new MemoryStream())
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memoryStream, obj);
            memoryStream.Flush();
            memoryStream.Position = 0;

            return memoryStream.ToArray();
        }
    }
    public T Deserialize<T>(byte[] byteData) //바이트형태를 T타입으로 리턴해줌.
    {
        using (var stream = new MemoryStream(byteData))
        {
            var formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }
    public void ShowLeaderboard() //리더보드 보여주기
    {
        if (!Social.localUser.authenticated)
            LoginGPGS();

        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }
    public void ShowAchievement() //업적 보여주기
    {
        if (!Social.localUser.authenticated)
            LoginGPGS();

        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }
    public void LoginGPGS() //로그인
    {
        if (!Social.localUser.authenticated)
            Social.localUser.Authenticate(LoginCallBackGPGS);
    }

    public void LoginCallBackGPGS(bool result) //로그인
    {
        bLogin = result;
    }

    public void LogoutGPGS()//로그아웃
    {
        if(Social.localUser.authenticated)
        {
            ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
            bLogin = false;
        }
    }

    public Texture2D GetImageGPGS() //내 이미지
    {
        if (Social.localUser.authenticated)
            return Social.localUser.image;
        else
            return null;
    }
    
    public string GetNameGPGS() // 내 이름
    {
        if (Social.localUser.authenticated)
            return Social.localUser.userName;
        else
            return null;
    }
}
