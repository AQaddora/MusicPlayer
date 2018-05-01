using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelect : MonoBehaviour {

	public void SelectME()
	{
		MusicPlaylist.Instance.SelectSongByName(transform.parent.name);
		Debug.Log(transform.parent.name);
	}

}
