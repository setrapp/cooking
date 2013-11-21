using UnityEngine;
using System.Collections;

public class RALPH_Animate_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MainGameEventScheduler.playerPositions.Add(this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
			animation.Play("Walk_fwd");
           
        
        }else if(Input.GetKey("space")){
			animation.Play("default_T");
		}else{
			animation.Play ("default_Stand");
          
		}
        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.F))
        {
            animation.Play("holding_item");
        }
	
	}

    public static void ForceAnimation()
    { 
        GameObject.FindGameObjectWithTag("PlayerMesh").animation.Play("holding_item");
    }
}
