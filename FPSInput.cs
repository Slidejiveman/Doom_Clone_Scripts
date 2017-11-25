using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides First Person movement in Unity.
/// Requires the GameObject to also be a CharacterController
/// Can be accessed from the component menu under the directory
/// specified below. This script is set for walking by default,
/// but flight can be enabled if gravity is set to 0f.
/// </summary>
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Scripts/FPS Input")]
public class FPSInput : MonoBehaviour {

    public float speed = 6.0f;    // the speed variable affects how the player can move
    public float gravity = -9.8f; // actual acceleration due to gravity in m/s
    /// <summary>
    /// variable for referencing the character controller.
    /// This is needed so we can add collision detection to
    /// the player character.
    /// </summary>
    private CharacterController _charController;

	// Use this for initialization
	void Start () {
        // access other components attached to the same object.
        _charController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        // Unity input settings have WASD and the arrow keys mapped under
        // the Horizontal and Vertical properties by default.
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);   // a vector represents movement
        movement = Vector3.ClampMagnitude(movement, speed);  // restrict diagonal movement speed to axis movement speed
        movement.y = gravity;                                // use gravity instead of 0 to prevent flying
        movement *= Time.deltaTime;                          // makes it frame independent
        movement = transform.TransformDirection(movement);   // makes movement relative to global coordinates
        _charController.Move(movement);	                     // this is the actual movement taken
	}
}
