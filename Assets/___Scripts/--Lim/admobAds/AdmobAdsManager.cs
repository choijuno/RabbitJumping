using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class AdmobAdsManager : MonoBehaviour {

    BannerView bannerView = null;

    string BannerId = "ca-app-pub-8370172094711212/4315872083";
    // Use this for initialization
    
    void Start ()
    {
        bannerView = new BannerView(BannerId, AdSize.Banner, AdPosition.TopLeft);
        AdRequest.Builder builder = new AdRequest.Builder();

        AdRequest request = new AdRequest.Builder().Build();
        request = builder.Build();

        bannerView.LoadAd(request);
        BottomBannerShow();
       
    }
    void BottomBannerShow()
    {
        bannerView.Show();
    }
    void OnDisable() //배너 안보이게.
    {
        bannerView.Hide();
    }
}
