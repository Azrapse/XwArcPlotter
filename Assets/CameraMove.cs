using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float speed;
    public Vector3 lookAt = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var wheel = Input.GetAxis("Mouse ScrollWheel");
        var mouse = Input.mouseScrollDelta;

        var movementDelta = (transform.right * h + transform.up * v + transform.forward * wheel) * speed * Time.deltaTime;
        transform.position += movementDelta;
        transform.LookAt(lookAt, Vector3.up);
	}
}
