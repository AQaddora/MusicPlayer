using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoudButton : MonoBehaviour {

    private AudioSource sound;

    void Awake()
    {
        sound = GameObject.Find("Sound").GetComponent<AudioSource>();
    }

    void Update ()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = (sound.volume <= 0.3f);
        GetComponent<CanvasGroup>().alpha = sound.volume >= 0.3f ? 0 : 1;
    }

    public void Mute()
    {
        sound.volume = 0;
    }
}
