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



	public void Update() {
		List<Bullet> diedBullets = new List<Bullet>();
		foreach(Bullet bullet in this.Bullets) {
			if(!bullet.Died) {
				Vector3 prevPos = bullet.gameObject.transform.position;
				bullet.gameObject.transform.position = new Vector3(prevPos.x, prevPos.y + bullet.Info.Speed * Time.fixedDeltaTime, prevPos.z);
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

	public BulletInfo CreateInfo(BulletInfo prototype) {
		BulletInfo info = new BulletInfo();
		info.Speed = prototype.Speed;
		info.Accel = prototype.Accel;
		info.BulletSprite = prototype.BulletSprite;
		info.Damage = prototype.Damage;
		return info;
	}

	void _CreateBullet(Vector3 position, BulletInfo prototype) {
		GameObject bulletObj = new GameObject();
		Bullet bullet = bulletObj.AddComponent<Bullet>();
		bullet.Info = CreateInfo(prototype);
		
		bullet.transform.position = position;
		
		SpriteRenderer renderer = bulletObj.AddComponent<SpriteRenderer>();
		renderer.sprite = bullet.Info.BulletSprite;
		renderer.sortingOrder = 1;
		
		bulletObj.AddComponent<BoxCollider2D>().isTrigger = true;
		
		Bullets.Add(bullet);
		bullet.transform.parent = BulletContainer.transform;
	}

	public void ShootBullet(Vector3 position, int id) {
		this._CreateBullet(position, this.BulletTypes[id]);

	}

	public void ShootBullet(Vector3 position, BulletInfo prototype) {
		this._CreateBullet(position, prototype);
	}



};
