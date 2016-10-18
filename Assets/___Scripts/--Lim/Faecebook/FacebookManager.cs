using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour
{
    public GameObject Friend;
    private static string applink = "https://fb.me/775279745947064";
    public GameObject FacebookOn;
    public GameObject FacebookOff;

    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack);
        }
       
    }
    void InitCallBack() //초기화콜백
    {
        Debug.Log("FB has been initiased.");
    }

    public void Login() //로그인
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack);
        }
        if (!FB.IsLoggedIn)
        {
            FB.LogInWithReadPermissions(new List<string> { "user_friends" }, LoginCallBack);
        }
    }

    void LoginCallBack(ILoginResult result) //로그인 콜백
    {
        if(result.Error == null)
        {
            Debug.Log("FB has logged in.");
            //showUI();
            FacebookOn.SetActive(true);
            FacebookOff.SetActive(false);
        }
        else
        {
            Debug.Log("Error During Login " + result.Error);
            FacebookOn.SetActive(false);
            FacebookOff.SetActive(true);
        }
    }
    void showUI()
    {
        if (FB.IsLoggedIn)
        {
            FB.API("me/picture?width=100&height=100", HttpMethod.GET, PictureCallBack);
            FB.API("me?fields=first_name", HttpMethod.GET, NameCallBack);
            FB.API("me/friends", HttpMethod.GET, FriendCallBack);
            FB.GetAppLink(ApplinkCallback);
        }else
        {
        }
    }

    void PictureCallBack(IGraphResult result)
    {
        Texture2D image = result.Texture;
        //LoggedInUI.transform.FindChild("ProfilePicture").GetComponent<Image>().sprite = Sprite.Create(image, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
    }

    void NameCallBack(IGraphResult result)
    {
        IDictionary<string, object> profile = result.ResultDictionary;
        //LoggedInUI.transform.FindChild("Name").GetComponent<Text>().text = "Hello " + profile["first_name"];

    }

    public void LogOut() //로그아웃
    {
        if (FB.IsLoggedIn)
        {
            FB.LogOut();
            FacebookOn.SetActive(false);
            FacebookOff.SetActive(true);
        }
    }

    public void Share()
    {
        FB.ShareLink(new System.Uri("http://naver.com"), "This game is awesome!", "A description of the game.", new System.Uri("http://naver.com/"));
    }

    public void Invite() //초대
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack);
        }
        if (!FB.IsLoggedIn)
        {
            Login();
        }

        FB.Mobile.AppInvite(new System.Uri(applink), null, InviteCallBack);
        //FB.AppRequest(message: "You should really try this game.", title: "Check this super game!");

    }

    void InviteCallBack(IAppInviteResult result) //초대 콜백
    {
        if (result.Cancelled)
        {
            Debug.Log("Invite cancelled.");
        }else if(!string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Error in ivite: " + result.Error);
        }else
        {
            Debug.Log("Invite was successful : " + result.RawResult);
        }
    }
    
    public void Challenge()
    {
        FB.AppRequest("Custom message", null, new List<object> { "app_users" }, null, null, "Data", "Challenge your friends!", ChallengeCallback);
    }
    void ChallengeCallback(IAppRequestResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("Challenge cancelled.");
        }
        else if (!string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Error in ivite: " + result.Error);
        }
        else
        {
            Debug.Log("Challenge was successful : " + result.RawResult);
        }
    }
    private void FriendCallBack(IGraphResult result)
    {
        IDictionary<string, object> data = result.ResultDictionary;

        List<object> friends = (List<object>)data["data"];

        foreach(object obj in friends)
        {
            Dictionary<string, object> dictio = (Dictionary<string, object>)obj;
            CreateFriend(dictio["name"].ToString(), dictio["id"].ToString());
        }
    }

    void CreateFriend(string name, string id)
    {
        GameObject myFriend = Instantiate(Friend);
        //Transform parent = LoggedInUI.transform.FindChild("ListContainer").FindChild("FriendsList");
        //myFriend.transform.SetParent(parent);

        myFriend.GetComponentInChildren<Text>().text = name;
        FB.API(id + "/picture?width=100&height=100", HttpMethod.GET, delegate (IGraphResult result)
        {
            myFriend.GetComponentInChildren<Image>().sprite = Sprite.Create(result.Texture, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
        });

    }

    void ApplinkCallback(IAppLinkResult result)
    {
        if (string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Applink Done : " + result.RawResult);
            IDictionary<string, object> dictio = result.ResultDictionary;
            if (dictio.ContainsKey("target_url"))
            {
                string url = dictio["target_url"].ToString();
                string keyword = "request_ids=";
                int k = 0;
                while (k < url.Length - keyword.Length && !url.Substring(k, keyword.Length).Equals(keyword))
                    k++;
                k += keyword.Length;
                int l = k;

                while (url[l] != '&' && url[l] != '%')
                    l++;

                string id = url.Substring(k, l - k);
                FB.API("/" + id + "_" + AccessToken.CurrentAccessToken.UserId, HttpMethod.GET, RequestCallback);
            }
        }
        else
        {
            Debug.Log("Applink error : " + result.Error);
        }
    }

    void RequestCallback(IGraphResult result)
    {
        if (string.IsNullOrEmpty(result.Error))
        {
            IDictionary<string, object> dictio = result.ResultDictionary;
            if (dictio.ContainsKey("data"))
            {
                Debug.Log(dictio["data"]);
            }
            if (dictio.ContainsKey("id"))
            {
                FB.API("/" + dictio["id"], HttpMethod.DELETE, null);
            }
        }else
        {
            Debug.Log("Error in request: " + result.Error);
        }
    }
}

