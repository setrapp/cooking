using System;
using UnityEngine;

public class Enemy: BaseSpaceObject {
	
	public int MaxHealth;
	public int CurrentHealth;
	public bool Died;

	int _damagedCooldown = 0;
	
	public void Start() {
		CurrentHealth = MaxHealth;
		Died = false;
	}
	
	public override void OnLogic(int currentFrame) {
		if(Died)
			return;

		if(_damagedCooldown > 0) {
			_damagedCooldown--;
			//if(_damagedCooldown == 0)
			//	this.GetComponent<SpriteRenderer>().color = Color.white;
		}
	}
	
	public override void OnEnabled() {
		
	}
	
	public override void OnDisabled() {
		
	}
	
	/*public void OnTriggerEnter2D(Collider2D col) {
		if(Died)
			return;

		GameObject obj = col.gameObject;
		Bullet bullet = obj.GetComponent<Bullet>();
		if(bullet != null) {
			bullet.Died = true;
			this.CurrentHealth -= bullet.Info.Damage;
			
			this._damagedCooldown = 3;
			this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0);

			if(this.CurrentHealth <= 0) {
				ParticleSystem par = this.gameObject.GetComponentInChildren<ParticleSystem>();
				if(par != null) {
					par.Play();
				}
			//	this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

				//iTween.FadeTo(this.gameObject, iTween.Hash("amount", 0, "time", 0.5, "oncomplete", "OnDied", "oncompletetarget", this.gameObject));
			}

		}
	}*/

	public void OnDied() {
		Destroy(this.gameObject);
	}
	
	/*public void OnTriggerExit2D(Collider2D col) {
		
	}*/
	
};
