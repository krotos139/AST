using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLighterStart : MonoBehaviour {

    public float xShift = 0.02f;
    private bool gameOver;
    // Use this for initialization
    void Start () {
        gameOver = false;

    }
	
	// Update is called once per frame
	void Update () {
        Vector2 shift;
        if (!gameOver)
        {
            shift.x = -xShift;
            shift.y = 0.0f;
            transform.Translate(shift);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("EventLighter OnTriggerEnter2D obj="+col.gameObject.name.ToString()+" this="+this.name.ToString());
        EventLighter eventl = FindObjectOfType<EventLighter>();
        eventl.EventStart();
    }

    public void SetPosition(float x, float y, float z)
    {
        //transform.position = new Vector3(x, y, z);
        transform.Translate(x, y, z);
    }
}
