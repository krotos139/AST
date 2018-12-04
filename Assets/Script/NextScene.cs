using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour {

    public string nextScene;
    public float timeOut = 2.0f;
    private float nextTimeOut;
	// Use this for initialization
	void Start () {
        nextTimeOut = Time.fixedTime + timeOut;

    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("NextScene:"+ Time.fixedTime);
		if (nextTimeOut < Time.fixedTime)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
	}
}
