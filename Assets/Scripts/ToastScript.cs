using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ToastScript : MonoBehaviour {
	public static ToastScript Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindGameObjectWithTag("Globals").GetComponentInChildren<ToastScript>();	
			}
			return instance;
		}
	}
	private static ToastScript instance;
	public bool breadAcquired = false;
	private List<GameObject> breads = new List<GameObject>();
	private TimerUpdate toastTimer = null;
	TimerManager timerManager = null;
	GameObject player = null;
	GameObject toaster = null;
	public static List<GameObject> destroyObjects = new List<GameObject>();
	public static bool isActive = false;
	public AudioSource success = null;
	public AudioSource failure = null;
	public Objective grabToast = new Objective("grab toast", "Grab 100% Toast (F)");
	public Objective toastToast = new Objective("toast toast", "Begin Toasting the Toast in the Toastatron (F)");
	public Objective finishToast = new Objective("finish toast", "Save the Toast!!! Patience (F)");
	private InteractionPopup breadPopup = null;
	private InteractionPopup toasterPopup = null;
	private int haveBreadCount = 0;
	
	// Use this for initialization
	void Start () {
		
		timerManager = GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>();
		player = GameObject.FindGameObjectWithTag("Player");
		toaster = GameObject.FindGameObjectWithTag("Toaster");
		//breadPopup.enabled = false;
		//toasterPopup.enabled = false;
		//toastTimer = GameObject.Find("Toaster").GetComponent<TimerUpdate>();
		//toastTimer.AddTimee(this);
	}
	
	// Update is called once per frame
    void Update()
    {		
        if (isActive)
        {
			if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player");
				if (player == null) {
					return;
				}
			}
			
			if (toaster == null) {
				toaster = GameObject.FindGameObjectWithTag("Toaster");
				if (toaster == null) {
					return;
				}
			}
			
			if (toastTimer == null) {
				toastTimer = timerManager.FindTimer("Toaster");
				toastTimer.AddTimee(this);
			}
			
			FindPopups();
			
            breads = GameObject.FindGameObjectsWithTag("Bread").ToList();
			if (haveBreadCount < breads.Count) {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (breadPopup.collisionChecker.Triggering)
                    {
						breads[haveBreadCount].renderer.enabled = false;
						haveBreadCount++;
                    }
                }
			}
			else if (!breadAcquired){
				breadAcquired = true;
				GUIManager.Instance.RemoveObjective(grabToast.name);
				breadPopup.ForceOffAndDisable();
			}

            

            if (toastTimer.IsActive)
            {
                if (toasterPopup.collisionChecker.Triggering)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (toastTimer.AttemptSuccess(null, null, success, failure)) {
							ScoreManager.Instance.timerPercent(toastTimer);
							ScoreManager.Instance.SuccessScore();
                            MainGameEventScheduler.switchTask();
							foreach(var obj in destroyObjects)
								Destroy(obj);
							destroyObjects.Clear();
							GUIManager.Instance.RemoveObjective(finishToast.name);
							toasterPopup.ForceOffAndDisable();
						}
                    }
                }
                return;
            }
            else
                if (toasterPopup.collisionChecker.Triggering)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (breadAcquired)
                        {
							toastTimer.StartTimer();
							GUIManager.Instance.RemoveObjective(toastToast.name);
                            //GUIManager.message = "Shut the toaster before the bread burns. Press 'f' to shut the toster. Make sure you shut it at the right time, else the bread wont toast well";
							return;
                        }
                        else
                        {
                            GUIManager.message = "Grab all breads, before you turn the toaster ON";
                        }
                    }
                }
                else
                {
                    //GUIManager.message = "Find breads and toast them";
                }
        }

    }
	
	public void StartTask() 
	{
		GUIManager.Instance.AddObjective(grabToast);
		GUIManager.Instance.AddObjective(toastToast);
		GUIManager.Instance.AddObjective(finishToast);
		FindPopups();
		breadAcquired = false;
		haveBreadCount = 0;
		breadPopup.enabled = true;
		toasterPopup.enabled = true;
		foreach (var bread in breads)
	    {
			bread.renderer.enabled = true;	
		}
		toastTimer.resetTime();
	}
	
	public void TimerUpdate(TimerStep step) {
		if (step.name.Equals("Toaster")) {
			
		}
	}
	
	public void ControlTimerEnd(string timerName) {
		if (timerName.Equals("Toaster")) {
			
		}
	}

	public static void LoadFromDestroy()
	{
		foreach(var obj in destroyObjects)
		{
			obj.SetActive(true);
			obj.renderer.enabled = true;
		}
		destroyObjects.Clear();
	}
	
	private void FindPopups() {
		if (breadPopup == null || toasterPopup == null) {
			GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup");
			for (int i = 0; i < popups.Length; i++) {	
				InteractionPopup popup = popups[i].GetComponent<InteractionPopup>();
				if (popup.name.Equals("bread")) {
					breadPopup = popup;
				}
				else if (popup.name.Equals("toaster")) {
					toasterPopup = popup;
				}
			}
		}
	}
}
