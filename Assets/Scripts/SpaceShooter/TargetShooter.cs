using System;
using UnityEngine;

class TargetShooter: BulletShooter {
	public override void OnShoot(int frame) {
		this.BulletManager.ShootBulletTo(this.gameObject.transform.position,
		                                 SpacePlayerController.Instance.Player,
		                                 Color.white,
		                                 this.BulletType,
		                                 this.Scale,
		                                 "EB",
		                                 this.SpeedOverride);
	}
};
