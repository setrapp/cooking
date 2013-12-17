using UnityEngine;
using System.Collections;

public class GlobalsDependent : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		if (GameObject.FindGameObjectWithTag("Globals") == null) {
			Application.LoadLevel("mainmenu");	
			Destroy(GameObject.FindGameObjectWithTag("MainGame"));
		}
	}
}

