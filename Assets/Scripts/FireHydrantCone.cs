using UnityEngine;
using System.Collections;

public class FireHydrantCone : MonoBehaviour {
	public bool withinHydrantRange = false;
	
	void OnTriggerStay(Collider player)
	{
		if(player.gameObject.tag.Equals("PlayerTrigger"))
			withinHydrantRange = true;
	}
	
	void OnTriggerExit(Collider player)
	{
		if(player.gameObject.tag.Equals("PlayerTrigger"))
			withinHydrantRange = false;
	}
}
