using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MainGameEventScheduler : MonoBehaviour {
    public static task currentTask = 0;
    public static List<Vector3> playerPositions = new List<Vector3>();
    public GameObject torchPrefab;
	public static bool onFire;
	public Objective findFireHydrant = new Objective("find hydrant", "Find the Fire Hydrant");
	public Objective putOutFire = new Objective("on fire", "STOP BEING ON FIRE (E)");
	public float maxTime;
	private TimerUpdate fireTimer;
	public float currentTime;
	public float timer;

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
	}
	
	// Update is called once per frame
	void Update () {
		if (fireTimer == null) {
			fireTimer = GetComponent<TimerManager>().FindTimer("On Fire Timer");	
		}
		
		//EggScript.isActive = true;
		maxTime = fireTimer.maxTime;
		currentTime = fireTimer.CurTime;
		if(onFire == true && currentTime >= maxTime)
		{
			LoadAgain();
			GameObject.FindGameObjectWithTag("Playermesh").transform.position = playerPositions[playerPositions.Count - 1];
			onFire = false;
			Torchelight torch = GameObject.FindGameObjectWithTag("Playermesh").GetComponentInChildren<Torchelight>();
			fireTimer.resetTime();
			fireTimer.EndTimer();
			if(torch != null)
				Destroy(torch.gameObject);
		}
	}

	public void FailedObjective()
	{
        GameObject torch = (GameObject)Instantiate(torchPrefab, GameObject.Find("TorchPlaceHolder").transform.position, Quaternion.identity);
        torch.transform.parent = GameObject.FindGameObjectWithTag("Playermesh").transform;
		onFire = true;
		fireTimer.StartTimer();
		GUIManager.Instance.AddObjective(findFireHydrant);
		GUIManager.Instance.AddObjective(putOutFire);
	}
	
    public static void LoadAgain()
    {
		ScoreManager.Instance.ResetScore();
		GUIManager.Instance.RemoveAllObjectives();
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<OverheatMeter>().reset();
		player.GetComponent<MovementScripts>().ToggleSpecialRelativity(true, false);
		
		switchTask(true);
    }
    public static void switchTask(bool resetFromFailure = false)
    {
        if ((int)currentTask < (int)task.none - 1)
        {
			if (!resetFromFailure) {
           		currentTask++;
			}
        }
        switch (currentTask)
        {
            case task.toaster: DisableAllEventScripts();
                ToastScript.isActive = true;
				ToastScript.Instance.StartTask();
				//GUIManager.message = "Heat stove and grab egg";
				return;
                break;
            case task.eggs: DisableAllEventScripts();
				EggScript.isActive = true;
				EggScript.Instance.StartTask();
				return;
                break;
            /*case task.omlette: DisableAllEventScripts();
                OmeletteScript oScript = GameObject.Find("Objectives").GetComponent<OmeletteScript>();
                oScript.Initialize();
                OmeletteScript.isActive = true;
				return;
                break;*/
        }
    }

    private static void DisableAllEventScripts()
    {
        ToastScript.isActive = false;
		EggScript.isActive = false;
		OmeletteScript.isActive = false;
    }
	
	void OnGUI() {
        if (GUI.Button(new Rect(Screen.width - 160, Screen.height - 40, 150, 30), "Fail!!!")) {
            FailedObjective();
        }
	}
}


public enum task
{
    open = 0,
	toaster,
    eggs,
    omlette,
    //Quiche,
    none
}
