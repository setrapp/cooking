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
	public GameObject particleSystem = null;
	public CollisionChecker buttonTrigger;
	public GameObject cage = null;
	private bool droppedCage = false;
	private float cageY;
	
	// Use this for initialization
	void Start () {
		opened = false;
		alreadyplayed = false;
		triggered = false;
		player = GameObject.FindGameObjectWithTag("Playermesh");
		cageY = cage.transform.position.y;
		//ResetDecontamination();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (buttonTrigger.Triggering && (triggered == true)) {
			
			if (!droppedCage) {
				cage.rigidbody.useGravity = true;
				droppedCage = true;
			}
			if(Input.GetKey(KeyCode.B)) {
				if(!alreadyplayed) {
					gameObject.audio.Play();
					alreadyplayed = true;
					particleSystem.SetActive(true);
					buttonTrigger.gameObject.GetComponent<InteractionPopup>().ForceOffAndDisable();
					
					decomtaminationButton.renderer.material.color = Color.green;
					GUIManager.Instance.RemoveObjective(decontamination.name);
					GUIManager.Instance.AddObjective(pleaseWait);
					StartCoroutine(doorwait());
				}
			}
		}
		
		if (opened && cage.transform.position.y < cageY) {
			cage.transform.position += Vector3.up * 0.1f;	
		}
	}
	
	void OnTriggerEnter(Collider trigger) {
		
		if(trigger.gameObject.tag.Equals("PlayerTrigger") && !triggered) {
			GUIManager.Instance.AddObjective(decontamination);			
			triggered = true;
		}
	}
	
	IEnumerator doorwait (){
		
		yield return new WaitForSeconds(7.0f);
		particleSystem.SetActive(false);
		opened = true;
		//cage.SetActive(false);
		cage.rigidbody.useGravity = false;
	}
}
