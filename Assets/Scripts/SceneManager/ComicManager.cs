using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicManager : MonoBehaviour {

    private void Awake() {
        StartCoroutine(SceneManager.Instance.LoadSceneAsync("Zone1 - 副本"));
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
