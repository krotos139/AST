using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {
    private Rigidbody2D rigidBody1;
    public float xShift = 0.02f;
    private bool gameOver;
    // Use this for initialization

    public void Awake()
    {
        rigidBody1 = GetComponent<Rigidbody2D>();
    }
    public void Start () {
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
    /*
    IEnumerator SetPosition(float x, float y, float z)
    {
        yield return WaitForSeconds(1);
        rigidBody1.transform.position.Set(x, y, z);

    }
    */
    public void SetPosition(float x, float y, float z)
    {
        if(rigidBody1 == null)
        {
            Debug.Log("RIGID BODY IS NULL");
        }
        rigidBody1.transform.position = new Vector3(x, y, z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        InspectorObject inspector = FindObjectOfType<InspectorObject>();
        inspector.GameOver();
        
        

    }

    // Вызывается из InspectorObject
    public void GameOver()
    {
        gameOver = true;
    }

    // Вызывается из InspectorObject
    public void GameWin()
    {

    }
}
