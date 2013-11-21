using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainGameEventScheduler : MonoBehaviour {
    public static task currentTask = 0;
	public static List<Vector3> playerPositions = new List<Vector3>();
	// Use this for initialization
	void Start () {
		ToastScript.isActive = true;
		playerPositions.Add(GameObject.Find("PlayerMesh").transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		//EggScript.isActive = true;
	}
	public static void LoadAgain()
	{
		Debug.Log (currentTask);
		switch ((int)currentTask)
		{
		case 0: DisableAllEventScripts();
			ToastScript.isActive = true;
			ToastScript.LoadFromDestroy();
			GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>().FindTimer("Toaster").resetTime();
			break;
		case 1: DisableAllEventScripts();
			EggScript.isActive = true;
			EggScript.LoadFromDestroy();
			GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>().FindTimer("Heat").resetTime();
			GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>().FindTimer("Egg").resetTime();
			GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>().FindTimer("Boil").resetTime();
			break;
		default : DisableAllEventScripts();
			EggScript.isActive = true;
			EggScript.LoadFromDestroy();
			GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>().FindTimer("Heat").resetTime();
			GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>().FindTimer("Egg").resetTime();
			GameObject.FindGameObjectWithTag("Globals").GetComponent<TimerManager>().FindTimer("Boil").resetTime();
			break;
		}
	}
	public static void switchTask()
	{
		playerPositions.Add(GameObject.Find("PlayerMesh").transform.position);

        if ((int)currentTask < (int)task.none - 1)
        {
            currentTask++;
        }
        switch ((int)currentTask)
        {
            case 0: DisableAllEventScripts();
                ToastScript.isActive = true;
				//EggScript.isActive = true;
				GUIManager.message = "Heat stove and grab egg";
                break;
            case 1: DisableAllEventScripts();
				EggScript.isActive = true;
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
    none = 2
}
