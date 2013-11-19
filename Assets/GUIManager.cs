using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
    public static string message = string.Empty;
    public static bool isActive = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (isActive)
        {
            GUI.Label(new Rect((Screen.width * 0.5f) - 75, 75, 150, 100), message);
        }
    }
}
