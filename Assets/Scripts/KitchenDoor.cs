using UnityEngine;
using System.Collections;

public class KitchenDoor : MonoBehaviour
{
	void OnTriggerEnter(Collider trigger) {
		if(trigger.gameObject.tag.Equals("PlayerTrigger")) {
			/*ToastScript toastScript = GameObject.FindGameObjectWithTag("Globals").GetComponentInChildren<ToastScript>();
			GUIManager.Instance.AddObjective(toastScript.grabToast);
			GUIManager.Instance.AddObjective(toastScript.toastToast);
			GUIManager.Instance.AddObjective(toastScript.finishToast);*/
			MainGameEventScheduler.switchTask();
		}
	}
}

