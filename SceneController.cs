using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField]
    private GameObject enemyPrefab; // Serialized variable for linking to prefab
    private GameObject _enemy;      // keeps track of the enemy instance in scene
	
	// Update is called once per frame
	void Update () {
        // only spawn a new enemy if there isn't one in the scene
		if(_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject; // method that copies prefab
            _enemy.transform.position = new Vector3(-25, 1, 8);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
        }
	}
}
