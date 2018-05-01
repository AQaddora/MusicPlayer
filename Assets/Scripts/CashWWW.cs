using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;

[System.Serializable]
public class CashWWW : MonoBehaviour
{
	public static CashWWW Instance;

	private void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			MusicPlaylist.Instance.cashWWWInstance = this;
			Debug.Log("Start CashWWW");
			DontDestroyOnLoad(this);
		}
		else
			Destroy(this);
	}

	public WWW GetCachedWWW(string url)
	{
		Debug.Log("GetCashedWWW First");
		string filePath = Application.persistentDataPath;
		filePath += "/" + GetInt64HashCode(url);
		string loadFilepath = filePath;
		bool web = false;
		WWW www;
		bool useCached = false;
		useCached = System.IO.File.Exists(filePath);
		if (useCached)
		{
			//check how old
			System.DateTime written = File.GetLastWriteTimeUtc(filePath);
			System.DateTime now = System.DateTime.UtcNow;
			double totalHours = now.Subtract(written).TotalHours;
			if (totalHours > 300)
				useCached = false;
		}
		if (System.IO.File.Exists(filePath))
		{
			string pathforwww = "file://" + loadFilepath;
			www = new WWW(pathforwww);
		}
		else
		{
			web = true;
			www = new WWW(url);
		}
		Debug.Log("GetCashedWWW LAST");

		CashWWW.Instance.StartCoroutine(DoLoad(www, filePath, web));
		return www;
	}

	private static string GetInt64HashCode(string url)
	{
//#if UNITY_ANDROID
		Int64 hashCode = 0;
		if (!string.IsNullOrEmpty(url))
		{
			//Unicode Encode Covering all characterset
			byte[] byteContents = Encoding.Unicode.GetBytes(url);
			System.Security.Cryptography.SHA256 hash =
			new System.Security.Cryptography.SHA256CryptoServiceProvider();
			byte[] hashText = hash.ComputeHash(byteContents);
			Int64 hashCodeStart = BitConverter.ToInt64(hashText, 0);
			Int64 hashCodeMedium = BitConverter.ToInt64(hashText, 8);
			Int64 hashCodeEnd = BitConverter.ToInt64(hashText, 24);
			hashCode = hashCodeStart ^ hashCodeMedium ^ hashCodeEnd;
		}
		return (hashCode + "");
//#endif
		//return UnityEngine.Random.Range(0, 999999999) + "";
	}

	static IEnumerator DoLoad(WWW www, string filePath, bool web)
	{
		yield return www;

		Debug.Log("Do Load");

		if (www.error == null)
		{
			if (web)
			{
				//System.IO.Directory.GetFiles
				//Debug.Log("SAVING DOWNLOAD  " + www.url + " to " + filePath);
				// string fullPath = filePath;
				File.WriteAllBytes(filePath, www.bytes);
				//Debug.Log("SAVING DONE  " + www.url + " to " + filePath);
				//Debug.Log("FILE ATTRIBUTES  " + File.GetAttributes(filePath));
				//if (File.Exists(fullPath))
				// {
				//    Debug.Log("File.Exists " + fullPath);
				// }
			}
		}
		else
		{
			if (!web)
			{
				File.Delete(filePath);
			}
			Debug.LogError("WWW ERROR " + www.error);
			MusicPlaylist.Instance.loadingPanel.Hide();
		}
	}

}