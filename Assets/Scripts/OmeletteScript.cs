using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    // Use this for initialization
	void Start () {
        timeManager = GameObject.Find("Globals").GetComponent<TimerManager>();
        oilHeat = timeManager.FindTimer("Oil Heat");
		onionCook = timeManager.FindTimer("Cook Onions");
        mushroomsCook = timeManager.FindTimer("Cook Mushrooms");
        omletteCook = timeManager.FindTimer("Make Omlette");
        currentTimer = oilHeat;
        player = GameObject.FindGameObjectWithTag("Playermesh");
	}

    public void Initialize()
    {
        GameObject.Find("Omlette").SetActive(true);
        GameObject.Find("Omlette").renderer.enabled = true;
        GameObject.Find("Frying pan").SetActive(true);
        GameObject.Find("Frying pan").renderer.enabled = true;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
			if(!timerActive)
			{
            if (!oilHeated)
            {
                if (Input.GetKey(KeyCode.E))
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
                    if (Input.GetKey(KeyCode.E))
                    {
                        if (Vector3.Distance(player.transform.position, GameObject.Find("Onion Basket").transform.position) < 5)
                        {
                            foreach (var onion in GameObject.FindGameObjectsWithTag("Onions"))
                            {
                                onion.SetActive(false);
                                onion.renderer.enabled = false;
                                destroyObjects.Add(onion);
                            }
                            onionCook.StartTimer();
                            timerActive = true;
                            currentTimer = onionCook;
                        }
                    }
                }
                else
                {
                    if (!mushroomsCollected)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {

                            if (Input.GetKey(KeyCode.E))
                            {
                                if (Vector3.Distance(player.transform.position, GameObject.Find("Mushroom").transform.position) < 5)
                                {
                                    foreach (var onion in GameObject.FindGameObjectsWithTag("Mushroom"))
                                    {
                                        onion.SetActive(false);
                                        onion.renderer.enabled = false;
                                        destroyObjects.Add(onion);
                                    }
                                    mushroomsCook.StartTimer();
                                    timerActive = true;
                                    currentTimer = mushroomsCook;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!eggsCollected)
                        {
                            omletteCook.StartTimer();
                            timerActive = true;
                        }
                        else
                        {
                           
                        }
                    }
                }
            }
			}
            if(timerActive)
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    if (currentTimer.name.Equals(oilHeat.name))
                    {
                        if(Vector3.Distance(player.transform.position, GameObject.Find("Frying pan").transform.position) < 5)
                            if(currentTimer.AttemptSuccess("Just in Time, now find onions from frdige and cook them.", "Oops you over heated oil", true, true, true))
                            {
                                oilHeated = true;
                            }
                    }

                    if(currentTimer.name.Equals(onionCook.name))
                    {
                        if(currentTimer.AttemptSuccess("You have heated onions, quick get mushrooms from fridge and heat them too!", "The onions get dark and over cooked, restarting from last checkpoint", true, true, true))
                        {
                            onionsCollected = true;
                        }
                    }

                    if(currentTimer.name.Equals(mushroomsCook.name))
                    {
                        if(currentTimer.AttemptSuccess("Mushrooms are cooked, time to grab eggs and pour the yolk in the frying pan", "Mushrooms are burnt and ruined, try again!", true, true, true))
                        {
                            mushroomsCollected = true;
                        }
                    }

                    if(currentTimer.name.Equals(omletteCook.name))
                    {
                        if(currentTimer.AttemptSuccess("You just learnt how to make a omlette succesfully, but dont flatter yourself, its just an omlette", "Failed in the last step, retry making the omlette", true, true, true))
                        {
                            //FINISH
                        }
                    }

                    currentTimer = null;
                    timerActive = false;
                }
            }
        }
	}

    public static void LoadFromDestroy()
    {
        foreach(var obj in destroyObjects)
        {
            obj.SetActive(true);
            obj.renderer.enabled = true;
        }
    }
}
