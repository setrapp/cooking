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
	private Rect scoreLetterBox;
	private Rect scoreBox;
	public float timer;
	public bool showingScore = false;
	public GUIStyle objectivesStyle;
	public GUIStyle gradeLetterStyle;
	public GUIStyle gradeMessageStyle;
	//public bool isObjectivesActive = true;
	// Use this for initialization
	void Start () {
		objectives = new List<Objective>();
	}
	
	// Update is called once per frame
	void Update () {
		objectivesBox = WrapBoxToScreen(objectivesBox);
		scoreBox = WrapBoxToScreen(scoreBox);
		scoreLetterBox = new Rect(0, 0, 100, 100);
		scoreBox = new Rect (0, 0, 300, 100);
		scoreBox.center = new Vector2 (Screen.width/2, Screen.height/2);
		scoreLetterBox.center = new Vector2 (Screen.width/2, scoreBox.y - scoreLetterBox.width / 2);
		if(timer < Time.time)
		{
			showingScore = false;
		}
	}

    void OnGUI()
    {
        /*if (isActive)
        {
            GUI.Label(new Rect((Screen.width * 0.5f) - 75, 75, 150, 100), message);
        }*/
		//Print objectives
		if(showingScore == true)
		{
			GUI.Label(scoreLetterBox, ScoreManager.Instance.scoreLetter, gradeLetterStyle);
			GUI.Label(scoreBox, ScoreManager.Instance.scoreMessage, gradeMessageStyle);
		}
		if (objectives.Count > 0) {
			string objectivesString = "Objectives:\n";
			for (int i = 0; i < objectives.Count; i++) {
				objectivesString += "\t" + objectives[i].objective;
				if (i < objectives.Count - 1) {
					objectivesString += "\n";	
				}
			}
			GUI.Label(objectivesBox, objectivesString, objectivesStyle);	
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
				i--;
			}
		}
	}
	
	public void RemoveAllObjectives() {
		for (int i = 0; i < objectives.Count;) {
			objectives.RemoveAt(i);
		}
	}
	public void ShowScore() {
		timer = Time.time + 4;
		showingScore = true;
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
