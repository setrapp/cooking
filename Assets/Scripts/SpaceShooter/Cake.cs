using System;
using UnityEngine;

public class Cake: Enemy {

	public void Start() {
	}

	public override void OnLogic(int currFrame) {
		Vector3 diff = this.transform.position - SpacePlayerController.Instance.Player.transform.position;
		float angle = (float)Math.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
		this.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		base.OnLogic(currFrame);
	}
};
