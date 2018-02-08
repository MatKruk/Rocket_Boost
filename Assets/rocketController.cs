using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketController : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource boosterSource;

    [Range(0.0f, 256.0f)]
    [SerializeField] float rcsThrust = 100.0f;
    [Range(0.0f, 256.0f)]
    [SerializeField] float mainThrust = 100.0f;
    // Use this for initialization
    void Start () {

        rigidBody = GetComponent<Rigidbody>();
        boosterSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        ProcessInput();
	}

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly collision");
                break;
            case "Fuel":
                print("Fuel collision");
                //todo Increase fuel level
                break;
            case "Finish":
                print("Finish");
                //todo finish game loop
                break;
            default:
                print("Death collision");
                //todo Kill player
                break;
        }
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);

            if (!boosterSource.isPlaying) // stop audio layering
            {
                boosterSource.Play();
            }
        }
        else
        {
            boosterSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual rotation control.

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Accept rotational input alongside main thruster input.
        {
            
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {

            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        rigidBody.freezeRotation = false; // resume physics rotation control.
    }
}
