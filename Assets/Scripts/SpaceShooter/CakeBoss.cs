using System;
using UnityEngine;


class CakeBoss: Enemy {

	public override void OnLogic(int currentFrame) {
		base.OnLogic(currentFrame);
	}
	
	public override void OnEnabled() {
		base.OnEnabled();
		Scroller.Instance.OtherScrollObjects.Add (this.gameObject);

		HUDLayer.Instance.ShowScreenText("Eat Eat Eat!!!", 0.5f, 2.0f, 0.5f, TextAnchor.UpperLeft);
	}
	
	public override void OnDisabled() {
		base.OnDisabled();
		Scroller.Instance.OtherScrollObjects.Remove (this.gameObject);
	}

	public override void OnDied() {
		base.OnDied();
		Scroller.Instance.OtherScrollObjects.Remove (this.gameObject);

		HUDLayer.Instance.ShowScreenText("Thank you!!!!Press X to exit", 0.5f, 99999f, 0.5f, TextAnchor.UpperCenter);
	}

};
