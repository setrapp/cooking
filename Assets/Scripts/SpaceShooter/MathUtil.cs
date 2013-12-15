using System;
using UnityEngine;

class MathUtil {
	
	public static float Clamp(float start, float end, float v) {
		if(v < start)
			v = start;
		else if(v > end)
			v = end;
		return v;
	}


}