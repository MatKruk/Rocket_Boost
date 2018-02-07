using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {

        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            print("Thrust");
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Accept rotational input alongside main thruster input.
        {
            print("left rotation");
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            print("right rotation");
        }
    }
}
