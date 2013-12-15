using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
	public static GUIManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindGameObjectWithTag("Globals").GetComponent<GUIManager>();
			}
			return instance;
		}
	}
	private static GUIManager instance = null;
    public static string message = string.Empty;
    public static bool isActive = true;
	public List<Objective> objectives = null;
	public Rect objectivesBox;
	//public bool isObjectivesActive = true;
	// Use this for initialization
	void Start () {
		objectives = new List<Objective>();
	}
	
	// Update is called once per frame
	void Update () {
		objectivesBox = WrapBoxToScreen(objectivesBox);
	}

    void OnGUI()
    {
        /*if (isActive)
        {
            GUI.Label(new Rect((Screen.width * 0.5f) - 75, 75, 150, 100), message);
        }*/
		if (objectives.Count > 0) {
			string objectivesString = "Objectives:\n";
			for (int i = 0; i < objectives.Count; i++) {
				objectivesString += "\t" + objectives[i].objective;
				if (i < objectives.Count - 1) {
					objectivesString += "\n";	
				}
			}
			GUI.Label(objectivesBox, objectivesString);	
		}
    }
	
	private Rect WrapBoxToScreen(Rect box) {
		// Wrap around the far side of the screen if offsets are negative
		if (box.x < 0)
		{
			box.x = Screen.width + (box.width + 1);	
		}
		if (box.y < 0)
		{
			box.y = Screen.height + (box.height + 1);	
		}
		return box;
	}
	
	public void AddObjective(string name, string objective) {
		Objective newObjective = new Objective(name, objective);
		AddObjective(newObjective);
	}
	
	public void AddObjective(Objective newObjective) {
		bool duplicate = false;
		for (int i = 0; i < objectives.Count; i++) {
			if (objectives[i].name.Equals(newObjective.name)) {
				duplicate = true;
			}
		}
		if (duplicate) {
			return;
		}
		objectives.Add (newObjective);
	}
	
	public void RemoveObjective(string name) {
		for (int i = 0; i < objectives.Count; i++) {
			if (objectives[i].name.Equals(name)) {
				objectives.RemoveAt(i);
			}
		}
	}
	
	public void RemoveAllObjectives() {
		for (int i = 0; i < objectives.Count; i++) {
			objectives.RemoveAt(i);
		}
	}
}

[System.Serializable]
public class Objective {
	public string name;
	public string objective;
	
	public Objective(string name, string objective) {
		this.name = name;
		this.objective = objective;
	}
}
