using UnityEngine;
using System.Collections;

public class Rotatediscoball : MonoBehaviour {

	public float rotSpeed;
	
	// Use this for initialization
	void Start () {
		rotSpeed = 50f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
	}
}
