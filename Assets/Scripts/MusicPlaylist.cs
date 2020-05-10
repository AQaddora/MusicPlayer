using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public class MusicPlaylist : MonoBehaviour {

	public static MusicPlaylist Instance;
	DirectoryInfo MusicFolder;
	WWW myClip;
	string myPath;
	AudioSource audioSource;
	private GameObject[] listings;
	public AudioClip[] clips;
	private int index = 0;
	public GameObject listingPrefab;
	public AlphaUI loadingPanel;
	public Transform listingsParent;
	public Image puasePlayToggle;
	public Sprite pause, play;
	private float spentTime = 0.0f;
	private bool unpaused = false;
	public CashWWW cashWWWInstance;
	public Text loadingText;

	private Vector3 wantedPos;
	private bool isLerping;
	
	void Awake () 
	{
		Instance = this;
		audioSource = GetComponent<AudioSource>();
		Application.runInBackground = true;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
#if UNITY_ANDROID
		myPath = Application.persistentDataPath + "/Music";
#endif
#if UNITY_STANDALONE
		myPath = "Builds/Music";
#endif
#if Unity_Editor
		myPath = "Builds/Music";
#endif
		
		

	}
	private void Start()
	{
		try
		{
			MusicFolder = new DirectoryInfo(myPath);
			StartCoroutine(UpdateMusicLibrary());
		}
		catch (Exception e)
		{
			Debug.LogError(e.Message);
		}
	}

	void Update()
	{
		if (isLerping)
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, wantedPos, Time.deltaTime * 5f);
			isLerping = !(Mathf.Abs(transform.localPosition.x - wantedPos.x) < 0.2f);
		}
		if(unpaused)
		{
			spentTime += Time.deltaTime;
		}
	}
	private IEnumerator UpdateMusicLibrary()
	{
		if (!MusicFolder.Exists)
		{
			listings = new GameObject[clips.Length];
			for (int i = 0; i < clips.Length; i++)
			{
				GameObject obj = Instantiate(listingPrefab);
				obj.transform.parent = listingsParent;
				obj.GetComponentInChildren<Text>().text = "  " + clips[i].name;
				obj.name = clips[i].name;;
				obj.GetComponent<RectTransform>().localScale = Vector3.one;
				listings[i] = obj;
			}
			yield break;
		}
		loadingPanel.Show();
		
		int length = MusicFolder.GetFiles().Length;
		if (length == 0)
		{
			loadingPanel.Hide();
			yield break;
		}
		loadingText.text = "Scanning files \"" + MusicFolder.Name + "\"...";
		clips = new AudioClip[length];
		listings = new GameObject[length];
		for(int i = 0; i < MusicFolder.GetFiles().Length; i++)
		{
			while (cashWWWInstance == null)
			{
				Debug.Log("CashWWW = null"); 
				yield return null;
			}

			if (!(MusicFolder.GetFiles()[i].FullName.Contains("mp3") || MusicFolder.GetFiles()[i].FullName.Contains("ogg")))
				continue;
			loadingText.text = "Loading \"" + MusicFolder.GetFiles()[i].Name + "\"...";

#if UNITY_STANDALONE
			myClip = new WWW("file:///" + MusicFolder.GetFiles()[i].FullName);
#endif
#if Unity_Android
			myClip = cashWWWInstance.GetCachedWWW("file:///" + MusicFolder.GetFiles()[i].FullName);
#endif
			GameObject obj = Instantiate(listingPrefab);
			obj.transform.parent = listingsParent;
			obj.GetComponentInChildren<Text>().text = "  " + MusicFolder.GetFiles()[i].Name.Substring(0, MusicFolder.GetFiles()[i].Name.Length-4);
			obj.name =  MusicFolder.GetFiles()[i].Name;
			obj.GetComponent<RectTransform>().localScale = Vector3.one;
			listings[i] = obj;
			clips[i] = myClip.GetAudioClip(false, false);
			while(clips[i].loadState != AudioDataLoadState.Loaded)
			{
				yield return null;
			}
		}
		audioSource.clip = clips[index];
		audioSource.Play();
		if(puasePlayToggle.sprite != pause)
				puasePlayToggle.sprite = pause;

		unpaused = true;
		loadingPanel.Hide();
	}
	public void VolumeControl(float vol)
	{
		audioSource.volume = vol;
	}

	public void Quit()
	{
		Application.Quit();
	}
	public void SelectSongByName(string s)
	{
		for(int i = 0; i < clips.Length; i++)
		{
			if(listings[i].name.Equals(s))
			{
				index = i;
				audioSource.clip = clips[i];
				audioSource.Play();
				if(puasePlayToggle.sprite != pause)
					puasePlayToggle.sprite = pause;
				spentTime = 0;
				CancelInvoke();				
				Invoke("ChangeToNextClip", clips[index].length);
				return; 
			}
		}
	}
	public void TogglePause()
	{
		Debug.Log("Pause/Unpause");
		if(audioSource.isPlaying){
			audioSource.Pause();
			unpaused = false;
			puasePlayToggle.sprite = play;	
			CancelInvoke();		
		}
		else {
			unpaused = true;
			Invoke("ChangeToNextClip", audioSource.clip.length - spentTime);
			audioSource.UnPause();
			puasePlayToggle.sprite = pause;			
		}
	}
	public void ChangeToPreviousClip()
	{
		index = (index == 0) ? clips.Length-1 : index-1;
		audioSource.clip = clips[index];
		audioSource.Play();
		if(puasePlayToggle.sprite != pause)
			puasePlayToggle.sprite = pause;
		spentTime = 0;
		CancelInvoke();		
		Invoke("ChangeToNextClip", clips[index].length); 
	}
	public void ChangeToNextClip()
	{
		index = (index+1) % clips.Length;
		audioSource.clip = clips[index];
		audioSource.Play();
		if(puasePlayToggle.sprite != pause)
			puasePlayToggle.sprite = pause;
		spentTime = 0;
		CancelInvoke();		
		Invoke("ChangeToNextClip", clips[index].length); 
	}

	public void ToggleListButton()
	{
		float xValue = transform.localPosition.x < 1 ? 10 : 0;
		wantedPos = new Vector3(xValue, transform.localPosition.y, transform.localPosition.z);
		isLerping = true;
	}
}