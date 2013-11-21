using UnityEngine;
using System.Collections;
using System.Linq;
public class FoodCollection : MonoBehaviour {
	
	public GameObject player;
	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		var tables = GameObject.FindGameObjectsWithTag("Table");
        if(Input.GetKey(KeyCode.F))
		foreach(var table in tables)
		{
            if (Vector3.Distance(table.transform.position, player.transform.position) < 20)
            {
                var fruits =  GameObject.FindGameObjectsWithTag("Fruits").ToList();
                int i = fruits.Count() - 1;
                bool condition = true;
                while (condition)
                {
                    var t = fruits[i];
                    if (Vector3.Distance(t.transform.position, player.transform.position) < 20)
                    {
                        condition = true;
                        t.SetActive(false);
                        t.renderer.active = false;
                        break;
                    }
                    else
                    {
                        i--;
                    }
                }
                
            }
		}
	}
}
