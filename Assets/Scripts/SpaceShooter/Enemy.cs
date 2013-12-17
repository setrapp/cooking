using System;
using UnityEngine;

public class Enemy: BaseSpaceObject {
	
	public int MaxHealth;
	public int CurrentHealth;
	public bool Died;

	public AudioSource ExplosionSound;

	int _damagedCooldown = 0;
	
	public void Start() {
		CurrentHealth = MaxHealth;
		Died = false;

		foreach(BulletShooter shooter in this.GetComponents<BulletShooter>()) {
			shooter.enabled = false;
		}
	}
	
	public override void OnLogic(int currentFrame) {
		if(Died || Scroller.Paused)
			return;

		if(_damagedCooldown > 0) {
			_damagedCooldown--;
			//if(_damagedCooldown == 0)
			//	this.GetComponent<SpriteRenderer>().color = Color.white;
		}

		foreach(BulletShooter shooter in this.GetComponents<BulletShooter>()) {
			shooter.OnLogic(currentFrame);
		}
	}
	
	public override void OnEnabled() {
		foreach(BulletShooter shooter in this.GetComponents<BulletShooter>()) {
			shooter.enabled = true;
			shooter.OnEnabled();
		}
	}
	
	public override void OnDisabled() {
		foreach(BulletShooter shooter in this.GetComponents<BulletShooter>()) {
			shooter.OnDisabled();
		}
	}
	
	/*public void OnTriggerEnter2D(Collider2D col) {
		if(Died)
=======
	public void OnTriggerEnter2D(Collider2D col) {
		if(Died || !Activated || Scroller.Paused)
>>>>>>> master
			return;

		GameObject obj = col.gameObject;
		if(obj.tag != "PB")
			return;

		Bullet bullet = obj.GetComponent<Bullet>();
		if(bullet != null) {
			bullet.Died = true;
			this.CurrentHealth -= bullet.Info.Damage;
			
			this._damagedCooldown = 3;
			SpriteRenderer sprRenderer = this.GetComponent<SpriteRenderer>();
			if(sprRenderer != null)
				sprRenderer.color = new Color(1f, 0.5f, 0);

			if(this.CurrentHealth <= 0) {
				ParticleSystem par = this.gameObject.GetComponentInChildren<ParticleSystem>();
				if(par != null) {
					par.Play();
				}
				this.gameObject.GetComponent<Collider2D>().enabled = false;
				if(ExplosionSound != null)
					ExplosionSound.Play();

				//iTween.FadeTo(this.gameObject, iTween.Hash("amount", 0, "time", 0.5, "oncomplete", "OnDied", "oncompletetarget", this.gameObject));
			}
		}
	}*/

	public virtual void OnDied() {
		Destroy(this.gameObject);
	}
	
	/*public void OnTriggerExit2D(Collider2D col) {
		
	}*/
};
