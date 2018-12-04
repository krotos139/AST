using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBorder : MonoBehaviour {

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
        Debug.Log("WIIN "+ col.gameObject.name.ToString());
        InspectorObject inspector = FindObjectOfType<InspectorObject>();
        inspector.GameWin();
    }

    public void SetPosition(float x, float y, float z)
    {
        //transform.position = new Vector3(x, y, z);
        transform.Translate(x, y, z);
    }
}
