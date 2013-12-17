using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimerManager : MonoBehaviour
{
	public static TimerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>();
            }
            return instance;
        }
    }
    public static TimerManager instance = null;
	
	List<TimerUpdate> timers = new List<TimerUpdate>();
	
	private GameState gameState = null;
	public GameState GameState {
		get {
			if (gameState == null) {
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				if (player) {
					gameState = player.GetComponent<GameState>();
				}
			} 
			return gameState;
		}
	}
	private MovementScripts movement = null;
	public MovementScripts Movement {
		get {
			if (movement == null) {
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				if (player) {
					movement = player.GetComponent<MovementScripts>();
				}
			} 
			return movement;
		}
	}
	
	
	void Start() {
	}
	
	void OnGUI() {
        /*if (GUI.Button(new Rect(10, Screen.height - 40, 150, 30), "Perfect!!!")) {
            DebugJumpToPerfect(true);
        }*/
	}
	
	public void AddTimer(TimerUpdate timer) {
		timers.Add(timer);
	}
	
	public void RemoveTimer(TimerUpdate timer) {
		timers.Remove(timer);
	}
	
	public void RemoveTimer(string timerName) {
		RemoveTimer(FindTimer("timeName"));
	}
	
	public TimerUpdate FindTimer(string name) {
		TimerUpdate foundTimer = null;
		foreach(TimerUpdate timer in timers) {
			if (timer.name.Equals(name)) {
				foundTimer = timer;	
			}
		}
		return foundTimer;
	}
	
	public void StartTimer(string name) {
		TimerUpdate timer = FindTimer(name);
		timer.StartTimer();
	}
	
	public void DebugJumpToPerfect(bool pause = true) {
		foreach (TimerUpdate timer in timers) {
			timer.DebugJumpToPerfect(pause);
		}
	}
	
	
}

