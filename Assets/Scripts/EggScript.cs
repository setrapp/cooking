using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class EggScript : MonoBehaviour {
	public static EggScript Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindGameObjectWithTag("Globals").GetComponentInChildren<EggScript>();	
			}
			return instance;
		}
	}
	private static EggScript instance;
	public static bool eggAcquired = false;
	private List<GameObject> eggs = new List<GameObject>();
	public TimerUpdate heatTimer = null;
	public TimerUpdate eggTimer = null;
	public TimerUpdate boilTimer = null;
	TimerManager timerManager = null;
	GameObject player = null;
	GameObject stove = null;
	private static bool boiling = false;
	private static bool heating = false;
	public static List<GameObject> destroyObjects = new List<GameObject>();
	public static bool isActive = false;
	private GameObject container = null;
	public AudioSource success = null;
	public AudioSource failure = null;
	public Objective grabEgg = new Objective("grab egg", "Find an Egg (E)");
	public Objective heatStove = new Objective("heat stove", "Set Stove to ON Mode (H)");
	public Objective placeEgg = new Objective("place egg", "Enter Egg in Boiling Water (E)");
	public Objective finishEgg = new Objective("finish egg", "Retrieve the Perfect Egg (E)");
	public InteractionPopup eggPopup = null;
	public InteractionPopup stovePopup = null;
	public InteractionPopup eggBoilerPopup = null;
	bool foundPopups = false;
	
	// Use this for initialization
	void Start () {
		
		timerManager = GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>();
		player = GameObject.FindGameObjectWithTag("Player");
		stove = GameObject.Find("Heater");
		container = GameObject.Find ("Container");
		//toastTimer = GameObject.Find("Toaster").GetComponent<TimerUpdate>();
		/*eggTimer.AddTimee(this);
		boilTimer.AddTimee(this);
		heatTimer.AddTimee(this);*/
	}
	
	// Update is called once per frame
	void Update()
	{	
		if (isActive)
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
			
			if (!foundPopups) {
				FindPopups();
				if (eggPopup != null) {
					eggPopup.enabled = true;
				}
				if (stovePopup != null) {
					stovePopup.enabled = true;
				}
				if (eggBoilerPopup != null) {
					eggBoilerPopup.enabled = true;
				}
				foundPopups = true;
			}
			
			if (Input.GetKeyDown(KeyCode.H))
			{
				
				if (!heating) {
					if (stovePopup.collisionChecker.Triggering) {
						if (heatTimer.CurTime > 0) {
							heatTimer.InvertTimer();
						} else {
							heatTimer.StartTimer();
						}
						heating = true;
						GUIManager.Instance.RemoveObjective(heatStove.name);
					}
				}
				else {
					if (stovePopup.collisionChecker.Triggering) {
						heatTimer.InvertTimer();
						heating = false;
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.E))
			{
				if(!boiling) {
					if(!eggAcquired)
					{
						eggs = GameObject.FindGameObjectsWithTag("Egg").ToList();
						foreach (var egg in eggs)
						{
							if (eggPopup.collisionChecker.Triggering)
							{
								egg.renderer.enabled = false;
								eggAcquired = true;
								destroyObjects.Add(egg);
								egg.SetActive(false);
								GUIManager.Instance.RemoveObjective(grabEgg.name);
								eggPopup.ForceOffAndDisable();
								eggBoilerPopup.enabled = true;
								//Destroy(egg);
								break;
							}
						}
						eggTimer.StartTimer();
					}
					else
					{
						if (stovePopup.collisionChecker.Triggering) {
							if(eggTimer.AttemptSuccess() && heatTimer.AttemptSuccess(null, null, success, failure, false, false)) {
								ScoreManager.Instance.timerPercent(eggTimer);
								boilTimer.StartTimer();
								boiling = true;
								GUIManager.Instance.RemoveObjective(placeEgg.name);
							}
							else {
								heatTimer.EndTimer();
							}
						}
					}
				}
				else {
					if (stovePopup.collisionChecker.Triggering) {
						if (boilTimer.AttemptSuccess()) {
							ScoreManager.Instance.timerPercent(boilTimer);
							ScoreManager.Instance.SuccessScore();
							GUIManager.Instance.RemoveObjective(finishEgg.name);
							stovePopup.ForceOffAndDisable();
							eggBoilerPopup.ForceOffAndDisable();
							MainGameEventScheduler.switchTask();
							FinishTask();	
						}
						
						/*foreach(var obj in destroyObjects)
						{
							obj.SetActive(true);
							obj.renderer.enabled = true;
							obj.transform.position = container.transform.position;
						}*/
					}
					heatTimer.EndTimer();
				}
			}
			
			
			
			/*if (eggTimer.IsActive)
			{
				if (Vector3.Distance(container.transform.position, player.transform.position) < 5)
				{
					if (Input.GetKeyDown(KeyCode.Q))
					{
						if (eggTimer.AttemptSuccess()) {
							ScoreManager.Instance.timerPercent(boilTimer);
							ScoreManager.Instance.SuccessScore();
							GUIManager.Instance.RemoveObjective(finishEgg.name);
							stovePopup.ForceOffAndDisable();
							eggBoilerPopup.ForceOffAndDisable();
							MainGameEventScheduler.switchTask();
							FinishTask();
						}
						else
						{
							LoadFromDestroy();
						}
					}
				}
				return;
			}*/
		}
		
	}
	
	public void StartTask() 
	{
		GUIManager.Instance.AddObjective(grabEgg);
		GUIManager.Instance.AddObjective(heatStove);
		GUIManager.Instance.AddObjective(placeEgg);
		GUIManager.Instance.AddObjective(finishEgg);
		foundPopups = false;
		FindPopups();		
		boiling = false;
		heating = false;
		eggAcquired = false;
		eggPopup.enabled = true;
		stovePopup.enabled = true;
		eggBoilerPopup.enabled = false;
	}

	public void FinishTask()
	{
		//ScoreManager.Instance.SuccessScore();
		//var container = GameObject.Find ("Container");
		//GameObject.Find("Frying pan").SetActive(true);
		//GameObject.Find("Frying pan").renderer.enabled = true;
		/*foreach(var obj in destroyObjects)
		{
			obj.SetActive(true);
			obj.renderer.enabled = true;
			obj.transform.position = container.transform.position;
		}*/
		boiling = false;
		heating = false;
		eggAcquired = false;
	}

	public static void LoadFromDestroy()
	{
		var container = GameObject.Find ("Container");
        GameObject.Find("Frying pan").SetActive(true);
        GameObject.Find("Frying pan").renderer.enabled = true;
		foreach(var obj in destroyObjects)
		{
			obj.SetActive(true);
			obj.renderer.enabled = true;
			//obj.transform.position = container.transform.position;
		}
		boiling = false;
		heating = false;
		eggAcquired = false;
	}
	
	private void FindPopups() {
		if (eggPopup == null || stovePopup == null || eggBoilerPopup == null) {
			GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup");
			for (int i = 0; i < popups.Length; i++) {	
				InteractionPopup popup = popups[i].GetComponent<InteractionPopup>();
				if (popup.name.Equals("egg")) {
					eggPopup = popup;
				}
				else if (popup.name.Equals("heater")) {
					stovePopup = popup;
				}
				else if (popup.name.Equals("boiling")) {
					eggBoilerPopup = popup;
				}
			}
		}
	}
}
