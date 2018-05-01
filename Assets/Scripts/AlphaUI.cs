using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]

public class AlphaUI : MonoBehaviour {
	
    public float duration = 0.5f;
    public bool autoWork = false;

	private bool work = false;
	private CanvasGroup cg;
	private int direction = -1;

    void Start () {
		cg = GetComponent<CanvasGroup> ();
        if (autoWork) Invoke("Hide", 1.3f);
    }

	void Update ()
    {
		if (work) {
			cg.alpha += direction*Time.deltaTime/duration;
			cg.blocksRaycasts = cg.alpha>=0.5f;
			work = !(cg.alpha==1 || cg.alpha==0);
		}
	}
	public void Show()
    {
		direction = 1;
		work = true;
	}
	public void Hide()
    {
		direction = -1;
		work = true;
	}
    public void Toggle()
    {
        if(cg.alpha==1)
        {
            Hide();
        } else
        {
            Show();
        }
    }
}
