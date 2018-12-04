using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseUp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("scene2");
    }

    public void LoadScene()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("scene2", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
