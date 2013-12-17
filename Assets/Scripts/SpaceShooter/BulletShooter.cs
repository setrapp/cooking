using System;
using UnityEngine;

public class BulletShooter: MonoBehaviour {

	public int 			 Cooldown = 30;
	public BulletManager BulletManager;
	public int 			 BulletType;
	public int 			 MaxCount = 0;
	public float 		 Scale = 1f;
	public float 		 SpeedOverride = 0f;

	int _currentCooldown = 0;
	int _currentCount;

	public void Start() {
		_currentCooldown = 0;
		_currentCount = 0;
	}

	public virtual void OnLogic(int currentFrame) {
		if(MaxCount > 0 && _currentCount > MaxCount)
			return;

		if(_currentCooldown != 0) {
			--_currentCooldown;
		} else {
			this.OnShoot(currentFrame);

			_currentCooldown = this.Cooldown;
			this._currentCount += 1;
		}
	}

	public virtual void OnEnabled() {

	}

	public virtual void OnDisabled() {

	}

	public virtual void OnShoot(int frame) {

	}


};
