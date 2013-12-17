using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Globals").GetComponent<ScoreManager>();
            }
            return instance;
        }
    }
    public static ScoreManager instance = null;
	public char score;
	public string scoreLetter;
	public string scoreMessage;
	private float offPerfectPercent = 0;
	private int timersCounted = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SuccessScore() {
		
		
		if(offPerfectPercent < .1 && offPerfectPercent >= 0)
		{
			score = 'A';
			scoreMessage = "Acceptable.";
			BroadcastMessage("PlayAudio",1);
		}
		else if(offPerfectPercent > .1 && offPerfectPercent < .5){
			score = 'B';
			scoreMessage = "Needs improvement.";
			BroadcastMessage("PlayAudio",2);
		}
		else if(offPerfectPercent > .5 && offPerfectPercent < 1){
			score = 'C';
			scoreMessage = "Barely adequate.";
			BroadcastMessage("PlayAudio",3);
		}
		else if(offPerfectPercent > 1 && offPerfectPercent < 1.5){
			score = 'D';
			scoreMessage = "Not acceptable. Prepare self destruct sequence.";
			BroadcastMessage("PlayAudio",4);
		}
		else
		{
			score = 'F';
			scoreMessage = "Utter failure. Terminate ASAP.";
			BroadcastMessage("PlayAudio",5);
		}
		scoreLetter = "" + score;
		
		GUIManager.Instance.ShowScore();
		ResetScore();
	}
	
	public void timerPercent (TimerUpdate timerAdd) {
		if(timerAdd.perfectTimeWindow <= 0)
			return;
		float percent = (timerAdd.CurTime - timerAdd.pivotTime)/timerAdd.perfectTimeWindow;
		if(percent < 0 || percent > 1)
			return;
		if(offPerfectPercent == 0)
		{
			offPerfectPercent = percent;
		}
		else
			offPerfectPercent = (offPerfectPercent*timersCounted + percent)/timersCounted;
		
		timersCounted++;
	}
	
	public void ResetScore(){
	
		offPerfectPercent = 0;
		timersCounted = 0;
	}
}
