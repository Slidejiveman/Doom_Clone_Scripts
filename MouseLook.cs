using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    /// <summary>
    /// Associates names with settings for mouse movements.
    /// Public enumerations show up as drop downs in the editor.
    /// </summary>
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    // Set default value for the public enumeration axes
    public RotationAxes axes = RotationAxes.MouseXAndY;

    // Set the default sensitivities for the rotation 
    // Used to scale against the mouse input.
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    // Limit the angle at which the player can look up or down
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    // the angle of rotation about X (pitch)
    private float _rotationX = 0f;

    // Start called once
    void Start ()
    {
        // the following code prevents the over-arching physics
        // simulation from affecting the player character's movement
        // other rigidbody objects will cause objects to bounce and 
        // tumble around in the scene.
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

	// Update is called once per frame
	void Update () {
		if (axes == RotationAxes.MouseX)
        {
            // horizontal rotation here
            // this will be rotation about the y - axis, or yaw.
            // using Input gets information from input devices.
            // using GetAxis accepts the name of the axis you are interested in
            // Mouse X refers to input set up in the unity editor, not my enum.
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            
        }
        else if (axes == RotationAxes.MouseY)
        {
            // vertical rotation here
            // increment vertical angle based on mouse
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            // clamp the vertical angle between minimum and maximum values
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            // keep the same Y angle. There is no rotation about the y-axis.
            float rotationY = transform.localEulerAngles.y;

            // create a new vector from the stored angle values
            // a vector is a group of number stored as a unit. Vector3 has 3 numbers
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        } 
        else
        {
            // both horizontal and vertical rotation here
            // increment vertical angle based on mouse
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            // clamp the vertical angle between minimum and maximum values
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            // delta is the amount of change that we are capturing.
            // this amount is then added to the current rotation value.
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
	}
}
