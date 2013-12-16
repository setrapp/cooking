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
				
				if (!heating) {
					if (Vector3.Distance(stove.transform.position, player.transform.position) < 5) {
						heatTimer.StartTimer();
						heating = true;
						GUIManager.Instance.RemoveObjective(heatStove.name);
					}
				}
				else {
					if (Vector3.Distance(stove.transform.position, player.transform.position) < 5) {
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
							if (Vector3.Distance(egg.transform.position, player.transform.position) < 5)
							{
								egg.renderer.enabled = false;
								eggAcquired = true;
								destroyObjects.Add(egg);
								egg.SetActive(false);
								GUIManager.Instance.RemoveObjective(grabEgg.name);
								eggPopup.enabled = false;
								eggBoilerPopup.enabled = true;
								//Destroy(egg);
								break;
							}
						}
						eggTimer.StartTimer();
					}
					else
					{
						if (Vector3.Distance(stove.transform.position, player.transform.position) < 5) {
							if(eggTimer.AttemptSuccess() && heatTimer.AttemptSuccess(null, null, success, failure, false, false)) {
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
					if (Vector3.Distance(stove.transform.position, player.transform.position) < 5) {
						boilTimer.AttemptSuccess();
						
						foreach(var obj in destroyObjects)
						{
							obj.SetActive(true);
							obj.renderer.enabled = true;
							obj.transform.position = container.transform.position;
						}
					}
					heatTimer.EndTimer();
				}
			}
			
			
			
			if (eggTimer.IsActive)
			{
				if (Vector3.Distance(container.transform.position, player.transform.position) < 5)
				{
					if (Input.GetKeyDown(KeyCode.Q))
					{
						if (eggTimer.AttemptSuccess()) {
							GUIManager.Instance.RemoveObjective(finishEgg.name);
							stovePopup.enabled = false;
							eggBoilerPopup.enabled = false;
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
			}
		}
		
	}
	
	public void StartTask() 
	{
		GUIManager.Instance.AddObjective(grabEgg);
		GUIManager.Instance.AddObjective(heatStove);
		GUIManager.Instance.AddObjective(placeEgg);
		GUIManager.Instance.AddObjective(finishEgg);
		eggPopup.enabled = true;
		stovePopup.enabled = true;
		eggBoilerPopup.enabled = false;
	}

	public void FinishTask()
	{
		var container = GameObject.Find ("Container");
		GameObject.Find("Frying pan").SetActive(true);
		GameObject.Find("Frying pan").renderer.enabled = true;
		foreach(var obj in destroyObjects)
		{
			obj.SetActive(true);
			obj.renderer.enabled = true;
			obj.transform.position = container.transform.position;
		}
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
}
