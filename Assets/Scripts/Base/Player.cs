using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	OutlineObject pickingCacheObject = null;

	public virtual void Start () {

	}
	
	public virtual void Update () {
		CheckPickObject();
		Ray ray = this.GetRay();
		Debug.DrawRay(ray.origin, ray.direction,Color.green);
	}
	
	
	protected virtual void CheckPickObject() {;
		RaycastHit hit;
		Ray ray = this.GetRay();
		Debug.DrawRay(ray.origin, ray.direction,Color.green);
		if(Physics.Raycast(ray.origin, ray.direction, out hit, 100)) 
		{
			OutlineObject obj = hit.collider.gameObject.GetComponent<OutlineObject>();

			if(obj != null) {
				Debug.Log("asdsd");
				if(obj.enabled) {
	           		if(obj != pickingCacheObject) {
						obj.OnPickingEnter();
						if(pickingCacheObject)
							pickingCacheObject.OnPickingExit();
						pickingCacheObject = obj;
	 	           	}
				}
    	     } else {
				if(pickingCacheObject)
					pickingCacheObject.OnPickingExit();
				pickingCacheObject = null;
			}
		} else {
				if(pickingCacheObject)
					pickingCacheObject.OnPickingExit();
				pickingCacheObject = null;
			}
	}
	
	protected virtual Ray GetRay()
	{
		return new Ray(Camera.main.transform.position, Camera.main.transform.forward);
	}

	public virtual void CameraFadeIn() {
		iTween.CameraFadeAdd();
		iTween.CameraFadeTo(iTween.Hash("amount", 1.0, "time", 1.0, "oncomplete", "DoTeleport", "oncompletetarget", this.gameObject));
	}
	
	public virtual void CameraFadeOut() {
		
		iTween.CameraFadeTo(iTween.Hash("amount", 0.0, "time", 1.0));	
	}

}
