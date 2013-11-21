using UnityEngine;
using System.Collections;

public class TriggerSwapper : MonoBehaviour
{
	public GameObject swapObjectOn = null;
	public GameObject swapObjectOff = null;
	bool swapped = false;
	
	void OnTriggerEnter(Collider collider) {
		swapObjectOn.SetActive(true);
		swapObjectOff.SetActive(false);
	}
}

