using System.Collections;
using UnityEditor;
using UnityEngine;

public class ModernUIEditor : EditorWindow {

	private static ModernUIEditor instance = null;

	[MenuItem("Modern UI Pack/Buttons/Basic")]
	static void CreateBasicButton()
	{
		Instantiate(Resources.Load<GameObject>("Buttons/Basic")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Buttons/Basic Outline")]
	static void CreateBasicOutline()
	{
		Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Buttons/Basic With Image")]
	static void CreateBasicWithImage()
	{
		Instantiate(Resources.Load<GameObject>("Buttons/Basic With Image")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Buttons/Basic Outline With Image")]
	static void CreateBasicOutlineWithImage()
	{
		Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline With Image")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Buttons/Box Outline With Image")]
	static void CreateBoxOutlineWithImage()
	{
		Instantiate(Resources.Load<GameObject>("Buttons/Box Outline With Image")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Buttons/Box With Image")]
	static void CreateBoxWithImage()
	{
		Instantiate(Resources.Load<GameObject>("Buttons/Box With Image")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Buttons/Circle Outline With Image")]
	static void CreateCircleOutlineWithImage()
	{
		Instantiate(Resources.Load<GameObject>("Buttons/Circle Outline With Image")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Buttons/Circle With Image")]
	static void CreateCircleWithImage()
	{
		Instantiate(Resources.Load<GameObject>("Buttons/Circle With Image")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Buttons/Rounded")]
	static void RoundedButton()
	{
		Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Buttons/Rounded Outline")]
	static void RoundedOutline()
	{
		Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Notifications/Fading Notification")]
	static void FadingNotification()
	{
		Instantiate(Resources.Load<GameObject>("Notifications/Fading Notification")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Notifications/Popup Notification")]
	static void PopupNotification()
	{
		Instantiate(Resources.Load<GameObject>("Notifications/Popup Notification")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Notifications/Slippery Notification")]
	static void SlipperyNotification()
	{
		Instantiate(Resources.Load<GameObject>("Notifications/Slippery Notification")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Notifications/Slipping Notification")]
	static void SlippingNotification()
	{
		Instantiate(Resources.Load<GameObject>("Notifications/Slipping Notification")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars/Radial PB Bold")]
	static void RadialPBBold()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Bold")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars/Radial PB Filled H")]
	static void RadialPBFilledH()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Filled H")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars/Radial PB Filled V")]
	static void RadialPBFilledV()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Filled V")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars/Radial PB Light")]
	static void RadialPBLight()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Light")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars/Radial PB Regular")]
	static void RadialPBRegular()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Regular")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars/Radial PB Thin")]
	static void RadialPBThin()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Thin")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars/Standart PB")]
	static void StandartPB()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars/Standart PB")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars (Loop)/Circle Arround")]
	static void CircleArround()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Circle Arround")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars (Loop)/Circle Fix")]
	static void CircleFix()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Circle Fix")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars (Loop)/Circle Glass")]
	static void CircleGlass()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Circle Glass")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars (Loop)/Circle Pie")]
	static void CirclePie()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Circle Pie")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars (Loop)/Circle Run")]
	static void CircleRun()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Circle Run")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars (Loop)/Circle Trapez")]
	static void CircleTrapez()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Circle Trapez")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars (Loop)/Standart Fastly")]
	static void StandartFastly()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Standart Fastly")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars (Loop)/Standart Finish")]
	static void StandartFinish()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Standart Finish")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Progress Bars (Loop)/Standart Run")]
	static void StandartRun()
	{
		Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Standart Run")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Sliders/Gradient")]
	static void GradientSlider()
	{
		Instantiate(Resources.Load<GameObject>("Sliders/Gradient")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Sliders/Outline")]
	static void OutlineSlider()
	{
		Instantiate(Resources.Load<GameObject>("Sliders/Outline")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Sliders/Standart")]
	static void StandartSlider()
	{
		Instantiate(Resources.Load<GameObject>("Sliders/Standart")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Switches/Outline")]
	static void OutlineSwitch()
	{
		Instantiate(Resources.Load<GameObject>("Switches/Outline")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Switches/Standart")]
	static void StandartSwitch()
	{
		Instantiate(Resources.Load<GameObject>("Switches/Standart")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Toggles/Standart Toggle (Bold)")]
	static void StandartToggleBold()
	{
		Instantiate(Resources.Load<GameObject>("Toggles/Standart Toggle (Bold)")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Toggles/Standart Toggle (Light)")]
	static void StandartToggleLight()
	{
		Instantiate(Resources.Load<GameObject>("Toggles/Standart Toggle (Light)")).GetComponent<ModernUIEditor>();
	}

	[MenuItem("Modern UI Pack/Toggles/Standart Toggle (Regular)")]
	static void StandartToggleRegular()
	{
		Instantiate(Resources.Load<GameObject>("Toggles/Standart Toggle (Regular)")).GetComponent<ModernUIEditor>();
	}

	public static void OnCustomWindow()
	{
		EditorWindow.GetWindow(typeof(ModernUIEditor));
	}
}
