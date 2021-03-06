using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

	public Vector3 direction; 
	public Player player;
	public int scoreValue;
	private GameController gameController; 

	public int damage = 3; 
	public float speed = 3.5f;
	public float distanceToStop = 1f;
	public bool chasingPlayer = true; 

	public float missingInterval = 0.5f;
	private float missingTimer = 0f; 

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController> ();
		}
		if (gameController == null)
		{
				Debug.Log("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, player.transform.position) < distanceToStop) {
			chasingPlayer = false;
		}

		if (chasingPlayer) {
			transform.position += direction * speed * Time.deltaTime; 
		} else { 
			missingTimer -= Time.deltaTime;
			if (missingTimer <= 0f) {
				missingTimer = missingInterval; 

				player.health -= damage; 
			} 
		}
	}

	void OnMouseOver(){
		//When you left click on the dog...
		if (Input.GetMouseButtonDown (0)) {
			Destroy (gameObject);
			gameController.AddScore (scoreValue); 
		}
	}
}

