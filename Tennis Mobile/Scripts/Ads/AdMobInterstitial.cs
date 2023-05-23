using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;

public class AdMobInterstitial : MonoBehaviour
{
    public void Start()
    {
        // Google Mobile Ads SDK を初期化します。
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // このコールバックは、MobileAds SDK が初期化されると呼び出されます。
            this.LoadInterstitialAd();
        });
    }


    // これらの広告ユニットは、常にテスト広告を配信するように設定されています。
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/1033173712";      // テスト広告
    //private string _adUnitId = "ca-app-pub-8673262984895359~3819444368";      // リリース用広告

#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";      // テスト広告
    //private string _adUnitId = "ca-app-pub-8673262984895359~9037425102";      // リリース用広告

#else
  private string _adUnitId = "unused";
#endif

    private InterstitialAd interstitialAd;

    /// <summary>
    /// インタースティシャル広告を読み込みます。
    /// </summary>
    public void LoadInterstitialAd()
    {
        // 新しい広告をロードする前に、古い広告をクリーンアップしてください。
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // 広告の読み込みに使用するリクエストを作成します。
        var adRequest = new AdRequest.Builder()
                .AddKeyword("unity-admob-sample")
                .Build();

        // 広告を読み込むリクエストを送信します。
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // error が null でない場合、読み込みリクエストは失敗しました。
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });
    }


    /// <summary>
    /// インタースティシャル広告を表示します。
    /// </summary>
    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }


    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // 広告が収益を上げたと推定される場合に発生します。
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // 広告のインプレッションが記録されたときに発生します。
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // 広告のクリックが記録されたときに発生します。
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // 広告がフルスクリーン コンテンツを開いたときに発生します。
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // 広告がフルスクリーン コンテンツを閉じたときに発生します。
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");


        };
        // 広告がフルスクリーン コンテンツを開くことができなかった場合に発生します。
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    private void RegisterReloadHandler(InterstitialAd ad)
    {
        // 広告がフルスクリーン コンテンツを閉じたときに発生します。
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            // できるだけ早く別の広告を表示できるように、広告を再読み込みしてください。
            LoadInterstitialAd();
        };
        // 広告がフルスクリーン コンテンツを開くことができなかった場合に発生します。
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // できるだけ早く別の広告を表示できるように、広告を再読み込みしてください。
            LoadInterstitialAd();
        };
    }

}
