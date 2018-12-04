using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownObject : MonoBehaviour {

    public FlyingBall ballon;
    public Material fallingObjectMaterial;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Drop object");
        //GameObject newobject = Instantiate<GameObject>(col.gameObject);
        Vector2 ballon_pos = ballon.GetPos();
        Rigidbody2D obj_rigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        float x_pos = obj_rigidbody.transform.position.x;
        float x_pos_rel = x_pos - GetComponentInParent<Transform>().position.x;
        float scaleFactor = 0.1f;

        obj_rigidbody.velocity *= scaleFactor;
        //SpriteRenderer mat = col.gameObject.GetComponent<SpriteRenderer>();
        //mat.material = fallingObjectMaterial;
        obj_rigidbody.transform.position = new Vector3 (ballon_pos.x + 0.25f + x_pos_rel * scaleFactor * 0.5f, ballon_pos.y-2.0f, 0.0f);
        col.gameObject.layer = 10; // Level
        col.gameObject.transform.localScale *= scaleFactor;
        //obj_rigidbody.transform.Translate(ballon_rigidbody.transform.position - obj_rigidbody.transform.position );

        Debug.Log(obj_rigidbody.transform.position.ToString());
        //newobject.transform.Translate();
        //Destroy(col.gameObject);
        
    }
}
