using UnityEngine;
using System.Collections;

public class RelativityLimiter : MonoBehaviour
{
	public MovementScripts mover = null;
 	public bool disableRelativity = true;
	
	void OnTriggerEnter(Collider collider) {
		mover.ToggleSpecialRelativity(true, !disableRelativity);
	}
}

