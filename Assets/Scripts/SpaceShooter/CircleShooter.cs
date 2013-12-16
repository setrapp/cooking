using System;
using UnityEngine;

class CircleShooter: BulletShooter {
	public int 	 BulletCount;
	public float Radius = 360f;
	public float StartDegree = 0f;
	public float Accel1 = 0f;
	public float Accel2 = 0f;
	public bool  FacingTarget;

	public override void OnShoot(int frame) {
		float start = StartDegree;
		if(FacingTarget) {
			start += MathUtil.GetDgrBetweenObjects(this.gameObject, SpacePlayerController.Instance.Player);
		}
		StartDegree += Accel1;
		Accel1 += Accel2;
		for(int i=0; i<BulletCount; ++i) {
			if(this.BulletManager != null)
				this.BulletManager.ShootBullet(this.transform.position,
				                               Color.white,
				                               this.BulletType,
				                               Radius / BulletCount * i + start,
				                               this.Scale,
			        	                       "EB");
		}
	}
};
