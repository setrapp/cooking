using UnityEngine;
using System.Collections;

public class Flowerscript : MonoBehaviour {
	
	public bool touched;
	//public bool grabbed;
	
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 objectsrotation;
	
	public bool hitwallfor;
	public bool hitwallbac;
	public bool hitwallrig;
	public bool hitwalllef;
	public bool hitwallup;
	public bool hitwalldwn;
	
	public int gravity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		gravity = 1;
		
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Vector3 bck = transform.TransformDirection(Vector3.back);
		Vector3 rgt = transform.TransformDirection(Vector3.right);
		Vector3 lft = transform.TransformDirection(Vector3.left);
		Vector3 up = transform.TransformDirection(Vector3.up);
		Vector3 dwn = transform.TransformDirection(Vector3.down);
		
		
			if(Physics.Raycast(transform.position, fwd, 1))
			{
				print("There is something in front of me");
				hitwallfor = true;
			}
		else
			{
				hitwallfor = false;
			}
		
		if(Physics.Raycast(transform.position, bck, 1))
				{
				print("There is something in front of me");
				hitwallbac = true;
			}
		else
			{
				hitwallbac = false;
			}
		
		if(Physics.Raycast(transform.position, rgt, 1))
				{
				print("There is something in front of me");
				hitwallrig = true;
			}
		else
			{
				hitwallrig = false;
			}
		
		if(Physics.Raycast(transform.position, lft, 1))
				{
				print("There is something in front of me");
				hitwalllef = true;
			}
		else
			{
				hitwalllef = false;
			}
		
			if(Physics.Raycast(transform.position, up, 1))
				{
				print("There is something in front of me");
				hitwallup = true;
			}
		else
			{
				hitwallup = false;
			}
		
		if(Physics.Raycast(transform.position, dwn, 1))
				{
				print("There is something in front of me");
				hitwalldwn = true;
			}
		else
			{
				hitwalldwn = false;
			}
		
		
		
		if(hitwalldwn == !true)
		{
			transform.Translate(Vector3.down * gravity * Time.deltaTime);
			
		}
	
		
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
