using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OmeletteScript : MonoBehaviour {
    public static bool isActive;
    public bool onionsCollected = false;
    public bool mushroomsCollected = false;
    public static List<GameObject> destroyObjects = new List<GameObject>();
    public bool eggsCollected = false;
    public bool oilHeated = false;
    public TimerUpdate oilHeat;
    public TimerUpdate onionCook;
    public TimerUpdate mushroomsCook;
    public TimerUpdate omletteCook;
    public TimerManager timeManager;
    public bool timerActive;
    private TimerUpdate currentTimer = null;
    GameObject player;
    public bool onionsPickedup = false;
    public bool mushroomsPickedUp = false;
    // Use this for initialization
	void Start () {
        timeManager = GameObject.Find("Globals").GetComponent<TimerManager>();
        oilHeat = timeManager.FindTimer("Oil Heat");
		onionCook = timeManager.FindTimer("Cook Onions");
        mushroomsCook = timeManager.FindTimer("Cook Mushrooms");
        omletteCook = timeManager.FindTimer("Make Omlette");
        currentTimer = oilHeat;
        player = GameObject.FindGameObjectWithTag("Playermesh");
		Initialize();
	}

    public void Initialize()
    {
		
		GameObject.Find("Cylinder55").SetActive(true);
		GameObject.Find("Cylinder55").renderer.enabled = true;
		GameObject.Find("Box59").SetActive(true);
		GameObject.Find("Box59").renderer.enabled = true;
		GameObject.Find("Omlette").SetActive(true);
        GameObject.Find("Omlette").renderer.enabled = true;
        GameObject.Find("Frying pan").SetActive(true);
        GameObject.Find("Omlette").particleSystem.renderer.enabled = false;
        GameObject.Find("Frying pan").renderer.enabled = true;
        GameObject.Find("Omlette").renderer.enabled = false;
        GameObject.Find("Omlette").particleSystem.enableEmission = false;
    }
	
	// Update is called once per frame
	void Update () {
        //if(Input.GetKey(KeyCode.R))
        //{
        //    var t =  GameObject.Find("Omlette");
         
        //}
		if (oilHeat == null)
		{
			oilHeat = timeManager.FindTimer("Oil Heat");
			onionCook = timeManager.FindTimer("Cook Onions");
			mushroomsCook = timeManager.FindTimer("Cook Mushrooms");
			omletteCook = timeManager.FindTimer("Make Omlette");
			currentTimer = oilHeat;
		}
		if (isActive)
        {
            if (!timerActive)
            {
                if (!oilHeated)
                {
					GUIManager.message = "Press O to complete the next task";
                    if (Input.GetKey(KeyCode.O))
                    {
                        timerActive = true;
                        currentTimer = oilHeat;
                        oilHeat.StartTimer();
                    }
                }
                else
                {
                    if (!onionsCollected)
                    {
						GUIManager.message = "Press P to complete the next task";
						if (Input.GetKeyDown(KeyCode.P))
						{
                            if (Vector3.Distance(player.transform.position, GameObject.Find("Onion Basket").transform.position) < 5)
                            {
                                foreach (var onion in GameObject.FindGameObjectsWithTag("Onions"))
                                {
                                    onion.SetActive(false);
                                    onion.renderer.enabled = false;
                                    destroyObjects.Add(onion);
                                }
                                onionsPickedup = true;
								timerActive = true;
                                currentTimer = onionCook;
                            }
                        }
                    }
                    else
                    {
                        //if (!mushroomsCollected)
                        //{
                        //    if (Input.GetKeyDown(KeyCode.E))
                        //    {
                        //        if (Vector3.Distance(player.transform.position, GameObject.Find("Mushroom Basket").transform.position) < 5)
                        //        {
                        //            foreach (var onion in GameObject.FindGameObjectsWithTag("Mushroom"))
                        //            {
                        //                //onion.SetActive(false);
                        //                onion.renderer.enabled = false;
                        //                destroyObjects.Add(onion);
                        //            }
                        //            mushroomsPickedUp = true;
                        //            timerActive = true;
                        //            currentTimer = mushroomsCook;
                        //        }
                        //    }
                        //}
                        //else
                        {
                            if (!eggsCollected)
                            {
                                omletteCook.StartTimer();
								GUIManager.message = "Press P to complete the next task";

								GameObject.Find("Omlette").renderer.enabled = true;
                                GameObject.Find("Omlette").particleSystem.enableEmission = true;
								GameObject.Find("Omlette").renderer.enabled = true;
								GameObject.Find("Omlette").particleSystem.enableEmission = true;
								GameObject.Find("Omlette").particleSystem.Emit(25);
								timerActive = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            else
            {
                if(onionsPickedup && !onionsCollected)
                {
                    if (Vector3.Distance(player.transform.position, GameObject.Find("Frying pan").transform.position) < 5)
                        if (Input.GetKey(KeyCode.E))
                        {
                            onionCook.StartTimer();
                            timerActive = true;
                            return;
                        }
                }
                if(mushroomsPickedUp && !mushroomsCollected)
                {
                    if (Vector3.Distance(player.transform.position, GameObject.Find("Frying pan").transform.position) < 5)
                        if (Input.GetKey(KeyCode.E))
                        {
                            mushroomsCook.StartTimer();
                            timerActive = true;
                            return;
                        }
                }
                
                if (timerActive)
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        if (currentTimer.name.Equals(oilHeat.name))
                        {
                            if (Vector3.Distance(player.transform.position, GameObject.Find("Frying pan").transform.position) < 5)
                                if (currentTimer.AttemptSuccess("Just in Time, now find onions from frdige and cook them.", "Oops you over heated oil", null, null, false, true, true))
                                {
                                    oilHeated = true;
                                }
                                else
                                {
                                    LoadFromDestroy();
                                    oilHeat.resetTime();
                                }
							timerActive = false;
                        }

                        if (currentTimer.name.Equals(onionCook.name))
                        {
                            if (currentTimer.AttemptSuccess("You have heated onions, quick get mushrooms from fridge and heat them too!", "The onions get dark and over cooked, restarting from last checkpoint", null, null, true, true, true))
                            {
                                onionsCollected = true;
                            }
                            else
                            {
                                LoadFromDestroy();
                                oilHeat.resetTime();
                            }
                        }

                        if (currentTimer.name.Equals(mushroomsCook.name))
                        {
                            if (currentTimer.AttemptSuccess("Mushrooms are cooked, time to grab eggs and pour the yolk in the frying pan", "Mushrooms are burnt and ruined, try again!", null, null, true, true, true))
                            {
                                mushroomsCollected = true;
                            }
                            else
                            {
                                LoadFromDestroy();
                                oilHeat.resetTime();
                                mushroomsCook.resetTime();
                            }
                        }

                        if (currentTimer.name.Equals(omletteCook.name))
                        {
                            if (currentTimer.AttemptSuccess(null, null, null, null, true, true, true) && currentTimer.AttemptSuccess("You just learnt how to make a omlette succesfully, but dont flatter yourself, its just an omlette", "Failed in the last step, retry making the omlette", null, null, true, true, true))
                            {
                                MainGameEventScheduler.switchTask();
                            }
                            else
                            {
                                LoadFromDestroy();
                                omletteCook.resetTime();
                            }
                        }

                        currentTimer = null;
                        timerActive = false;
                    }
                }
            }
        }
	}

    public void LoadFromDestroy()
    {
        GameObject.Find("Omlette").particleSystem.enableEmission = false;
        foreach(var obj in destroyObjects)
        {
            obj.SetActive(true);
            obj.renderer.enabled = true;
        }
    }
}

