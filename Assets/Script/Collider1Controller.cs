using UnityEngine;
using UnityEngine.EventSystems;

public class Collider1Controller : MonoBehaviour
{
    public FlyingBall flyingBall;
   
    void Start()
    {
        flyingBall = FindObjectOfType<FlyingBall>();
        
        if (flyingBall == null)
        {
            Debug.Log("EROR: did not find flyingBall object.");
        }    
            //GameObject.Find("FlyingBall").GetComponent<FlyingBall>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        flyingBall = FindObjectOfType<FlyingBall>();
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        MouseDragAndDrop go = col.gameObject.GetComponent<MouseDragAndDrop>();
        go.dropped = true;
        go.dragging = false;

        if (go.rigidBody1 != null)
        {
            flyingBall.dropObject(go.rigidBody1.mass);
            Debug.Log(go.rigidBody1.mass);
        }
        InspectorObject inspector = FindObjectOfType<InspectorObject>();
        inspector.dropObject(go.rigidBody1.mass);
    }
}
