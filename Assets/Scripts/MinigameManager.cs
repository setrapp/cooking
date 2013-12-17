using UnityEngine;
using System.Collections;

public class MinigameManager : MonoBehaviour
{	
	public static MinigameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Globals").GetComponentInChildren<MinigameManager>();
            }
            return instance;
        }
    }
    public static MinigameManager instance = null;
	
	public GameObject mainGame = null;
	
	bool inMinigame = false;
	bool playFirst = true;
	bool moved = false;
	
	void Start() {
		mainGame.SetActive(true);
	}
	
	/*void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.name.Equals("trigger")) {
			GameObject.Find("Player").GetComponent<GameState>().PlayerVelocityVector *= -1;
			
			moved = true;
			if (playFirst) {
				mainGame.SetActive(false);
				Application.LoadLevel("SpaceShooter!");
			}
			else {
				mainGame.SetActive(false);
			}
			inMinigame = true;
		}
	}*/
	
	void Update() {
		if (!inMinigame && GameObject.FindGameObjectWithTag("Minigame") != null) {
			inMinigame = true;
			mainGame.SetActive(false);
		}
		
		if (Input.GetKeyDown(KeyCode.X)) {
			if (inMinigame) {
				mainGame.SetActive(true);
				inMinigame = false;
				GameObject[] minigames = GameObject.FindGameObjectsWithTag("Minigame");
				for (int i = 0; i < minigames.Length; i++) {
					GameObject minigame = minigames[i];
					minigames[i] = null;
					Destroy(minigame);
				}
			}
		}
	}
	
	public void EnterMinigame(string scene) {
		Application.LoadLevel(scene);
	}
}

