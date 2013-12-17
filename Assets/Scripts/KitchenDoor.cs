using UnityEngine;
using System.Collections;

public class KitchenDoor : MonoBehaviour
{
	bool startedToast = false;
	
	void OnTriggerEnter(Collider trigger) {
		if(!startedToast && trigger.gameObject.tag.Equals("PlayerTrigger")) {
			MainGameEventScheduler.switchTask();
			startedToast = true;
		}
	}
}

