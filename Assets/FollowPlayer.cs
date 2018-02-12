using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    GameObject player;

    //[Range(0f, 100f)]   [SerializeField] float smoothSpeed = 10f;
    [SerializeField] Vector3 cameraOffset;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] float smoothTime = 0.3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	// LateUpdate to make sure player movement is handled first and when it fully processes the camera should be updated.
    // This should prevent jittery movement of the camera
	void FixedUpdate () {

        Vector3 targetPosition = player.transform.position;// + cameraOffset;
        //Vector3 smoothedPosition = Vector3.Lerp(player.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        //Vector3 smoothedPosition = Vector3.SmoothDamp(player.transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = Vector3.SmoothDamp(player.transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = transform.position + cameraOffset;
	}
}
