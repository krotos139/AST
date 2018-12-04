using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWater : MonoBehaviour {

    private float xh;
    private float yh;
    // Use this for initialization
    void Start () {
        xh = Random.Range(0.0f, 100.0f);
        yh = Random.Range(0.0f, 100.0f);
    }
    

    // Update is called once per frame
    void Update () {
        Transform tr = GetComponent<Transform>();
        Vector3 pos = tr.position;
        pos.y += Mathf.Sin(0.6f * Time.fixedTime+yh) * 0.005f;
        pos.x += Mathf.Sin(0.8f * Time.fixedTime+xh) * 0.01f;
        pos.y += 0.3f*Mathf.Sin(0.6f * Time.fixedTime+yh*2.0f) * 0.005f;
        pos.x += 0.3f * Mathf.Sin(0.8f * Time.fixedTime + xh * 2.0f) * 0.01f;
        tr.position = pos;
    }
}
