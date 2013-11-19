using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ToastScript : MonoBehaviour {
	public bool breadAcquired = false;
	private List<GameObject> breads = new List<GameObject>();
	TimerUpdate toastTimer = null;
	
	public static bool isActive = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            breads = GameObject.FindGameObjectsWithTag("Bread").ToList();
            foreach (var bread in breads)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (Vector3.Distance(bread.transform.position, this.transform.position) < 5)
                    {
                        bread.renderer.enabled = false;
                        breadAcquired = true;
                        Destroy(bread);
                        Debug.Log(breadAcquired);
                        break;
                    }
                }
            }

            

            var Toaster = GameObject.Find("Toaster");
            if (toastTimer != null)
            {
                if (Vector3.Distance(this.transform.position, Toaster.transform.position) < 5)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (toastTimer.Check() == TimerUpdate.ResponseType.perfect)
                        {
                            GUIManager.message = "Perfect Time! Good job";
                        }
                        else
                        {
                            GUIManager.message = "You Missed it! ";
                        }
                    }
                }
                return;
            }
            else
                if (Vector3.Distance(this.transform.position, Toaster.transform.position) < 5)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (breads.Count == 0)
                        {
                            if (breadAcquired)
                            {
                                foreach (var timer in TimerUpdate.timers)
                                {
                                    if (timer.name == "Toaster")
                                    {
                                        toastTimer = timer;
                                        toastTimer.isActive = true;
                                        GUIManager.message = "Shut the toaster before the bread burns. Press 'f' to shut the toster. Make sure you shut it at the right time, else the bread wont toast well";
                                        return;
                                    }
                                }
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

}
