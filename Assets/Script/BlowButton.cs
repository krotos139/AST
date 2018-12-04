using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowButton : MonoBehaviour {
    public InspectorObject inspector;
    public GameObject fire;
    private ParticleSystem fire_ps;
    private int maxParticles;

    public bool down_pressed = false;
    // Use this for initialization
    void Start () {
        fire_ps = fire.GetComponent<ParticleSystem>();

    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnMouseDown()
    {
        down_pressed = true;
        Debug.Log("OnMouseDown");
        if (inspector != null) {
            inspector.ButtonBlowDown();
        }
        var main = fire_ps.main;
        main.maxParticles = 1;

    }

    void OnMouseUp()
    {
        down_pressed = false;
        Debug.Log("OnMouseUp");
        if (inspector != null) {
            inspector.ButtonBlowUp();
        }
        var main = fire_ps.main;
        main.maxParticles = 6;
    }
}
