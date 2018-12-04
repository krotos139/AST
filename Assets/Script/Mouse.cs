using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {
    public GameObject basket;
	// Use this for initialization
	void Start () {
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("MouseCone.OnMouseEnter");
        MouseDragAndDrop[] allChildren = basket.GetComponentsInChildren<MouseDragAndDrop>();
        foreach (MouseDragAndDrop child in allChildren)
        {
            child.dragging = false;
            // do what you want with the transform
        }
    }

    // Update is called once per frame
    void Update () {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
