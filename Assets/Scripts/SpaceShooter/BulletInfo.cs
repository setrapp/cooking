using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BulletInfo {
	public float  Speed = 3;
	public float  Accel = 0.1f;
	public int    Damage = 1;
	//public Sprite BulletSprite;
};

public class Bullet: MonoBehaviour {

	public BulletInfo Info;
	public bool       Died;

	public void Start() {
		this.Died = false;
	}
};
