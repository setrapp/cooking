using UnityEngine;
using System.Collections;

public class OmeletteScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void Initialize()
    {
        //GameObject[] internals = GameObject.Find("Frying pan").GetComponentsInChildren<GameObject>();
        foreach(var obj in internals)
        {
            
        }
        GameObject.Find("Frying pan").SetActive(true);
        GameObject.Find("Frying pan").renderer.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
