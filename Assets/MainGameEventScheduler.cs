using UnityEngine;
using System.Collections;

public class MainGameEventScheduler : MonoBehaviour {
    public static task currentTask = 0;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        ToastScript.isActive = true;
	}

    public static void switchTask()
    {
        if ((int)currentTask < (int)task.none - 1)
        {
            currentTask++;
        }
        switch ((int)currentTask)
        {
            case 0: DisableAllEventScripts();
                ToastScript.isActive = true;
                break;
            case 1: DisableAllEventScripts();
                break;
        }
    }

    private static void DisableAllEventScripts()
    {
        ToastScript.isActive = false;
    }
}


public enum task
{
    toaster = 0, 
    eggs = 1,
    omlette = 2,
    Quiche = 3,
    none = 4
}