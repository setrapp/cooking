using UnityEngine;
using System.Collections;

public class OverheatMeter : MonoBehaviour
{
	public MovementScripts mover = null;
	private TimerUpdate overheatTimer = null;
	TimerManager timerManager = null;
	private bool isVisible = false;
	private bool isHeating = false;
	
	void Start () {
		timerManager = GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>();
		isVisible = false;
	}
	
	void Update ()
	{
		if (overheatTimer == null) {
			overheatTimer = timerManager.FindTimer("RALPH Overheat");
			overheatTimer.AddTimee(this);
			overheatTimer.StartTimer();
			overheatTimer.PauseTimer();
		}
		
		if (mover != null) {
			if (mover.IsRelativistic || overheatTimer.CurTime > 0) {
				if (!isVisible) {
					overheatTimer.ResumeTimer();
					isVisible = true;
				}
			}
			else {
				if (isVisible) {
					overheatTimer.PauseTimer();
					isVisible = false;
				}
			}
			
			if (mover.IsRelativistic && !isHeating) {
				isHeating = true;
				overheatTimer.InvertTimer(true, false);
			}
			else if (!mover.IsRelativistic && isHeating) {
				isHeating = false;
				overheatTimer.InvertTimer(true, true);
			}
		}
	}
}

