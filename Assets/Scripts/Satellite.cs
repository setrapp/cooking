using UnityEngine;
using System.Collections;

public class Satellite : MonoBehaviour {

	public float rotSpeed;
	public float satSpeed;

	// Use this for initialization
	void Start () {
		rotSpeed = 100f;
		satSpeed = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
		transform.Translate(Vector3.up * satSpeed * Time.deltaTime);
	}
}