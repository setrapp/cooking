using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
/*The bar goes right to left, if you want it in the other direction, just change curTime to 0 and increase the time while adjusting  */
public class TimerUpdate : MonoBehaviour {
	public string name = string.Empty;
	public float maxTime = 100;
	public float startTime = 0;
	private float curTime = 0;
	public float CurTime {
		get { return curTime; }
	}
	public float timeBarLength;
	private float GUIWidth; 
	private float GUIHeight;
	public Texture2D background;  
	public Texture2D foreground;
	public Texture2D lion;
	Rect pivot;
	public float pivotTime = 0f;
	Rect backgroundRect;
	Rect timeRec;
	Rect textRec;
	public float step = 0f;
	public float backstep = 0f;
	public float offsetX = -26f;
	public float offsetY = 0f;
	public int perfectTimeWindow = 0;
	public float padding;       //Under Development for the GUI width to be independent of the timebar. 
	//public static List<TimerUpdate> timers = new List<TimerUpdate>();
	public List<MonoBehaviour> timees = null;
	private GameState gameState = null;
	private MovementScripts movement = null;
    private bool isActive = false;
	private bool paused = false;
	public bool hideOnPause = false;
	public bool ignoreRelativity = false;
	public bool killOnFailure = true;
	public bool attemptAtMin = true;
	public bool attemptAtMax = true;
	public bool IsActive {
		get { return isActive; }
	}
	private float dt = 0;
	private bool inverted = false;

    void Start () 
    {
		GUIWidth = maxTime;
		GUIHeight = 20;
		GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>().AddTimer(this);
		gameState = (GameState)GameObject.FindObjectOfType(typeof(GameState));
		movement = (MovementScripts)GameObject.FindObjectOfType(typeof(MovementScripts));
		timees = new List<MonoBehaviour>();
    }


    public enum ResponseType
    {
        perfect,
        missed
    }
 
    void Update () 
    {
		if(isActive)
		{
			if (!inverted) {
	     	   AddjustCurrentTime(step);        //This can allow you to have differernt time steps for the change. 
			}
			else {
				AddjustCurrentTime(-backstep);
			}
	        //if(Input.GetKeyDown(KeyCode.F))
	        if(isActive)
			if(curTime > (pivotTime - pivot.width / 2))
			{
				var playerObj = GameObject.FindGameObjectWithTag("Playermesh");
				if (playerObj != null && playerObj.particleSystem != null) {
					playerObj.particleSystem.enableEmission = true;
					playerObj.particleSystem.Play();
				}
			}
			else
			{
				GameObject.FindGameObjectWithTag("Playermesh").particleSystem.enableEmission = false;
			}
		}

		var player = GameObject.Find ("PlayerMesh");
		if(this.isActive)
		{
			if(this.curTime >= 0 + pivotTime - pivot.width / 2 && this.curTime <= pivotTime + pivot.width)
			{
				if (player != null && player.particleSystem != null) {
					player.particleSystem.enableEmission = true;
				}
			}
			else
			{
				if (player != null && player.particleSystem != null) {
					player.particleSystem.enableEmission = false;
				}
			}
		}
    }

    private ResponseType Check()
    {
        //Rect r = pivot;
        //r.x -= perfectTimeWindow / 2;
        //r.width = perfectTimeWindow;
        if (curTime > pivotTime - pivot.width / 2 && curTime < pivotTime + pivot.width)//(r.Contains(new Vector2(timeRec.x + timeRec.width, pivot.y + pivot.height / 2)))      //If the current time is in perfect time range
        {
            return ResponseType.perfect;
        }
        else
            return ResponseType.missed;
    }
    
    void OnGUI()
    {
		// Wrap around the far side of the screen if offsets are negative
		if (offsetX < 0)
		{
			offsetX = Screen.width + (offsetX + 1) - GUIWidth;	
		}
		if (offsetY < 0)
		{
			offsetY = Screen.height + (offsetY + 1) - GUIHeight;	
		}
		
		
		if(isActive && !(hideOnPause && paused))
		{
			textRec = new Rect(offsetX, offsetY - 30, 100, GUIHeight);
	        timeRec = new Rect(offsetX, offsetY, timeBarLength , GUIHeight);
	        pivot = new Rect(offsetX + pivotTime, offsetY, perfectTimeWindow, GUIHeight);
	        backgroundRect = new Rect(offsetX, offsetY, GUIWidth, GUIHeight);
			var boxRect = backgroundRect;
			boxRect.x -= padding/2;
			boxRect.width += padding;
			boxRect.y -= padding / 2;
			boxRect.height += padding;

			GUI.Label(textRec, name);

	        GUI.DrawTexture(backgroundRect, background);
	        GUI.DrawTexture(timeRec, foreground, ScaleMode.StretchToFill, false);
	        GUI.DrawTexture(pivot, lion);
			GUI.Box (boxRect, "");
		}
    }

