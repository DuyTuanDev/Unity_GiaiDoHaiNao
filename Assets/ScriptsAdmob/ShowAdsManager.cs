using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
public class ShowAdsManager : MonoBehaviour
{
	//public string idAdMobInterstitial = "ca-app-pub-6897346409790831/1696418335";
	private string idAdMobBanner = "ca-app-pub-6897346409790831/6929946600";
	private string testDeviceId = "d2f9fb31-eeab-4476-8a6c-35a81e8ba984";


	public static ShowAdsManager Instance = null;
	//InterstitialAd interstitial;
	BannerView bannerView;

	void Awake()
	{
		//Check if instance already exists
		if (Instance == null)
		{
			//if not, set instance to this
			Instance = this;

		}
		//If instance already exists and it's not this:
		else if (Instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start()
	{
		LoadBannerAdmod();
	}

	//public void LoadFullAdmob()
	//{
	//	// Create an empty ad request.
	//	interstitial = new InterstitialAd(idAdMobInterstitial);
	//	AdRequest request = new AdRequest.Builder()
	//		.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
	//		.AddTestDevice(testDeviceId)  // My test device.
	//		.Build();
	//	// Load the interstitial with the request.
	//	interstitial.LoadAd(request);
	//}

	//public void ShowFullAdmod()
	//{
	//	interstitial.Show();
	//	LoadFullAdmob();
	//}

	public void LoadBannerAdmod()
	{
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(idAdMobBanner, AdSize.Banner, AdPosition.Bottom);
		// Create an empty ad request.
		AdRequest requestBanner = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
            .AddTestDevice(testDeviceId)  // My test device.
            .Build();
		// Load the banner with the request.
		bannerView.LoadAd(requestBanner);
	}

	public void ShowBanner()
	{
		bannerView.Show();
	}

	public void HideBanner()
	{
		bannerView.Hide();
	}
}

