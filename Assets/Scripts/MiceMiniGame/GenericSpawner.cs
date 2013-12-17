using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnInfo {
	public float 	Interval;
	public string 	TargetPath;
	public float    MoveTime;
	public float 	StartDelay;
	public int 		MaxCount;
	public GameObject Prototype;

	public float 	CurrentTime;
	public int 		CurrentCount;

};


// spawns object with a MovableObject component
// and follows a predefined iTween move path
class GenericSpawner: MonoBehaviour {

	public GameObject  	   ObjectContainer;
	public List<SpawnInfo> SpawnInfoes = new List<SpawnInfo>();
	public bool			   Stopped = false;

	public void Start() {
		foreach(SpawnInfo info in SpawnInfoes) {
			info.Prototype.SetActive(false);
		}
	}

	public void LateUpdate() {
		foreach(SpawnInfo info in SpawnInfoes) {
			if(info.MaxCount != 0 &&
			   info.CurrentCount >= info.MaxCount) { 
					continue;
				}
			info.CurrentTime += Time.deltaTime;
			if(info.CurrentTime >= info.Interval) {
				info.CurrentTime = 0f;
				++info.CurrentCount;
				this.DoSpawn(info);
			}
		}
	}

	// -1 = all
	public void Reset(int target = -1) {
		if(target == -1) {
			foreach(SpawnInfo info in SpawnInfoes) {
				info.CurrentCount = 0;
			}
		} else {
			SpawnInfoes[target].CurrentCount = 0;
		}
	}

	void DoSpawn(SpawnInfo info) {
		Vector3[] path = iTweenPath.GetPath(info.TargetPath);
		if(path != null) {
			Vector3 startPoint = path[0];
			GameObject newObj = (GameObject)GameObject.Instantiate(info.Prototype);
			newObj.transform.position = startPoint;
			
			MovableObject movePath = newObj.GetComponent<MovableObject>();
			movePath.Delay 			= info.StartDelay;
			movePath.MovePathName 	= info.TargetPath;
			movePath.MoveTime 		= info.MoveTime;
			
			if(ObjectContainer != null)
				newObj.transform.parent = ObjectContainer.transform;

			newObj.SetActive(true);
			movePath.StartMove();
		}
	}

};
