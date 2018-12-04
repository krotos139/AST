using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {


    private SpriteRenderer sr;
    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Random.Range(0, 99) <= 10)
        {
            Color tmp = sr.color;
            tmp.a = 1.0f;
            sr.color = tmp;
        } else
        {
            Color tmp = sr.color;
            tmp.a *= 0.3f;
            sr.color = tmp;
        }
    }
}
