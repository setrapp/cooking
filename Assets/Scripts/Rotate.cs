using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float rotSpeed;

	// Use this for initialization
	void Start () {
		rotSpeed = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
	}
}
