using System;
using UnityEngine;

public class BaseSpaceObject: MonoBehaviour {

	public int CurrentFrame;
	public bool Activated;

	public void Start() {
		CurrentFrame = 0;
		Activated = false;
	}

	public void Update() {
		if(Activated) {
			CurrentFrame++;
			this.OnLogic(CurrentFrame);

			if(!Scroller.CameraRect.Contains(this.gameObject.transform.position)) {
				this.Activated = false;
				this.OnDisabled();
			}
		}
		else {
			if(Scroller.CameraRect.Contains(this.gameObject.transform.position)) {
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
