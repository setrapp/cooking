using System;
using UnityEngine;


class CakeBoss: Enemy {

	public override void OnLogic(int currentFrame) {
		base.OnLogic(currentFrame);
	}
	
	public override void OnEnabled() {
		base.OnEnabled();
		Scroller.Instance.OtherScrollObjects.Add (this.gameObject);
	}
	
	public override void OnDisabled() {
		base.OnDisabled();
		Scroller.Instance.OtherScrollObjects.Remove (this.gameObject);
	}

	public override void OnDied() {
		base.OnDied();
		Scroller.Instance.OtherScrollObjects.Remove (this.gameObject);
	}

};
