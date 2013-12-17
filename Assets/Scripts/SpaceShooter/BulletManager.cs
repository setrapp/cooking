using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager: MonoBehaviour {

	public List<BulletInfo> BulletTypes;
	public List<Bullet> Bullets;
	public GameObject BulletContainer;

	public BulletManager() {

	}

	public void Start() {

	}



	public void LateUpdate() {
		List<Bullet> diedBullets = new List<Bullet>();
		foreach(Bullet bullet in this.Bullets) {
			if(!bullet.Died) {
				Vector3 prevPos = bullet.gameObject.transform.position;
				float dist = bullet.Info.Speed * Time.deltaTime;
				bullet.gameObject.transform.position = new Vector3(prevPos.x + (float)(dist * Math.Cos (bullet.Info.Direction)),
				                                                   prevPos.y + (float)(dist * Math.Sin (bullet.Info.Direction)), 
				                                                   prevPos.z);
				bullet.Info.Speed += bullet.Info.Accel;

				if(!Scroller.CameraRect.Contains(bullet.transform.position)) {
					diedBullets.Add(bullet);
				}
			} else {
				diedBullets.Add(bullet);
			}
		}
		foreach(Bullet bullet in diedBullets) {
			Bullets.Remove(bullet);
			Destroy(bullet.gameObject);
		}
	}

	public BulletInfo CreateInfo(BulletInfo prototype, float dir) {
		BulletInfo info = new BulletInfo();
		info.Speed = prototype.Speed;
		info.Accel = prototype.Accel;
		info.BulletSprite = prototype.BulletSprite;
		info.Damage = prototype.Damage;
		info.Direction = dir * Mathf.Deg2Rad;
		return info;
	}

	Bullet _CreateBullet(Vector3 position, Color color, BulletInfo prototype, float direction, float scale, string tag) {
		GameObject bulletObj = new GameObject();
		Bullet bullet = bulletObj.AddComponent<Bullet>();
		bullet.Info = CreateInfo(prototype, direction);
		bullet.transform.position = position;
		
		SpriteRenderer renderer = bulletObj.AddComponent<SpriteRenderer>();
		renderer.sprite = bullet.Info.BulletSprite;
		renderer.sortingOrder = 1;
		renderer.color = color;

		bulletObj.AddComponent<BoxCollider2D>().isTrigger = true;
		
		Bullets.Add(bullet);
		bullet.transform.parent = BulletContainer.transform;

		scale = prototype.Scale * scale;
		bullet.transform.localScale = new Vector3(scale, scale, scale);
		bullet.transform.rotation = Quaternion.AngleAxis(direction + 90, Vector3.forward);

		bullet.tag = tag;

		return bullet;
	}

	public Bullet ShootBullet(Vector3 position, Color color, int id, float direction, float scale, string tag) {
		return this._CreateBullet(position, color, this.BulletTypes[id], direction, scale, tag);
	}

	public Bullet ShootBullet(Vector3 position, Color color, BulletInfo prototype, float direction, float scale, string tag) {
		return this._CreateBullet(position, color, prototype, direction, scale, tag);
	}

	public Bullet ShootBulletTo(Vector3 position, GameObject target, Color color, int id, float scale, string tag) {
		Vector3 targetPos = target.transform.position;
		Vector3 dis = targetPos - position ;
		float angle = (float)(Math.Atan2(dis.y, dis.x) * Mathf.Rad2Deg);
		return this._CreateBullet(position, color, this.BulletTypes[id], angle, scale, tag);
	}


};
