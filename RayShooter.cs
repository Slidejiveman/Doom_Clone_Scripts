using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {

    private Camera _camera;

	// Use this for initialization
	void Start () {
        _camera = GetComponent<Camera>();            // access other components attached to same object
        Cursor.lockState = CursorLockMode.Locked;    // locking the cursor will lock the mouse. Esc frees it.
        Cursor.visible = false;                      // Hides the cursor when not locked
	}
	
	// Update is called once per frame
	void Update () {
        // respond to the mouse button: 0 = left; 1 = right; 2 = middle
		if (Input.GetMouseButtonDown(0))
        {
            // the middle of the screen is half of its width and half of its height
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            // create the ray at the center of the screen
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) // raycast fills a reference variable with information
            {
                GameObject hitObject = hit.transform.gameObject; // retrieve the object the ray hit
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null) // make sure it is a reactive target
                {
                    Debug.Log("Target hit");
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point)); // launch a coroutine in response to a hit
                } 
            }
        }
	}

    /// <summary>
    /// Coroutines use IEnumerator functions
    /// </summary>
    /// <param name="point"> the point with the intersection information </param>
    /// <returns></returns>
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); // code to create a sphere
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1); // yield tells where coroutines should pause
        Destroy(sphere); // remove the GameObject and clear its memory
    }

    /// <summary>
    /// OnGUI is added to display the indicator to the screen
    /// </summary>
    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*"); // this command displays it to the screen
    }
}
