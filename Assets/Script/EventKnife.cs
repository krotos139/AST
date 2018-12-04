using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventKnife : MonoBehaviour {

    public BlowButton blowButton;
    private bool isEnable = false;

    // Use this for initialization
    void Start () {
        //EventStart();

    }
	
	// Update is called once per frame
	void Update () {
		if (Random.Range(0, 200) <1)
        {
            //EventStart();
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("WIIN " + col.gameObject.name.ToString());
        if (isEnable && col.gameObject.name.Equals("knife"))
        {
            EventEnd();
        }
    }

    private void EventStart()
    {
        SpriteRenderer blowButton_sr = blowButton.GetComponent<SpriteRenderer>();
        blowButton_sr.sprite = Resources.Load<Sprite>("elementStone054");
        blowButton.enabled = false;
        isEnable = true;
        FlyingBall flyingBall = FindObjectOfType<FlyingBall>();
        flyingBall.BlowDown();
    }
    private void EventEnd()
    {
        SpriteRenderer blowButton_sr = blowButton.GetComponent<SpriteRenderer>();
        blowButton_sr.sprite = Resources.Load<Sprite>("elementStone011");
        blowButton.enabled = true;
        isEnable = false;
        FlyingBall flyingBall = FindObjectOfType<FlyingBall>();
        flyingBall.BlowUp();
    }
}
