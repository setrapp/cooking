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
	
	public bool  IsEnabled;
	
	public void Start() {
		gameObject.GetComponent<SpriteRenderer>().color = NormalColor;
		IsEnabled = true;
	}
	
	public void OnMouseEnter() {
		if(!IsEnabled)
			return;
		
		IsHover = true;
		gameObject.GetComponent<SpriteRenderer>().color = HoverColor;
		
		if(EventReceiver != null && OnHoverEvent.Length > 0)
			EventReceiver.SendMessage(OnHoverEvent);
	}
	
	public void OnMouseExit() {
		if(!IsEnabled)
			return;
		
		IsHover = false;
		gameObject.GetComponent<SpriteRenderer>().color = NormalColor;
	}
	
	public void OnMouseDown() {
		if(!IsEnabled)
			return;
		
		gameObject.GetComponent<SpriteRenderer>().color = ClickColor;
	}
	
	public void OnMouseUpAsButton() {
		if(!IsEnabled)
			return;
		
		gameObject.GetComponent<SpriteRenderer>().color = IsHover ? HoverColor : NormalColor;
		
		if(EventReceiver != null && OnClickEvent.Length > 0)
			EventReceiver.SendMessage(OnClickEvent);
	}
	
	
	
};