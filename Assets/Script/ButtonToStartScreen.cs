using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToStartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToStartScreen2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("start_game");
    }

    public void ToCredits2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("credits");
    }

    public void ToScene1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("scene2");
    }

    public void ToScene2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("scene2");
    }

    public void ToScene3()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("boss");
    }
}
