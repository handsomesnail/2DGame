﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadExamples : MonoBehaviour {

	public void LoadExample(string level){
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
	}
}
