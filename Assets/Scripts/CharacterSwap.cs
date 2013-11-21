using UnityEngine;
using System.Collections;

public class CharacterSwap : MonoBehaviour
{
	public GameObject relativisticCharacter = null;
	public GameObject nonrelativisticCharacter = null;
	
	void Start () {
		nonrelativisticCharacter.SetActive(true);
		relativisticCharacter.SetActive(false);
	}
	
	public void SetCharacterType(bool relativistic) {
		nonrelativisticCharacter.SetActive(!relativistic);
		relativisticCharacter.SetActive(relativistic);
	}
}

