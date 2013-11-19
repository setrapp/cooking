using UnityEngine;
using System.Collections;

public class Flowerscript : MonoBehaviour {
	
	public bool touched;
	//public bool grabbed;
	
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 objectsrotation;
	

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
	
	void OnMouseDown()
{
    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
 
    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
 	
	transform.Rotate(-90, 0, 0);
		
	print("grabbed");
	
}
	
	void OnMouseDrag()
{
Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
 
Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
transform.position = curPosition;
	
}
	
	void OnMouseUp()
	{
		//grabbed = false;
	}
}
