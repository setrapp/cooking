using System;
using UnityEngine;

class SpacePlayerController: MonoBehaviour {

	public GameObject Player;
	public BulletManager BulletManager;
	public float Speed = 5.0f;
	public float DashSpeed = 8.0f;
	public float DashTimeout = 0.8f;
	public float PlayerHalfWidth = 1.0f;
	public float PlayerHalfHeight = 0.5f;
	public int   ShootCooldown = 20;

	float _dashResetTime = 0.0f;
	float _currentSpeed;
	int   _currentShootCooldown;
	ParticleSystem _particle;

	public static SpacePlayerController Instance;

	public void Start() {
		Player = this.gameObject;
		_currentSpeed = this.Speed;

		Player.animation["default_T"].wrapMode = WrapMode.Loop;
		_particle = Player.GetComponentInChildren<ParticleSystem>();
		BulletManager = this.GetComponentInChildren<BulletManager>();
	
		Instance = this;
	}

	public void Update() {
		this.CheckDash();
		this.CheckMove();
		this.CheckAttack();
	}

	public void CheckDash() {
		if(_dashResetTime > 0.0f) {
			_dashResetTime -= Time.fixedDeltaTime;
			if(_dashResetTime <= 0.0f) {
				this._currentSpeed = Speed;
				_dashResetTime = 0.0f;
				
				_particle.startSize = 1;
				_particle.startSpeed = 10;
				this.ShootCooldown *= 2;
			}
		}

		if(Input.GetKeyDown(KeyCode.C)) {
			this._dashResetTime = this.DashTimeout;
			this._currentSpeed = this.DashSpeed;

			_particle.startSize = 3;
			_particle.startSpeed = 20;
			this.ShootCooldown /= 2;
		}

	}

	public void CheckMove() {
		Vector3 prevPos = Player.transform.position;
		float rspeed = Time.fixedDeltaTime * _currentSpeed;
		float deltaX = 0.0f;
		float deltaY = 0.0f;
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			deltaX = -rspeed;
		}
		else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			deltaX = rspeed;
		}
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			deltaY = rspeed;
		}
		else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			deltaY = -rspeed;
		}
		if(deltaX != 0.0f && deltaY != 0.0f) {
			float rt = (float)(Math.Sqrt(2) / 2 * rspeed);
			deltaX = rt * Math.Sign(deltaX);
			deltaY = rt * Math.Sign(deltaY);
		}
		Vector3 cameraPos = Camera.main.transform.position;
		float cameraOrthoSize = Camera.main.orthographicSize;
		
		float newX = MathUtil.Clamp(cameraPos.x - cameraOrthoSize + PlayerHalfWidth, 
		                            cameraPos.x + cameraOrthoSize - PlayerHalfWidth, 
		                            prevPos.x + deltaX);
		float newY = MathUtil.Clamp(cameraPos.y - cameraOrthoSize - PlayerHalfHeight, 
		                            cameraPos.y + cameraOrthoSize - PlayerHalfHeight, 
		                            prevPos.y + deltaY);

		
		Player.transform.position = new Vector3(newX, newY, prevPos.z);
	}

	public void CheckAttack() {
		if(_currentShootCooldown == 0) {
			if(Input.GetKey(KeyCode.Z)) {
				Vector3 pos = this.transform.position;
				BulletManager.ShootBullet(new Vector3(pos.x - 0.8f, pos.y + 1.0f, pos.z), 0);
				BulletManager.ShootBullet(new Vector3(pos.x + 0.8f, pos.y + 1.0f, pos.z), 0);
				BulletManager.ShootBullet(new Vector3(pos.x, pos.y + 1.5f, pos.z), 1);
				
				_currentShootCooldown = this.ShootCooldown;
			}
		} else {
			--_currentShootCooldown;
		}
	}

};