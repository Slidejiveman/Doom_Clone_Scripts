using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {

	public void ReactToHit()
    {
        // Get the Wandering AI component script
        WanderingAI behavior = GetComponent<WanderingAI>();
        if(behavior != null)
        {
            // if shot, turn the alive bool to false to stop movement
            behavior.SetAlive(false);
        }
        StartCoroutine(Die()); // coroutines are similar to interrupts
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0); // topple the enemy
        yield return new WaitForSeconds(1.5f); // wait 1.5 seconds before disappearing
        Destroy(this.gameObject);  // object can destroy itself to disappear
    }

}
