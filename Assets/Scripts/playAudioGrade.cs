using UnityEngine;
using System.Collections;

public class playAudioGrade : MonoBehaviour {

	public int gradeNumber;

	// Use this for initialization
	void Start () {
		gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PlayAudio(int grade) {

		if (grade == gradeNumber) 
		{
		gameObject.audio.Play();
		}

	}
}
