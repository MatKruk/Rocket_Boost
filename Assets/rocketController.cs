using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocketController : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;//, successSource, explosionSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;


    [Range(0.0f, 1000.0f)]  [SerializeField] float rcsThrust = 100.0f;

    [Range(0.0f, 1000.0f)]  [SerializeField] float mainThrust = 100.0f;
                            [SerializeField] float levelLoadDelay = 2f;

                            [SerializeField] AudioClip successAudio , thrustAudio, deathAudio;
                            [SerializeField] ParticleSystem successParticles, thrustParticles, deathParticles;

    

    // Use this for initialization
    void Start () {

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

        if (!isTransitioning)
        {
            ProcessInput();
        }
        if (Debug.isDebugBuild)
        {
            ProcessDebugInput();
        }
    }

    private void ProcessDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // progress to next level
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; } // ignore collision triggers when dead

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            #region Fuel
            //Unsure if it'll be a feature
            //case "Fuel":
            //    print("Fuel collision");
            //    //todo Increase fuel level
            //    break; 
            #endregion
            case "Finish":
                StartSuccessSequence();
                //todo add a level finish screen before the next level
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        thrustParticles.Stop();
        audioSource.PlayOneShot(deathAudio);
        deathParticles.Play();
        Invoke("ReloadCurrentLevel", levelLoadDelay);
    }
    #region Level Loading
    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
#endregion

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

            if (!audioSource.isPlaying) // stop audio layering
            {
                audioSource.PlayOneShot(thrustAudio);
            }
            thrustParticles.Play();
        }
        else
        {
            audioSource.Stop();
            thrustParticles.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.angularVelocity = Vector3.zero; // remove rotations applied by engines physics.

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Accept rotational input alongside main thruster input.
        {
            
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {

            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        
    }
}
