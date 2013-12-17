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
	public float BarralRotationSpeed = 8f;
	public int   ShootCooldown = 20;
	public int   MaxHealth = 3;
	public int   CurrentHealth = 3;
	public GUIStyle HUDGuiStyle;

	public AudioSource ShootingSound;

	float _dashResetTime = 0.0f;
	float _currentSpeed;
	float _rotateY = 180f;
	int   _currentShootCooldown;
	ParticleSystem _particle;

	public static SpacePlayerController Instance;

	public void Start() {
		Instance = this;

		Player = this.gameObject;
		_currentSpeed = this.Speed;

		Player.animation["default_T"].wrapMode = WrapMode.Loop;
		_particle = Player.GetComponentInChildren<ParticleSystem>();
		BulletManager = this.GetComponentInChildren<BulletManager>();
	
		CurrentHealth = MaxHealth;
	}

	public void Update() {
		if(Scroller.Paused)
			return;

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
		float rspeed = Time.deltaTime * _currentSpeed;
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
		
		float newX = MathUtil.Clamp(cameraPos.x - cameraOrthoSize * Camera.main.aspect + PlayerHalfWidth, 
		                            cameraPos.x + cameraOrthoSize * Camera.main.aspect - PlayerHalfWidth, 
		                            prevPos.x + deltaX);
		float newY = MathUtil.Clamp(cameraPos.y - cameraOrthoSize - PlayerHalfHeight, 
		                            cameraPos.y + cameraOrthoSize - PlayerHalfHeight, 
		                            prevPos.y + deltaY);
		if(deltaX != 0)	{
			_rotateY += BarralRotationSpeed * Mathf.Sign(deltaX);
			this.gameObject.transform.rotation = Quaternion.AngleAxis(_rotateY, Vector3.up);
		}

		Player.transform.position = new Vector3(newX, newY, prevPos.z);
	}

	public void CheckAttack() {
		if(_currentShootCooldown == 0) {
			if(Input.GetKey(KeyCode.Z)) {
				Vector3 pos = this.transform.position;
				Color color = new Color(1, 1, 1, 0.7f);

				float dx = 0.7f * Mathf.Cos(_rotateY * Mathf.Deg2Rad);
				float dz = 0.7f * Mathf.Sin(_rotateY * Mathf.Deg2Rad);
				BulletManager.ShootBullet(new Vector3(pos.x - dx, pos.y + 0.9f, pos.z + dz), color, 0, 90, 1, "PB");
				BulletManager.ShootBullet(new Vector3(pos.x + dx, pos.y + 0.9f, pos.z + dz), color, 0, 90, 1, "PB");
				BulletManager.ShootBullet(new Vector3(pos.x, pos.y + 1.5f, pos.z), color, 1, 90, 1, "PB");

				if(ShootingSound != null)
					ShootingSound.Play();

				_currentShootCooldown = this.ShootCooldown;
			}
		} else {
			--_currentShootCooldown;
		}
	}

	public void OnTriggerEnter2D(Collider2D col) {
		GameObject obj = col.gameObject;
		if(obj.tag != "EB")
			return;
		
		Bullet bullet = obj.GetComponent<Bullet>();
		if(bullet != null) {
			this.CurrentHealth -= 1;
			if(this.CurrentHealth <= 0) {
				this.OnDied();
			}
			bullet.Died = true;
		}
	}

	void OnDied() {
		HUDLayer.Instance.ShowScreenText("Mission Failed!", 1.0f, 999f, 1.0f, TextAnchor.UpperCenter);
		Scroller.Paused = true;
	}

	public void OnGUI() {
		GUI.Label(new Rect(0, Screen.height - 30, 600, 60), "Health: " + this.CurrentHealth.ToString() + "/" + this.MaxHealth.ToString(), HUDGuiStyle);
	}

};