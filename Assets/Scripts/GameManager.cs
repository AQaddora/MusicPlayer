/**
 * this is the Game Manager script
 * you now just need to fill in the things in the inspector and start designing your main scene ...
 * you also just need to set the gameStatus variable into "Started", "Lost", "Waiting" in other classes.
 * Last thing you need is to change the score value as you want in other classes.
 * 
 * Dont forget to setup google games from window>google play games> Android
 * after setting the google games dont forget to remove the comments from the two google games methods below
 * 
 * this script may show compiler errors if you've not set platform to Android, so take care
 *
 * every thing else in the game is ready to publish
 **/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
#if UNITY_ANDROID
using GoogleMobileAds;
using GoogleMobileAds.Api;

using GooglePlayGames;
#endif
*/

public class GameManager : MonoBehaviour {
	
	public static string gameStatus;
	public static int score;
	private static bool isGoogleGamesActivated;
	public static bool isRetry;

	private int bestScore;
	private bool hidePanels = false;

	[Header("AlphaUIs")]
	public AlphaUI startPanel;
	public AlphaUI hudPanel;
	public AlphaUI endPanel;
	public AlphaUI notificationPanel;
	public AlphaUI fadePanel;
	public GameObject socialPanel, googleGamesPanel, settingsPanel;
	public Animator socialPanelAtEnd, googleGamesPanelAtEnd, settingsPanelAtEnd;
	[Header("Sound controllers")]
	public static AudioSource sound;
	public AudioClip main_loopClip, loseSound, scoreClip;
	private static AudioClip thisScoreClip;

	/*
	#if UNITY_ANDROID
	[Header("Ads things")]
	public string withoutVideoAdsID;
	public string VideoAdsID;
	public int nonVideoAdSohwDelay;
	public int videoAdSohwDelay;
	private int deathCountForeNonVedioAds = 0;
	private int deathCountForeVedioAds = 0;
	private InterstitialAd nonVideo;
	private InterstitialAd video;
	#endif
	*/

	[Header("Score things")]
	public Text scoreShow;
	public Text scoreShowInEnd;
	public Text bestScoreShow;


	[Header("Url things")]
	public string packageName;
	public string facebookUrl;
	public string windowsStoreLink;

	void Start () 
	{
		thisScoreClip = scoreClip;
		ShowStart ();
		bestScore = PlayerPrefs.GetInt ("bestScore", 0);

		sound = GameObject.FindGameObjectsWithTag ("Sound")[0].GetComponent<AudioSource>();


		// show start screen at the beggining

		/*
		#if UNITY_ANDROID
		// Activating google play games and  signing in
		if(!isGoogleGamesActivated)
		{
			Debug.Log (isGoogleGamesActivated);
			PlayGamesPlatform.Activate();

			if (!Social.localUser.authenticated) {
				Social.localUser.Authenticate (success => {});
			}
			isGoogleGamesActivated = true;
		}
		#endif
        */

		score = 0;

		fadePanel.Hide ();

		/*
		#if UNITY_ANDROID
		// start loading Ads
		RequestVedio ();
		RequestNonVideo ();
		#endif
		*/
	}

	void LateUpdate()
	{
		if (gameStatus == "Lost")
		{
			gameStatus = "Waiting";
			if (sound.clip != main_loopClip) 
			{
				sound.clip = main_loopClip;
				sound.Play ();
			}
			hudPanel.GetComponent<Animator> ().SetBool ("Hud", false);
			ShowEnd ();
			Invoke ("EndPanelShow", 0.5f);
			sound.PlayOneShot (loseSound, 1);

			/*
			#if UNITY_ANDROID
			deathCountForeNonVedioAds++;
			deathCountForeVedioAds++;
			#endif
			*/
			}
	}

