using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour {

    public float Yspeed = 0.1f;
    private bool isUp;
    private bool isDown;
    private float startYOffset;
    private float yOffset;
    private GameObject obj;
    public bool isFree;

    // Use this for initialization
    void Start () {
        yOffset = 0.0f;
        MoveY(-20.0f);
        isUp = false;
        isDown = false;
        obj = null;
        Transform tr = GetComponent<Transform>();
        startYOffset = tr.position.y;
        isFree = true;
    }

    private void MoveYP(float off)
    {
        Transform tr = GetComponent<Transform>();
        Vector3 pos = tr.position;
        yOffset += (off - (pos.y - startYOffset)) * 0.05f;
        pos.y += (off- (pos.y- startYOffset))*0.05f;
        tr.position = pos;
    }
    private void MoveY(float off)
    {
        Transform tr = GetComponent<Transform>();
        Vector3 pos = tr.position;
        pos.y += off;
        tr.position = pos;
    }
    private void ObjMoveY(float off)
    {
        if (obj == null) return;
        Transform tr = obj.GetComponent<Transform>();
        Vector3 pos = tr.position;
        pos.y += off;
        tr.position = pos;
    }
    private void DestroyObj()
    {
        if (obj == null) return;
        Transform tr = obj.GetComponent<Transform>();
        Destroy(obj);
        isFree = true;
    }

    public void Activate()
    {
        isUp = true;
        isFree = false;
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("yOffset="+yOffset);
		if (isUp)
        {
            //yOffset += Yspeed;
            //MoveY(Yspeed);
            MoveYP(20.0f);
            if (yOffset >= 17.0f)
            {
                isUp = false;
            }
        }
        if (isDown)
        {
            yOffset -= Yspeed;
            MoveY(-Yspeed);
            ObjMoveY(-Yspeed);
            if (yOffset <= -10.0f)
            {
                isDown = false;
                DestroyObj();
            }
        }
        MoveY(Mathf.Sin(0.8f*Time.fixedTime)*0.02f);
        //ObjMoveY(Mathf.Sin(0.1f * Time.fixedTime));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Tentacle " + col.gameObject.name.ToString());
        MouseDragAndDrop col_mdnd = col.gameObject.GetComponent<MouseDragAndDrop>();
        if (!col_mdnd.isTentacled) {
            col_mdnd.isTentacled = true;
            Rigidbody2D col_rb = col.gameObject.GetComponent<Rigidbody2D>();
            col_rb.gravityScale = 0;
            col_rb.velocity *= 0.0f;
            col_rb.rotation *= 0.0f;

            obj = col.gameObject;
            isUp = false;
            isDown = true;

            Boss boss = FindObjectOfType<Boss>();
            boss.DeleteTentacle();
        }
    }
}
