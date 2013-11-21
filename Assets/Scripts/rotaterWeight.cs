using UnityEngine;
using System.Collections;

public class rotaterWeight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			gameObject.transform.Rotate(90,0,0);
		}else if(Input.GetMouseButtonUp(0)){
			gameObject.transform.Rotate(-90,0,0);
		}
	
	}
}
