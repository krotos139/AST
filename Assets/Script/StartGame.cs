﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartTheGame()
    {
        CommonSingleton.level_index = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("intro", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
