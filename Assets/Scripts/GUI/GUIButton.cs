using System;
using UnityEngine;

class GUIButton: GUIWidget {

	public string 	ButtonText;
	public TextMesh TextContainer;

	public void Start() {
		TextContainer.text = ButtonText;

		base.Start();
	}

};