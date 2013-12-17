using UnityEngine;
using System.Collections;

public class MinigameTrigger : MonoBehaviour
{
	public string minigameScene = null;
	public CollisionChecker collisionChecker = null;
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.M) && collisionChecker.Triggering) {
			MinigameManager.Instance.EnterMinigame(minigameScene);
		}
	}
}

