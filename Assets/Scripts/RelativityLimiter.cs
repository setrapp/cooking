using UnityEngine;
using System.Collections;

public class RelativityLimiter : MonoBehaviour
{
	public MovementScripts mover = null;
 	public bool disableRelativity = true;
	
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag.Equals("PlayerTrigger")) {
			if (mover == null) {
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				if (player != null) {
					mover = player.GetComponent<MovementScripts>();
				}
				if (mover == null) {
					return;	
				}
			}
			mover.relativityAvailable = !disableRelativity;
			mover.ToggleSpecialRelativity(true, false);
		}
	}
}

