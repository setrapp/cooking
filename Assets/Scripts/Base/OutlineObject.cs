using System;
using UnityEngine;

class OutlineObject: MonoBehaviour {
	public bool highlighted;
	
	public Color outlineColor;
	protected Shader outlineShader;
	protected Shader originalShader;

	public void Start() {
		outlineShader = Shader.Find("Outlined/Silhouetted Diffuse");
		OnStart();
	}
	
	public virtual void OnStart() {
		
	}
	
	public virtual void OnPickingEnter() {
		originalShader = this.gameObject.renderer.material.shader;
		
		this.gameObject.renderer.material.shader = outlineShader;
		outlineColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, 1.0f);
		this.gameObject.renderer.material.SetColor("_OutlineColor", outlineColor);
	    highlighted = true;
	}
	 
	public virtual void OnPickingExit() {
		this.gameObject.renderer.material.shader = originalShader;
	    highlighted = false;
	}

};
