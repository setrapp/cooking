using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
class GUIWidget: MonoBehaviour {

	public GameObject EventReceiver;
	public string OnClickEvent;
	public string OnHoverEvent;

	public bool  IsHover;
	public Color NormalColor = Color.white;
	public Color HoverColor = Color.white;
	public Color ClickColor = Color.white;

	public void OnMouseEnter() {
		IsHover = true;
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);

		if(EventReceiver != null && OnHoverEvent.Length > 0)
			EventReceiver.SendMessage(OnHoverEvent);
	}

	public void OnMouseExit() {
		IsHover = false;
		gameObject.GetComponent<SpriteRenderer>().color = NormalColor;
	}


};
