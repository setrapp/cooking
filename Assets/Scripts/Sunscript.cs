using UnityEngine;
using System.Collections;

public class Sunscript : MonoBehaviour {

	public float sunSpeed;

	// Use this for initialization
	void Start () {

		sunSpeed = 1f;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Translate(Vector3.up * sunSpeed * Time.deltaTime);
	}
}
