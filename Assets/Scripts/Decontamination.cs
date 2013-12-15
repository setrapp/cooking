using UnityEngine;
using System.Collections;

public class Decontamination : MonoBehaviour {
	
	
	public bool opened;
	public bool alreadyplayed;
	public bool triggered;
	public Objective decontamination = new Objective("decontaminate", "Press Button for Decontamination (B)");
	public Objective pleaseWait = new Objective("wait", "Please Wait ...");
	public GameObject decomtaminationButton;
	private GameObject player = null;
	public float buttonRange = 10;
	
	// Use this for initialization
	void Start () {
		opened = false;
		alreadyplayed = false;
		triggered = false;
		player = GameObject.FindGameObjectWithTag("Playermesh");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.B) && (triggered == true) && (decomtaminationButton.transform.position - player.transform.position).sqrMagnitude < buttonRange * buttonRange) {
			if(alreadyplayed == false) {
				gameObject.audio.Play();
				alreadyplayed = true;
			}
			
			decomtaminationButton.renderer.material.color = Color.green;
			GUIManager.Instance.RemoveObjective(decontamination.name);
			GUIManager.Instance.AddObjective(pleaseWait);
			StartCoroutine(doorwait());
			
		}
	
	}
	
	void OnTriggerEnter(Collider trigger) {
		
		if(trigger.gameObject.tag.Equals("PlayerTrigger") && !opened) {
			GUIManager.Instance.AddObjective(decontamination);
			
			triggered = true;
		}
	}
	
	IEnumerator doorwait (){
		
		yield return new WaitForSeconds(7.0f);
		opened = true;
	}
}
