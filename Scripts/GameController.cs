using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameController : MonoBehaviour {

	public Player player; 
	public GameObject DogPrefab;
	public Text scoreText; 
	private int score;
	public float enemySpawnDistance = 20f; 

	public float enemyInterval = 2.0f;
	public float minimumEnemyInterval = 0.5f;
	public float enemyIntervalDecrement = 0.1f;
	public GameObject GameOverCanvas;

	private float enemyTimer = 0f; 
	private float resetTimer = 6f;
	private bool gameOver = false; 

	// Use this for initialization
	void Start () {
		score = 0; 
		UpdateScore ();
		GameOverCanvas.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (player.health <0 && !gameOver) {
			gameOver = true;
			GameOverCanvas.SetActive (true);
		}
		if (gameOver) {
			resetTimer -= Time.deltaTime; 
			if (resetTimer <0.0f) {
				SceneManager.LoadScene("game");
				gameOver = false;
				player.health = 100; 
				GameOverCanvas.SetActive (false); 
			}
		}			

		enemyTimer -= Time.deltaTime;
		if (enemyTimer <=0) {
			enemyTimer = enemyInterval;
			enemyInterval -= enemyIntervalDecrement;

			if (enemyInterval < minimumEnemyInterval) {
				enemyInterval = minimumEnemyInterval;
			}

			GameObject dogObject = Instantiate(DogPrefab);
			Dog dog = dogObject.GetComponent<Dog> ();

			float randomAngle = Random.Range (0f, 2f * Mathf.PI);
			dog.transform.position = new Vector3 (
				player.transform.position.x + Mathf.Cos (randomAngle) * enemySpawnDistance,
				dog.transform.position.y,
				player.transform.position.z + Mathf.Sin (randomAngle) * enemySpawnDistance
			); 

			dog.player = player; 
			dog.direction = (player.transform.position - dog.transform.position).normalized; 
		}
	}
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore (); 
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score; 
	}

}
