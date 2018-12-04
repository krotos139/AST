using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCone : MonoBehaviour {

    public GameObject basket;
    private PolygonCollider2D collider;

    // Use this for initialization
    void Start () {
        collider = GetComponent<PolygonCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
    
    }

    void OnMouseEnter()
    {
        Debug.Log("MouseCone.OnMouseEnter");
        MouseDragAndDrop[] allChildren = basket.GetComponentsInChildren<MouseDragAndDrop>();
        foreach (MouseDragAndDrop child in allChildren)
        {
            child.dragging = false;
            // do what you want with the transform
        }
    }
}
