using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MainGameEventScheduler : MonoBehaviour {
    public static task currentTask = 0;
    public static List<Vector3> playerPositions = new List<Vector3>();
    public GameObject torchPrefab;
	public static bool onFire = false;
	public float timer;
	//public bool notExtinguished = false;
    public static MainGameEventScheduler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Globals").GetComponent<MainGameEventScheduler>();
            }
            return instance;
        }
    }
    public static MainGameEventScheduler instance = null;
    
    // Use this for initialization
	void Start () {
		ToastScript.isActive = true;
        currentTask = task.toaster;
	}
	
	// Update is called once per frame
	void Update () {
		//EggScript.isActive = true;
		if(onFire == true && timer < Time.time)
		{
			LoadAgain();
			GameObject.FindGameObjectWithTag("Playermesh").transform.position = playerPositions[playerPositions.Count - 1];
			onFire = false;
			Torchelight torch = GameObject.FindGameObjectWithTag("Playermesh").GetComponentInChildren<Torchelight>();
			if(torch != null)
				Destroy(torch.gameObject);
		}
	}
	
	public void FailedObjective()
	{

        GameObject torch = (GameObject)Instantiate(torchPrefab, GameObject.Find("TorchPlaceHolder").transform.position, Quaternion.identity);
        torch.transform.parent = GameObject.FindGameObjectWithTag("Playermesh").transform;
		onFire = true;
		timer = Time.time + 3;
		
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
		case 2:DisableAllEventScripts();
			OmeletteScript oScript = GameObject.Find("Objectives").GetComponent<OmeletteScript>();
			OmeletteScript.isActive = true;
			break;
		}		
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<OverheatMeter>().reset();
		player.GetComponent<MovementScripts>().ToggleSpecialRelativity(true, false);
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
		OmeletteScript.isActive = false;
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
