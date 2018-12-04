using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour {
    public bool on;
    public GameObject fire;
    private ParticleSystem fire_ps;
    // Use this for initialization
    void Start () {
        on = false;
        fire_ps = fire.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        MouseDragAndDrop drag = GetComponent<MouseDragAndDrop>();

        
        if (drag.dragging != on)
        {
            on = drag.dragging;
            if (on)
            {
                LighterOn();
            }
            else
            {
                LighterOff();
            }
        }
    }

    private void LighterOff()
    {
        fire_ps.Stop();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("lighter_off");

    }
    private void LighterOn()
    {
        fire_ps.Play();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("lighter_on");
    }
}
