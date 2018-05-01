using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using TMPro;
public class MusicPlaylist : MonoBehaviour {

	public static MusicPlaylist Instance;
	DirectoryInfo MusicFolder;
	WWW myClip;
	string myPath;
	AudioSource audioSource;
	private GameObject[] listings;
	private AudioClip[] clips;
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

	void Awake () 
	{
		Instance = this;
		audioSource = GetComponent<AudioSource>();
#if UNITY_ANDROID
		myPath = "/storage/sdcard0/Music";
#endif
#if UNITY_STANDALONE
		myPath = "Builds/Music";
#endif
#if Unity_Editor
		myPath = "Builds/Music";
#endif

		MusicFolder = new DirectoryInfo(myPath);

	}
	private void Start()
	{
		StartCoroutine(UpdateMusicLibrary());
	}

	void Update()
	{
		if(unpaused)
		{
			spentTime += Time.deltaTime;
		}
	}
	private IEnumerator UpdateMusicLibrary()
	{
		loadingPanel.Show();
		int length = MusicFolder.GetFiles().Length;
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
			obj.GetComponentInChildren<TextMeshProUGUI>().SetText("  " + MusicFolder.GetFiles()[i].Name);
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
}