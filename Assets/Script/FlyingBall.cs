using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBall : MonoBehaviour {
    public float mass;
    public float startMass;
    public float  liftingForce;
    public float liftingPower;
    public float basketReScaleFactor;

    private float dt;
    private float vel_y;
    private Vector2 pos;
    private bool gameOver;
    private bool deflate;
    private bool deflateLight;
    private float ballVolume;

    
    

    private Rigidbody2D rigidBody1;
    // Use this for initialization
    void Start () {
        rigidBody1 = GetComponent<Rigidbody2D>();
        pos.x = 5.0f;
        pos.y = 0.0f;
        vel_y = 0.0f;
        dt = 0.1f;
        
        gameOver = false;
        deflate = false;
        deflateLight = false;
        ballVolume = 1.0f;

        //mass calclualtion
        mass = 160.0f;
        var objects = FindObjectsOfType<MouseDragAndDrop>();
        foreach (MouseDragAndDrop ob in objects)
        {
            Debug.Log(ob.name);
            mass += ob.rigidBody1.mass;
        }
        basketReScaleFactor = 35.0f;
        startMass = mass;
    }

    public void dropObject(float dmass)
    {
        mass -= dmass;
    }



    // Update is called once per frame
    void Update()
    {
        if (!gameOver) {
            float randomFluct = (Mathf.Sin(Random.Range(0.3f, 2.0f) * Time.fixedTime) + Mathf.Sin(Time.fixedTime*1.5f) + Mathf.Sin(Time.fixedTime * 0.3f))*0.1f; //magic fluctiations
            liftingPower = (20.0f  - pos.y + randomFluct) * startMass * 0.5f;
            liftingPower *= ballVolume;
            if(deflate)
            {
                ballVolume *= 0.998f;
            }
            if (deflateLight)
            {
                ballVolume *= 0.999f;
            }
            else
            {
                if (ballVolume < 1.0f)
                {
                    ballVolume *= 1.00025f;
                }
            }

            rigidBody1.transform.position = pos;
            pos.y += vel_y * dt;
            liftingForce = liftingPower - 9.81f * mass;
            vel_y += (liftingForce / mass) * dt;
            vel_y *= 0.95f;

            var objects = FindObjectsOfType<MouseDragAndDrop>();

            foreach (MouseDragAndDrop ob in objects)
            {
                if (!ob.dropped)
                {
                    Vector2 accel;
                    accel.x = 0.0f;
                    accel.y = liftingForce / mass; //inertion acceleration
                    // because basket coordinates is in another scale
                    ob.rigidBody1.AddForce(-basketReScaleFactor * accel * ob.rigidBody1.mass); // inertion to objects in the basket
                }
                
            }
        }
    }

    // Update is called once per frame
    public void BlowUp()
    {
        deflate = false;
    }
    public void BlowDown()
    {
        deflate = true;
    }

    // Update is called once per frame
    public void BlowUpLight()
    {
        deflateLight = false;
    }
    public void BlowDownLight()
    {
        deflateLight = true;
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

    public Vector2 GetPos()
    {
        return rigidBody1.transform.position;
    }
}
