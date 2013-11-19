using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
/*The bar goes right to left, if you want it in the other direction, just change curTime to 0 and increase the time while adjusting  */
public class TimerUpdate : MonoBehaviour {
	public int maxTime = 100;
	public float curTime = 100;
	public float timeBarLength;
	float GUIWidth; 
	public Texture2D background;  
	public Texture2D foreground;
	public Texture2D lion;
	Rect pivot;
	public float pivotTime = 0f;
	Rect backgroundRect;
	Rect timeRec;
	public float step = 0f;
	public float offsetY = 0f;
	public int perfectTimeWindow = 0;
	public float padding;       //Under Development for the GUI width to be independent of the timebar. 
	public static List<TimerUpdate> timers = new List<TimerUpdate>();
	public string Name = string.Empty;
    public TimerUpdate()
    {

    }
    void Start () 
    {
		GUIWidth = maxTime;
		timers.Add(this);
    }

    public enum ResponseType
    {
        perfect,
        missed
    }
 
    void Update () 
    {
        AddjustCurrentTime(step);        //This can allow you to have differernt time steps for the change. 
        if(Input.GetKeyDown(KeyCode.F))
            Debug.Log(Check().ToString());
    }

    private ResponseType Check()
    {
        Rect r = pivot;
        r.x -= perfectTimeWindow / 2;
        r.width = perfectTimeWindow;
        if (r.Contains(new Vector2(timeRec.x + timeRec.width, pivot.y + pivot.height / 2)))      //If the current time is in perfect time range
        {
            return ResponseType.perfect;
        }
        else
            return ResponseType.missed;
    }
    
    void OnGUI()
    {
        timeRec = new Rect(Screen.width - GUIWidth - 25, 10 + offsetY, timeBarLength , 20);
        pivot = new Rect(Screen.width - GUIWidth + pivotTime - 25, 10 + offsetY, 5, 20);
        backgroundRect = new Rect(Screen.width - GUIWidth - 25, 10 + offsetY, GUIWidth, 20);
		var boxRect = backgroundRect;
		boxRect.x -= padding/2;
		boxRect.width += padding;
		boxRect.y -= padding / 2;
		boxRect.height += padding;
        GUI.DrawTexture(backgroundRect, background);
        GUI.DrawTexture(timeRec, foreground, ScaleMode.StretchToFill, false);
        GUI.DrawTexture(pivot, lion);
		GUI.Box (boxRect, "");
    }

    public void AddjustCurrentTime(float adj)
    {
	if (movement.IsRelativistic) {
		adj *= 1 - (float)(gameState.PlayerVelocity / gameState.totalC);
		adj /= 5.0f;
	}
        curTime += adj;
        if (curTime < 0)
		{
			curTime = 0;
		}
        if (curTime > maxTime)
            curTime = maxTime;
        if (maxTime < 1)
            maxTime = 1;
        timeBarLength = GUIWidth * (curTime / ((float)maxTime)); 
    }
 
}
