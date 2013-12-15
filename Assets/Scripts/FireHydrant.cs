using UnityEngine;
using System.Collections;

public class FireHydrant : MonoBehaviour {
	
	public float hydrantRange = 10;
	private GameObject player;
	private GameObject waterSpout;
	public float timer;
	
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Playermesh");
		waterSpout = GameObject.FindGameObjectWithTag("WaterSpout");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.E) && (transform.position - player.transform.position).sqrMagnitude < hydrantRange * hydrantRange && 
			gameObject.GetComponentInChildren<FireHydrantCone>().withinHydrantRange)
		{
			waterSpout.particleSystem.Play();
			timer = Time.time + 5;
			MainGameEventScheduler.onFire = false;
			Torchelight torch = GameObject.FindGameObjectWithTag("Playermesh").GetComponentInChildren<Torchelight>();
			if(torch != null)
				Destroy(torch.gameObject);
			GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
			playerObject.GetComponent<OverheatMeter>().reset();
			playerObject.GetComponent<MovementScripts>().ToggleSpecialRelativity(true, false);
			MainGameEventScheduler scheduler = GameObject.FindGameObjectWithTag("Globals").GetComponent<MainGameEventScheduler>();
			GUIManager.Instance.RemoveObjective(scheduler.findFireHydrant.name);
			GUIManager.Instance.RemoveObjective(scheduler.putOutFire.name);
		}
		if(timer < Time.time)
			waterSpout.particleSystem.Stop();
	}
}
