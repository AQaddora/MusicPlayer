using System;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

// @GamerBox 2018

public class AudioEqualizer : MonoBehaviour
{

	public float rmsValue, dbValue, visualMultiplier = 50, smoothSpeed = 10, 
		maxVisualScale = 25, keepPercentage = 0.5f, bgIntensity;

	public Material bgMatierial;
	public Color minColor, maxColor;
	public GameObject visualPrefav;

	private AudioSource audioSource;
	private float[] samples, spectrum;
	private float sampleRate;

	private Transform[] visualList1, visualList2;
	private float[] visualScale1, visualScale2;
	private int amountOfVisuals = 16;

	int i = 1;
	//Arduino Variables
     public static SerialPort sp = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
     public static string strIn; 

	private void Start ()
	{
		OpenConnection();
		audioSource = GetComponent<AudioSource> ();
		samples = new float[1024];
		spectrum = new float[1024];
		sampleRate = AudioSettings.outputSampleRate;

		SpawnLine ();
	}

	private void SpawnLine ()
	{
		visualList1 = new Transform[amountOfVisuals];
		visualScale1 = new float[amountOfVisuals];
		visualList2 = new Transform[amountOfVisuals];
		visualScale2 = new float[amountOfVisuals];

		for (int i = 0; i < amountOfVisuals; i++) {
			GameObject obj = Instantiate (visualPrefav);
			visualList1 [i] = obj.transform;
			visualList1 [i].position = Vector3.right * i;
		}

		for (int i = amountOfVisuals - 1; i >= 0; i--) {
			GameObject obj = Instantiate (visualPrefav);
			visualList2 [i] = obj.transform;
			visualList2 [i].position = Vector3.right * (i + amountOfVisuals);
		}

		for (int i = 0; i < amountOfVisuals / 2; i++) {
			Transform temp = visualList2 [i];
			visualList2 [i] = visualList2 [amountOfVisuals - i - 1];
			visualList2 [amountOfVisuals - i - 1] = temp;
		}
	}

	private void Update ()
	{
		AnalyzeSound ();
		UpdateVisual ();
		UpdateBackground ();
	}

	private void UpdateBackground ()
	{
		bgIntensity -= Time.deltaTime * smoothSpeed;
		if (bgIntensity < dbValue / 40)
			bgIntensity = dbValue / 40;

		bgMatierial.color = Color.Lerp (maxColor, minColor, -bgIntensity * 0.7f);
	}

	private void UpdateVisual ()
	{
		int visualIndex = 0, spectrumIndex = 0;
		int avarageSize = (int)(1024 * keepPercentage / amountOfVisuals);

		while (visualIndex < amountOfVisuals) {
			int j = 0;
			float sum = 0;
			while (j < avarageSize) {
				sum += spectrum [spectrumIndex];
				spectrumIndex++;
				j++;
			}

			float scaleY = sum / avarageSize * visualMultiplier;
			visualScale1 [visualIndex] -= Time.deltaTime * smoothSpeed;
			visualScale2 [visualIndex] -= Time.deltaTime * smoothSpeed;

			if (visualScale1 [visualIndex] < scaleY)
				visualScale1 [visualIndex] = scaleY;

			if (visualScale1 [visualIndex] > maxVisualScale)
				visualScale1 [visualIndex] = maxVisualScale;

			visualList1 [visualIndex].localScale = Vector3.one * 0.5f + Vector3.up * visualScale1 [visualIndex];
			Debug.Log((int)visualScale1[visualIndex]);
			sp.Write(((int)visualScale1[visualIndex]).ToString());

			if (visualScale2 [visualIndex] < scaleY)
				visualScale2 [visualIndex] = scaleY;

			if (visualScale2 [visualIndex] > maxVisualScale)
				visualScale2 [visualIndex] = maxVisualScale;

			visualList2 [visualIndex].localScale = Vector3.one * 0.5f + Vector3.up * visualScale2 [visualIndex];

			
			visualIndex++;
		}
		sp.Write("-");
	}

	private void AnalyzeSound ()
	{
		audioSource.GetOutputData (samples, 0);

		//RMS
		float sum = 0;
		for (int i = 0; i < 1024; i++) {
			sum = samples [i] * samples [i];
		}
		rmsValue = Mathf.Sqrt (sum / 1024);

		//DB
		dbValue = 20 * Mathf.Log10 (rmsValue / 0.1f);

		//Spetrum using Foriuer transform. 
		audioSource.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris);
	}

	 //Function connecting to Arduino
     public void OpenConnection() 
     {
         if (sp != null) 
         {
             if (sp.IsOpen) 
             {
                 sp.Close();
                 Debug.Log("Closing port, because it was already open!");
             }
             else 
             {
                 sp.Open();  // opens the connection
                 sp.ReadTimeout = 60;  // sets the timeout value before reporting error
                 Debug.Log("Port Opened!");
             }
         }
         else 
         {
             if (sp.IsOpen)
             {
                 print("Port is already open");
             }
             else 
             {
                 print("Port == null");
             }
         }
     }
 
     void OnApplicationQuit() 
     {
         sp.Close();
     }
}
