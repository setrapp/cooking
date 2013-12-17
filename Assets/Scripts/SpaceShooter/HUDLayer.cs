using System;
using UnityEngine;

public class HUDLayer:  MonoBehaviour
{
	public GUIStyle guiStyle;
	public string screenText;

	private bool showScreenText;
	private float screenTextOutTime;
	private float screenTextStayTime;

	public static HUDLayer Instance;

	public HUDLayer ()
	{

	}

	public void Start() {
		Instance = this;
	}
	
	public void Enter() {

	}
		
	public void OnGUI() {
		if(showScreenText){
			GUI.Label(new Rect(0, 0, 600, 300), screenText, guiStyle);
		}
	}
	
 	void OnShowScreenTextFadeIn() {
		// just wait
		iTween.ValueTo(this.gameObject, iTween.Hash("from", 1.0, "to", 1.0, "time", screenTextStayTime, "oncomplete", "OnShowScreenTextStay", "onupdate", "FakeUpdate"));
	}

	void OnShowScreenTextStay() {
		iTween.ValueTo(this.gameObject, iTween.Hash("name", "fadeout", "from", 1.0, "to", 0.0, "time", screenTextOutTime, "oncomplete", "OnShowScreenTextFadeOut", "onupdate", "OnShowScreenTextUpdate"));
	}
	
	void OnShowScreenTextFadeOut() {
		showScreenText = false;
	}

	void FakeUpdate(float newVal) {

	}
		
	void OnShowScreenTextUpdate(float newVal) {
		guiStyle.normal.textColor = new Color(1, 1, 1, newVal);
	}
	
	public void ShowScreenText(string text, float timeIn, float timeStay, float timeOut, TextAnchor anchor) {
		showScreenText = true;
		screenTextOutTime = timeOut;
		screenTextStayTime = timeStay;
		screenText = text;
		guiStyle.alignment = anchor;
		
		iTween.ValueTo(this.gameObject, iTween.Hash("name", "fadein", "from", 0.0, "to", 1.0, "time", timeIn, "oncomplete", "OnShowScreenTextFadeIn", "onupdate", "OnShowScreenTextUpdate"));
	}
	
	public void ShowScreenText(string text)
	{
		showScreenText = true;
		screenText = text;
	}
	
	public void HideScreenText()
	{		
		showScreenText = false;
		screenText = "";
	}


}
