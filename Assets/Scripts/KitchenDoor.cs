using UnityEngine;
using System.Collections;

public class KitchenDoor : MonoBehaviour
{	
	void OnTriggerEnter(Collider trigger) {
		if(MainGameEventScheduler.currentTask < task.toaster && trigger.gameObject.tag.Equals("PlayerTrigger")) {
			MainGameEventScheduler.switchTask();
		}
	}
}

