using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    Vector3 z;
    float rotAngle;
	// Use this for initialization
	void Start () {
        rotAngle = 90.0f;
        z.x = 0.0f;
        z.y = 0.0f;
        z.z = 1.0f;
        transform.Rotate(z, rotAngle);
	}
	
	// Update is called once per frame
	void Update () {
        BlowButton button = GetComponentInChildren<BlowButton>();
        if (button.down_pressed)
        {
            if(rotAngle > 0.0f)
            {
                rotAngle -= 1.0f;
                transform.Rotate(z, -1.0f);
            }
        }
        else
        {
            if (rotAngle < 90.0f)
            {
                rotAngle += 1.0f;
                transform.Rotate(z, 1.0f);
            }

        }
    }

}
