using UnityEngine;
using System.Collections;

public class DontDieOnLoad : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}

