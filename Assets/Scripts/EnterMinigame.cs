using UnityEngine;
using System.Collections;

public class EnterMinigame : MonoBehaviour
{	
	public GameObject mainGame = null;
	public GameObject mainGameReset = null;
	public GameObject moleGame = null;
	public GameObject flowerGame = null;
	
	bool inMinigame = false;
	bool playFirst = true;
	bool moved = false;
	
	void Start() {
		mainGame.SetActive(true);
		//moleGame.SetActive(false);
		flowerGame.SetActive(false);
	}
	
	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.name.Equals("trigger")) {
			GameObject.Find ("Player").GetComponent<GameState>().PlayerVelocityVector *= -1;
			
			moved = true;
			if (playFirst) {
				mainGame.SetActive(false);
				//moleGame.SetActive(true);
				flowerGame.SetActive(false);
				playFirst = false;
			}
			else {
				mainGame.SetActive(false);
				//moleGame.SetActive(false);
				flowerGame.SetActive(true);
				playFirst = true;
			}
			inMinigame = true;
		}
	}
	
	void Update() {
		if(moved && (transform.position - mainGame.transform.position).magnitude > 4)
		{
			gameObject.collider.enabled = true;
			moved = false;
		}
		
		if (Input.GetKeyDown(KeyCode.X)) {
			gameObject.collider.enabled = false;
			if (inMinigame) {
				mainGame.SetActive(true);
				moleGame.SetActive(false);
				flowerGame.SetActive(false);
				inMinigame = false;
				//mainGame.transform.position = mainGameReset.transform.position;
				//mainGame.transform.rotation = mainGameReset.transform.rotation;
			}
		}
	}
}