	void Update () 
	{
		if (!(gameStatus == "Lost" || gameStatus == "Started" || gameStatus == "Waiting"))
		{
			Debug.LogError ("please set gameStatus with usefull value");
		}
			
		scoreShow.text = "" + score;
		scoreShowInEnd.text = "" + score;
		if(score >= PlayerPrefs.GetInt("bestScore"))
		{
			bestScore = PlayerPrefs.GetInt ("bestScore", 0);
			PlayerPrefs.SetInt ("bestScore", score);

			/*
			#if UNITY_ANDROID
			SetBestScoreOnLeaderBoard ();
			#endif
            */

			scoreShow.color = Color.red;
			bestScoreShow.color = Color.red;
			bestScoreShow.text = ("New Best: " + PlayerPrefs.GetInt("bestScore"));
		}
		else
		{
			bestScoreShow.color = Color.black;
			scoreShow.color = Color.black;
			bestScoreShow.text = "best: " + PlayerPrefs.GetInt("bestScore");
		}

		/*
		#if UNITY_ANDROID
		// ads displaying
		// nonVideo ads displaying
		if (deathCountForeNonVedioAds == nonVideoAdSohwDelay)
		{
			deathCountForeNonVedioAds = 0;
			if (nonVideo.IsLoaded())
			{
				nonVideo.Show();
				Invoke ("RequestInterstitial", 2);
			}
		}
		//video ads display
		if(deathCountForeVedioAds == videoAdSohwDelay)
		{
			deathCountForeVedioAds = 0;
			if (video.IsLoaded())
			{
				video.Show();
				Invoke ("RequestVedio", 2);
			}
		}
		#endif
        */

		// hide notification panel after showing it
		if (notificationPanel.GetComponent<CanvasGroup>().alpha >= 1)
		{
			Invoke ("HideNotification", 5);
		}

		// set the double pressing back button to leave the game
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			notificationPanel.GetComponentInChildren<Text> ().text = "press back again to leave !!" ;
			notificationPanel.Show ();
			notificationPanel.GetComponent<Animator> ().SetBool ("notification", true);
		}
		if (Input.GetKeyUp(KeyCode.Escape) && notificationPanel.GetComponent<CanvasGroup>().alpha >= 0.2f 
			&& notificationPanel.GetComponentInChildren<Text> ().text == "press back again to leave !!")
		{
			Debug.Log ("Quit Game !!");
			Application.Quit ();
		}
	}
	void HideNotification()
	{
		notificationPanel.GetComponent<Animator> ().SetBool ("notification", false);
		notificationPanel.Hide ();
	}

	//soundController
	public void ChangeClipForDuration(AudioClip clip)
	{
		sound.clip = clip;
		sound.Play ();
		float duration = clip.length;
		Invoke ("LoadMainClip", duration);
	}

	public static void ScorePlus()
	{
		score++;
		sound.PlayOneShot (thisScoreClip, 1);
	}
	//load main loop Music
	public void LoadMainClip()
	{
		sound.clip = main_loopClip;
		sound.Play ();
	}

	// show end method called when he lose
	public void ShowEnd()
	{
		endPanel.Show ();
		startPanel.Hide ();
		hudPanel.Hide ();
		fadePanel.Hide ();
	}

	// show start screen method called at start
	public void ShowStart()
	{
		endPanel.Hide ();
		fadePanel.Hide ();

		Debug.Log (isRetry + "   " + gameStatus);

		// to determine if he pressed retry button to load game immediatly
		if (isRetry)
		{
			isRetry = false;
			gameStatus = "Started";
			startPanel.GetComponent<CanvasGroup> ().alpha = 0;
			startPanel.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			HudPanelShow ();
			hudPanel.Show ();
			gameStatus = "Started";
		} else 
		{
			gameStatus = "Waiting";
			startPanel.Show ();
			Invoke ("startAnimation", 0.4f);
			hudPanel.Hide ();
		}
	}

	// put it into Retry button OnClick()
	public void RetryButton()
	{
		isRetry = true;
		endPanel.GetComponent<Animator> ().SetBool ("End", false);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	// put it into Home button OnClick()
	public void HomeButton()
	{
		endPanel.Hide ();
		SceneManager.LoadScene(SceneManager.GetActiveScene ().buildIndex);
	}

	// put it into Start button OnClick()
	public void StartButton() 
	{
		Invoke ("startAnimClose", 0.1f);
		gameStatus = "Started";
		HideSmallPanels ();
		Invoke ("HideStartpanelAfterAnim", 0.5f);
		Invoke ("HudPanelShow", 1);
		hudPanel.Show ();
	}

	public void socialButton()
	{
		socialPanelAtEnd.SetBool ("socialButton", true);
		socialPanel.GetComponent<Animator> ().SetBool ("socialButton", true);
		Invoke ("HideSmallPanels", 5);
	}
	public void GoogleGamesButton()
	{
		googleGamesPanelAtEnd.SetBool ("googleGamesButton", true);
		googleGamesPanel.GetComponent<Animator> ().SetBool ("googleGamesButton", true);
		Invoke ("HideSmallPanels", 5);
	}
	public void SettingsButton()
	{
		settingsPanel.GetComponent<Animator> ().SetBool ("settingButton", true);
		settingsPanelAtEnd.SetBool ("settingButton", true);
		Invoke ("HideSmallPanels", 5);
	}

	// put it into Rate button OnClick()
	public void RateButton() 
	{
		#if UNITY_ANDROID
		Application.OpenURL ("market://details?id=" + packageName);
		#else
		Application.OpenURL (windowsStoreLink);
		#endif
	}

	// put it into facebook button OnClick()
	public void FacebookButton() 
	{
		Application.OpenURL (facebookUrl);
	}

	// put it into Mute button OnClick()
	public void MuteButton()
	{
		Debug.Log ("531616516516551615511");
		sound.volume = 0;
	}

	// put it into Loud button OnClick()
	public void LoudButton()
	{
		sound.volume = 1;
	}
	// put it into Leaderboard button OnClick()
//	#if UNITY_ANDROID
//	public void leaderBoardButton() 
//	{
//		/**
//		 * Dont forget to remove the comment after setting google games
//		 * be sure that this is google games leaderboard name "highscores", if not change it below
//		**/
//		PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_highscores);
//	}
//
//	private void RequestNonVideo()
//	{
//		// Initialize an InterstitialAd.
//		nonVideo = new InterstitialAd(withoutVideoAdsID);
//		// Create an empty ad request.
//		AdRequest request = new AdRequest.Builder()/*.AddTestDevice(testerID)*/.Build();
//		// Load the interstitial with the request.
//		nonVideo.LoadAd(request);
//	}
//
//	private void RequestVedio()
//	{
//		// Initialize an InterstitialAd.
//		video = new InterstitialAd(VideoAdsID);
//		// Create an empty ad request.
//		AdRequest request = new AdRequest.Builder()/*.AddTestDevice(testerID)*/.Build();
//		// Load the interstitial with the request.
//		video.LoadAd(request);
//	}
//
//	// posting the best score to the leader board
//	public void SetBestScoreOnLeaderBoard()
//	{
//		/**
//		 * Dont forget to remove the comment after setting google games
//		 * be sure that this is google games leaderboard name "highscores", if not change it below
//		**/
//		Social.ReportScore(bestScore, GPGSIds.leaderboard_highscores, success => {});
//	}
//	#endif

	void startAnimation()
	{
		startPanel.GetComponent<Animator> ().SetBool ("start", true);
	}
	void startAnimClose()
	{
		startPanel.GetComponent<Animator> ().SetBool ("start", false);
	}
	void HideStartpanelAfterAnim()
	{
		startPanel.Hide ();
	}

	public void HideSmallPanels()
	{
		socialPanel.GetComponent<Animator> ().SetBool ("socialButton", false);
		googleGamesPanel.GetComponent<Animator> ().SetBool ("googleGamesButton", false);
		settingsPanel.GetComponent<Animator> ().SetBool ("settingButton", false);
		settingsPanelAtEnd.SetBool ("settingButton", false);
		socialPanelAtEnd.SetBool ("socialButton", false);
		googleGamesPanelAtEnd.SetBool ("googleGamesButton", false);
	}
	public void HideSettingsPanel()
	{
		settingsPanel.GetComponent<Animator> ().SetBool ("settingButton", false);
		settingsPanelAtEnd.SetBool ("settingButton", false);
	}
	public void HideSocialPanel()
	{
		socialPanel.GetComponent<Animator> ().SetBool ("socialButton", false);
		socialPanelAtEnd.SetBool ("socialButton", false);
	}
	public void HideGoogleGamesPanel()
	{
		googleGamesPanel.GetComponent<Animator> ().SetBool ("googleGamesButton", false);
		googleGamesPanelAtEnd.SetBool ("googleGamesButton", false);
	}
	public void HudPanelShow()
	{
		hudPanel.GetComponent<Animator> ().SetBool ("Hud", true);
	}
	public void EndPanelShow()
	{
		endPanel.GetComponent<Animator> ().SetBool ("End", true);
	}
	public void TestLose()
	{
		gameStatus = "Lost";
	}
	public void TestScore()
	{
		ScorePlus ();
	}
}
