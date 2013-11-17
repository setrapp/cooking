using UnityEngine;
using System.Collections;

public class FoodCollection : MonoBehaviour {
	
	public GameObject player;
	// Use this for initialization
	void Start () {
		var tables = GameObject.FindAllWithTags("Table");
		foreach(var table in tables)
		{
			//if(table.transform.position
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
