using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragAndDrop : MonoBehaviour {
    public bool dragging = false;
    public float distance;
    public float curDistanceSqr;
    public float maxAccel;
    public float kForce;
    public float maxMouseDistanceSqr = 15.0f;
    public Rigidbody2D rigidBody1;
    private Vector3 prevPos;
    private GameObject collider1;
    public bool dropped;
    private bool smartDrag;
    public bool isTentacled;

    private Vector3 screenSpace;
    private Vector3 startShift;
    private Quaternion startQuat;

    void Start()
    {
        smartDrag = true;
        dropped = false;
        rigidBody1 = GetComponent<Rigidbody2D>();
        collider1 = GameObject.Find("Collider1");
        maxAccel = 2000.0f;
        kForce = 1000.0f;
        isTentacled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Collider1")
        {
            dragging = false;
        }
    }

    void OnMouseDown()
    {

        Debug.Log("Catched");
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        prevPos = transform.position;
        
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        startShift = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)) - transform.position;
        startQuat = transform.rotation;
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void OnMouseDrag()
    {

        /*
        //keep track of the mouse position
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

        //convert the screen mouse position to world point and adjust with offset
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        //update the position of the object in the world
        transform.position = curPosition;
        */
    }

    void Update()
    {
        if(rigidBody1 == null)
        {
            rigidBody1 = GetComponent<Rigidbody2D>();
        }
        if (dragging)
        {

            if (!smartDrag)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 rayPoint = ray.GetPoint(distance);
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 unitPos = transform.position;
                Vector2 toMouseVector = mousePos - unitPos;//rayPoint - transform.position;
                Vector2 force = kForce * toMouseVector * rigidBody1.mass;
                float curAccelMagnitude = Mathf.Sqrt(force.SqrMagnitude()) / rigidBody1.mass;

                Debug.Log(curAccelMagnitude);
                if (curAccelMagnitude > maxAccel)
                {
                    force *= maxAccel / curAccelMagnitude;
                }
                rigidBody1.velocity *= 0.0f;
                curDistanceSqr = toMouseVector.sqrMagnitude;

                rigidBody1.AddForce(force);                
                
            }
            else
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 unitPos = transform.position;
                Quaternion delta = Quaternion.Lerp(transform.rotation, startQuat, 1);
                
                Vector3 relativeDragPosition = transform.rotation * (Quaternion.Inverse(startQuat) * startShift);
                //Vector3 relativeDragPosition = Quaternion.Inverse(transform.rotation) * (startQuat * startShift);
                Vector2 absoluteDragPosition = unitPos;

                absoluteDragPosition.x += relativeDragPosition.x;
                absoluteDragPosition.y += relativeDragPosition.y;


                Vector2 toMouseVector = mousePos - absoluteDragPosition;//rayPoint - transform.position;
                Vector2 force = kForce * toMouseVector * rigidBody1.mass;
                float curAccelMagnitude = Mathf.Sqrt(force.SqrMagnitude()) / rigidBody1.mass;

                if (curAccelMagnitude > maxAccel)
                {
                    force *= maxAccel / curAccelMagnitude;
                }
                rigidBody1.velocity *= 0.0f;
                curDistanceSqr = toMouseVector.sqrMagnitude;

                rigidBody1.AddForceAtPosition(force, absoluteDragPosition);

            }
            //}

        }
    }
}
