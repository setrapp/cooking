using UnityEngine;
using System.Collections;

public class RelativityMatSwap : MonoBehaviour {
	public MovementScripts mover = null;
	public int nonrelMatIndex = 0;
	public int relMatIndex = 1;
	private int curMatIndex = 0;
	public Material relativisticMat = null;
	private Material nonrelativisticMat = null;
	private bool isRelativistic = false;
	
	void Start () {
		nonrelativisticMat = renderer.materials[0];
	}

	void Update () {
		if (mover != null && relativisticMat != null) {
		/*	if (mover.IsRelativistic && curMatIndex != relMatIndex) {
				renderer.material = renderer.materials[relMatIndex];
				curMatIndex = relMatIndex;
			} else if (!mover.IsRelativistic && curMatIndex != nonrelMatIndex) {
				renderer.material = renderer.materials[nonrelMatIndex];
				curMatIndex = nonrelMatIndex;
			}
		*/
			if (mover.IsRelativistic && !isRelativistic) {
				renderer.material = relativisticMat;
				isRelativistic = true;
			}
			if (!mover.IsRelativistic && isRelativistic) {
				renderer.material = nonrelativisticMat;
				isRelativistic = false;
			}
		}	
	}
}
