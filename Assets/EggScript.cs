using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class EggScript : MonoBehaviour {
	public bool eggAcquired = false;
	private List<GameObject> eggs = new List<GameObject>();
	public TimerUpdate heatTimer = null;
	public TimerUpdate eggTimer = null;
	public TimerUpdate boilTimer = null;
	TimerManager timerManager = null;
	GameObject player = null;
	GameObject stove = null;
	private bool boiling = false;
	private bool heating = false;

	public static bool isActive = false;
	// Use this for initialization
	void Start () {
		
		timerManager = GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>();
		player = GameObject.FindGameObjectWithTag("Player");
		stove = GameObject.FindGameObjectWithTag("Stove");
		//toastTimer = GameObject.Find("Toaster").GetComponent<TimerUpdate>();
		/*eggTimer.AddTimee(this);
		boilTimer.AddTimee(this);
		heatTimer.AddTimee(this);*/
	}
	
	// Update is called once per frame
	void Update()
	{
		if (eggTimer == null) {
			eggTimer = timerManager.FindTimer("Egg");
			eggTimer.AddTimee(this);
		}

		if (boilTimer == null) {
			boilTimer = timerManager.FindTimer("Boil");
			boilTimer.AddTimee(this);
		}

		if (heatTimer == null) {
			heatTimer = timerManager.FindTimer("Heat");
			heatTimer.AddTimee(this);
		}
		
		if (isActive)
		{
			if (Input.GetKeyDown(KeyCode.H))
			{
				if(!boiling) {
					if (!heating) {
						if (Vector3.Distance(stove.transform.position, player.transform.position) < 5) {
							heatTimer.StartTimer();
							heating = true;
						}
					}
					else {
						if (Vector3.Distance(stove.transform.position, player.transform.position) < 5) {
							heatTimer.InvertTimer();
							heating = false;
						}
					}
				}
				else {
					if (Vector3.Distance(stove.transform.position, player.transform.position) < 5) {
						boilTimer.AttemptCompleteTimer();
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.E))
			{
				if(!eggAcquired)
				{
					eggs = GameObject.FindGameObjectsWithTag("Egg").ToList();
					foreach (var egg in eggs)
					{
						if (Vector3.Distance(egg.transform.position, player.transform.position) < 5)
						{
							egg.renderer.enabled = false;
							eggAcquired = true;
							Destroy(egg);
							Debug.Log(eggAcquired);
							break;
						}
					}
					eggTimer.StartTimer();
				}
				else
				{
					if (Vector3.Distance(stove.transform.position, player.transform.position) < 5) {
						if(eggTimer.AttemptCompleteTimer()) {
							boilTimer.StartTimer();
							boiling = true;
						}
					}
				}
			}
			
			
			
			if (eggTimer.IsActive)
			{
				if (Vector3.Distance(this.transform.position, player.transform.position) < 5)
				{
					if (Input.GetKeyDown(KeyCode.E))
					{
						if (eggTimer.AttemptCompleteTimer()) {
							MainGameEventScheduler.switchTask();
						}
					}
				}
				return;
			}
			else
				if (Vector3.Distance(this.transform.position, player.transform.position) < 5)
			{
				if (Input.GetKeyDown(KeyCode.F))
				{
					if (eggs.Count == 0)
					{
						if (eggAcquired)
						{
							eggTimer.StartTimer();
							//GUIManager.message = "Shut the toaster before the bread burns. Press 'f' to shut the toster. Make sure you shut it at the right time, else the bread wont toast well";
							return;
						}
					}
					else
					{
						//GUIManager.message = "Grab all breads, before you turn the toaster ON";
					}
				}
			}
			else
			{
				//GUIManager.message = "Find breads and toast them";
			}
		}
		
	}
	
	public void TimerUpdate(TimerStep step) {
		if (step.name.Equals("Toaster")) {
			
		}
	}
	
	public void ControlTimerEnd(string timerName) {
		if (timerName.Equals("Toaster")) {
			
		}
	}
}
