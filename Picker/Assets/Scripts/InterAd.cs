using UnityEngine;
using GoogleMobileAds.Api;

public class InterAd : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    private string interstitialUnitId = "ca-app-pub-3076913227168198/5650998220";

    private void OnEnable()
    {
        interstitialAd = new InterstitialAd(interstitialUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    public void ShowAd()
    {
        if(interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }
}
