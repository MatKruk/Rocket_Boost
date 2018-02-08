using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketController : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource boosterSource;

	// Use this for initialization
	void Start () {

        rigidBody = GetComponent<Rigidbody>();
        boosterSource = GetComponent<AudioSource>();
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

            if (!boosterSource.isPlaying)
            {
                boosterSource.Play();
            } 
        }
        else
        {
            boosterSource.Stop();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Accept rotational input alongside main thruster input.
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            
            transform.Rotate(-Vector3.forward);
        }
    }
}
