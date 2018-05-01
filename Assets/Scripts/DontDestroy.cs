using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

    private string Tag;
	public bool reabeating = false;

    void Start()
    {
        Tag = gameObject.tag;

        DontDestroyOnLoad(gameObject);

		if(reabeating)
			 Destroy(GameObject.FindGameObjectsWithTag(Tag)[1]);
    }
}
