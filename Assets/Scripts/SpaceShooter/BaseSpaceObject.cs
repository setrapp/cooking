using System;
using UnityEngine;

public class BaseSpaceObject: MonoBehaviour {

	public int CurrentFrame;
	public bool Activated;
	public float Height;

	public void Start() {
		CurrentFrame = 0;
		Activated = true;
	}

	public void Update() {
		if(Scroller.Paused)
			return;

		if(Activated) {
			if(!Scroller.CameraRect.Contains(this.gameObject.transform.position)) {
				this.Activated = false;
				this.OnDisabled();
			} else {
				CurrentFrame++;
				this.OnLogic(CurrentFrame);
			}
		}
		else {
			Vector3 pos = this.gameObject.transform.position;
			Vector3 rpos = new Vector3 (pos.x, pos.y + Height / 2, pos.z);

			if(Scroller.CameraRect.Contains(rpos)) {
				this.Activated = true;
				this.OnEnabled();
			}
		}
	}

	public virtual void OnLogic(int currentFrame) {
		// 
	}

	public virtual void OnEnabled() {

	}

	public virtual void OnDisabled() {

	}

};
