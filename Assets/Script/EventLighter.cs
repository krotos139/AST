using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLighter : MonoBehaviour {

    public BlowButton blowButton;
    private bool isEnable = false;
    private ParticleSystem fire_ps;
    public GameObject fire;
    // Use this for initialization
    void Start () {
        fire_ps = fire.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (Random.Range(0, 200) < 1)
        //{
        //    EventStart();
        //}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isEnable && col.gameObject.name.Equals("lighter"))
        {
            EventEnd();
        }
    }

    public void EventStart()
    {
        Debug.Log("EventLighter Start");
        //SpriteRenderer blowButton_sr = blowButton.GetComponent<SpriteRenderer>();
        //blowButton_sr.sprite = Resources.Load<Sprite>("elementStone054");
        //blowButton.enabled = false;
        if (!isEnable)
        {
            var main = fire_ps.main;
            main.maxParticles = 0;
            fire_ps.Stop();
            isEnable = true;
            FlyingBall flyingBall = FindObjectOfType<FlyingBall>();
            flyingBall.BlowDownLight();
            InspectorObject inspector = FindObjectOfType<InspectorObject>();
            inspector.setNightNoLight(true);
        }
    }
    private void EventEnd()
    {
        //SpriteRenderer blowButton_sr = blowButton.GetComponent<SpriteRenderer>();
        //blowButton_sr.sprite = Resources.Load<Sprite>("elementStone011");
        //blowButton.enabled = true;
        var main = fire_ps.main;
        main.maxParticles = 6;
        fire_ps.Play();
        isEnable = false;
        FlyingBall flyingBall = FindObjectOfType<FlyingBall>();
        flyingBall.BlowUpLight();
        InspectorObject inspector = FindObjectOfType<InspectorObject>();
        inspector.setNightNoLight(false);
    }
}
