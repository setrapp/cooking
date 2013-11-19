using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ToastScript : MonoBehaviour {
	public bool breadAcquired = false;
	private List<GameObject> breads = new List<GameObject>();
	TimerUpdate toastTimer = null;
	TimerManager timerManager = null;
	GameObject player = null;
	
	public static bool isActive = false;
	// Use this for initialization
	void Start () {
		
		timerManager = GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>();
		player = GameObject.FindGameObjectWithTag("Player");
		//toastTimer = GameObject.Find("Toaster").GetComponent<TimerUpdate>();
	}
	
	// Update is called once per frame
    void Update()
    {
		if (toastTimer == null) {
			toastTimer = timerManager.FindTimer("Toaster");
			toastTimer.AddTimee(this);
		}
		
        if (isActive)
        {
            breads = GameObject.FindGameObjectsWithTag("Bread").ToList();
            foreach (var bread in breads)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (Vector3.Distance(bread.transform.position, player.transform.position) < 5)
                    {
                        bread.renderer.enabled = false;
                        breadAcquired = true;
                        Destroy(bread);
                        Debug.Log(breadAcquired);
                        break;
                    }
                }
            }

            

            if (toastTimer.IsActive)
            {
                if (Vector3.Distance(this.transform.position, player.transform.position) < 5)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        toastTimer.AttemptCompleteTimer();
                    }
                }
                return;
            }
            else
                if (Vector3.Distance(this.transform.position, player.transform.position) < 5)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (breads.Count == 0)
                        {
                            if (breadAcquired)
                            {
								timerManager.StartTimer("Toaster");
                                GUIManager.message = "Shut the toaster before the bread burns. Press 'f' to shut the toster. Make sure you shut it at the right time, else the bread wont toast well";
                                return;
                            }
                        }
                        else
                        {
                            GUIManager.message = "Grab all breads, before you turn the toaster ON";
                        }
                    }
                }
                else
                {
                    GUIManager.message = "Find breads and toast them";
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
