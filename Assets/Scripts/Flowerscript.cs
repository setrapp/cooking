using UnityEngine;
using System.Collections;

public class Flowerscript : MonoBehaviour {
	
	public bool touched;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(touched == true)
		{
			renderer.material.color = Color.blue;
		}
		else if (touched == false)
		{
			renderer.material.color = Color.green;
		}
	
	}
	
	void OnMouseOver ()
	{
		touched = true;
	}
	
	void OnMouseExit ()
	{
		touched = false;
	}
	
}
