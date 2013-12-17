using System;
using UnityEngine;

class StartMenu: MonoBehaviour {

	public void OnStartClick() {
		Application.LoadLevel("main");
	}

	public void OnExitClick() {
		Application.Quit();
	}

};
