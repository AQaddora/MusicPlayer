using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioFiltering : MonoBehaviour
{
	public AudioMixerGroup[] filters;

	private Text[] filtersTexts;

	private void Start()
	{
		filtersTexts = new Text[5];
		filtersTexts[0] = GameObject.Find("Filter1").GetComponent<Text>();
		filtersTexts[1] = GameObject.Find("Filter2").GetComponent<Text>();
		filtersTexts[2] = GameObject.Find("Filter3").GetComponent<Text>();
		filtersTexts[3] = GameObject.Find("Filter4").GetComponent<Text>();
		filtersTexts[4] = GameObject.Find("NoFilter").GetComponent<Text>();
		filtersTexts[4].color = Color.green;
	}

	public void FilterButton(int filterIndex)
	{
		switch (filterIndex)
		{
			case 1:
				AudioEqualizer.audioSource.outputAudioMixerGroup = filters[0];
				foreach (Text filterText in filtersTexts)
				{
					filterText.color = Color.white;
				}
				filtersTexts[0].color = Color.green;
				break;
			case 2:
				AudioEqualizer.audioSource.outputAudioMixerGroup = filters[1];
				foreach (Text filterText in filtersTexts)
				{
					filterText.color = Color.white;
				}
				filtersTexts[1].color = Color.green;
				break;
			case 3:
				AudioEqualizer.audioSource.outputAudioMixerGroup = filters[2];
				foreach (Text filterText in filtersTexts)
				{
					filterText.color = Color.white;
				}
				filtersTexts[2].color = Color.green;
				break;
			case 4:
				AudioEqualizer.audioSource.outputAudioMixerGroup = filters[3];
				foreach (Text filterText in filtersTexts)
				{
					filterText.color = Color.white;
				}
				filtersTexts[3].color = Color.green;
				break;
			case 0:
				AudioEqualizer.audioSource.outputAudioMixerGroup = null;
				foreach (Text filterText in filtersTexts)
				{
					filterText.color = Color.white;
				}
				filtersTexts[4].color = Color.green;
				break;
		}
	}
}