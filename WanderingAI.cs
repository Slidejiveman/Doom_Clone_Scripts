using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;  // value for how close obstacles can get
    private bool _alive;                // keep track of whether enemy is hit
    [SerializeField]
    private GameObject fireballPrefab;
    private GameObject _fireball;

    // Start is called once prior to start of simulation
    void Start()
    {
        _alive = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_alive)
        {
            // move forward on Z continuously regardless of turning
            transform.Translate(0, 0, speed * Time.deltaTime);

            // a ray at the same position of the object, which projects
            // out in the same direction as the object
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // this performs raycasting with the circumference around the ray
            // this is done to account for the width of the object
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                // keep track of the object hit by the spherecast
                GameObject hitObject = hit.transform.gameObject;
                // if that object is the player, then shoot a fireball.
                if(hitObject.GetComponent<PlayerCharacter>())
                {
                    if(_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;

                        // place fireball in front of the enemy moving in the same direction
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    // turn in a semi-random direction
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
	}

    /// <summary>
    /// Allows outside code to set the alive bool
    /// </summary>
    /// <param name="alive"> bool representing current state </param>
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
