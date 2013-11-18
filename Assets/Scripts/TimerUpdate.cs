using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
/*The bar goes right to left, if you want it in the other direction, just change curTime to 0 and increase the time while adjusting  */
public class TimerUpdate : MonoBehaviour {
public int maxTime = 100;
public float curTime = 100;
public float timeBarLength;
public float GUIWidth; 
public Texture2D background;  
public Texture2D foreground;
public Texture2D lion;
public Rect pivot;
Rect backgroundRect;
Rect timeRec;
void Start () 
{
}

public enum ResponseType
{
    perfect,
    missed
}
 
void Update () 
{
    AddjustCurrentTime(-0.1f);        //This can allow you to have differernt time steps for the change. 
    if(Input.GetKeyDown(KeyCode.F))
        Debug.Log(Check().ToString());
}

private ResponseType Check()
{
    if (pivot.Contains(new Vector2(timeRec.x + timeRec.width, pivot.y + pivot.height / 2)))      //If the current time is in perfect time range
    {
        return ResponseType.perfect;
    }
    else
        return ResponseType.missed;
}
    
void OnGUI()
{
    backgroundRect = new Rect(30, 10, GUIWidth, 20);
    GUI.DrawTexture(backgroundRect, background);
    timeRec = new Rect(30, 10, timeBarLength, 20);
    GUI.DrawTexture(timeRec, foreground, ScaleMode.StretchToFill, true);
	GUI.DrawTexture(pivot, lion);
}

public void AddjustCurrentTime(float adj)
{
 
curTime += adj;

if (curTime < 0)
    curTime = 0;

if (curTime > maxTime)
    curTime = maxTime;

if (maxTime < 1)
    maxTime = 1;

timeBarLength = GUIWidth * (curTime / (float)maxTime);
 
}
 
}