    public void AddjustCurrentTime(float adj)
    {
		if (paused) {
			return;
		}
		if (movement.IsRelativistic && !ignoreRelativity) {
			adj *= 1 - (float)(gameState.PlayerVelocity / gameState.totalC);
			adj /= 5.0f;
		}
		adj *= Time.deltaTime;
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
		UpdateTimer(adj);
		if ((attemptAtMax && !inverted && curTime >= maxTime) || (attemptAtMin && inverted && curTime <= 0)) {
			AttemptSuccess();
		}
    }



	public override string ToString ()
	{
		return name;
	}
	
	public void AddTimee(MonoBehaviour timee) {
		timees.Add(timee);
	}
	
	public void RemoveTimee(MonoBehaviour timee) {
		timees.Remove(timee);
	}
 
	public void StartTimer() {
		isActive = true;
		paused = false;
		inverted = false;
		curTime = startTime;
	}
	
	private void UpdateTimer(float dt) {
		foreach(MonoBehaviour timee in timees) {
			timee.gameObject.SendMessage("TimerUpdate", new TimerStep(name, dt), SendMessageOptions.DontRequireReceiver);	
		}

    }
	
	public void EndTimer() {
		isActive = false;
		foreach(MonoBehaviour timee in timees) {
			timee.gameObject.SendMessage("TimerEnd", name, SendMessageOptions.DontRequireReceiver);	
		}
	}

	public void InvertTimer(bool forceInvert = false, bool forceTo = true) {
		if (forceInvert) {
			inverted = forceTo;
		} 
		else {
			inverted = !inverted;
		}
	}
	
	public void PauseTimer() {
		paused = true;	
	}
	
	public void ResumeTimer() {
		paused = false;
	}

	public void resetTime () {
		curTime = 0f;
	}
	
	public bool AttemptSuccess(string successString = null, string failureString = null, AudioSource successAudio = null, AudioSource failureAudio = null, bool endTimer = true, bool printSuccess = true, bool printFailure = true) {
		bool success = Check() == TimerUpdate.ResponseType.perfect;
		
		if (success) {
			if (printSuccess) {
           		GUIManager.message = (successString == null ? "Perfect Time! Good job" : successString);
				if (successAudio) {
					successAudio.Play();
				}
				
			}
        }
        else {
			if (printFailure) {
           		GUIManager.message = (failureString == null ? "You Missed it! Restarting to the CheckPoint!" : failureString);
				if (failureAudio) {
					failureAudio.Play();
				}
			}
			if (killOnFailure) {
				MainGameEventScheduler.LoadAgain();
				GameObject.Find ("PlayerMesh").transform.position = MainGameEventScheduler.playerPositions.LastOrDefault();
			}
        }
		if(endTimer) {
			EndTimer();
		}
		return success;
	}
	
	/*public bool AttemptSuccessAudio(, bool endTimer = true, bool playSuccess = true, bool playFailure = true) {
		bool success = Check() == TimerUpdate.ResponseType.perfect;
		
		if (successAudio == null && failureAudio == null) {
			return AttemptSuccess(null, null, endTimer, playSuccess, playFailure);
		}
		
		if (success) {
			if (playSuccess) {
           		successAudio.Play();
			}
        }
        else {
			if (playFailure) {
           		failureAudio.Play();
			}
			if (killOnFailure) {
				MainGameEventScheduler.LoadAgain();
				GameObject.Find ("PlayerMesh").transform.position = MainGameEventScheduler.playerPositions.LastOrDefault();
			}
        }
		if(endTimer) {
			EndTimer();
		}
		return success;
	}*/
	
	public void DebugJumpToPerfect(bool pause = true)
	{
		curTime = pivotTime;
		AddjustCurrentTime(0);
		if(pause) {
			PauseTimer();
		}
	}
}

public class TimerStep {
	public string name;
	public float dt;
	
	public TimerStep(string name, float dt) {
		this.name = name;
		this.dt = dt;
	}
}
