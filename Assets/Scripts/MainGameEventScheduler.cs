﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MainGameEventScheduler : MonoBehaviour {
    public static task currentTask = 0;
    public static List<Vector3> playerPositions = new List<Vector3>();
	// Use this for initialization
	void Start () {
		ToastScript.isActive = true;
        currentTask = task.toaster;
	}
	
	// Update is called once per frame
	void Update () {
		//EggScript.isActive = true;
	}

    public static void LoadAgain()
    {
        switch ((int)currentTask)
        {
            case 0: DisableAllEventScripts();
                ToastScript.isActive = true;
                
                break;
            case 1: DisableAllEventScripts();
                EggScript.isActive = true;
                break;
<<<<<<< HEAD
		case 2:DisableAllEventScripts();
			OmeletteScript oScript = GameObject.Find("Objectives").GetComponent<OmeletteScript>();
			OmeletteScript.isActive = true;
			break;
		}
	}
	public static void switchTask()
=======
        }
		
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<OverheatMeter>().reset();
		player.GetComponent<MovementScripts>().ToggleSpecialRelativity(true, false);
    }
    public static void switchTask()
>>>>>>> e20cb1fcc0c7a666110c3584a2fefb2f43e4198a
    {
        if ((int)currentTask < (int)task.none - 1)
        {
            currentTask++;
        }
        switch ((int)currentTask)
        {
            case 0: DisableAllEventScripts();
                ToastScript.isActive = true;
				GUIManager.message = "Heat stove and grab egg";
				return;
                break;
            case 1: DisableAllEventScripts();
				EggScript.isActive = true;
				return;
                break;
            case 2: DisableAllEventScripts();
                OmeletteScript oScript = GameObject.Find("Objectives").GetComponent<OmeletteScript>();
                oScript.Initialize();
                OmeletteScript.isActive = true;
				return;
                break;
        }
    }

    private static void DisableAllEventScripts()
    {
        ToastScript.isActive = false;
		EggScript.isActive = false;
    }
}


public enum task
{
    toaster = 0, 
    eggs = 1,
    omlette = 2,
    //Quiche = 3,
    none = 3
}
