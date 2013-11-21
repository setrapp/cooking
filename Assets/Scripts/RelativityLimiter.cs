using UnityEngine;
using System.Collections;

public class RelativityLimiter : MonoBehaviour
{
	public MovementScripts mover = null;
 	public bool disableRelativity = true;
	
	void OnTriggerEnter(Collider collider) {
		mover.relativityAvailable = !disableRelativity;
		mover.ToggleSpecialRelativity(true, false);
	}
}

