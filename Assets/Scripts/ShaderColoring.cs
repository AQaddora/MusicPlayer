using UnityEngine;
using System.Collections;

public class ShaderColoring : MonoBehaviour {

    public Color[] newColorsDown, newColorsUp;
    public float duration;

    private Material bgMat;
    private Color color1, color2;
    private int index = 0;
    private float timer;

    void Awake()
    {
        bgMat = GetComponent<Renderer>().material;
        bgMat.SetColor("_SColor", newColorsDown[0]);
        bgMat.SetColor("_FColor", newColorsUp[0]);
        index = Random.Range(0, newColorsDown.Length);
    }

    void LateUpdate()
    {
        if (newColorsDown.Length == 0 || newColorsUp.Length == 0) return;

        timer += Time.deltaTime / duration;
        if (timer >= 1.05f)
        {
            timer = 0;
            index++;
            if (index >= newColorsDown.Length) index = 0;
        }
        color1 = Color.Lerp(newColorsUp[(index == 0) ? newColorsUp.Length - 1 : index - 1], newColorsUp[index], timer);
        color2 = Color.Lerp(newColorsDown[(index == 0) ? newColorsDown.Length - 1 : index - 1], newColorsDown[index], timer);
        bgMat.SetColor("_SColor", color2);
        bgMat.SetColor("_FColor", color1);
    }
}
