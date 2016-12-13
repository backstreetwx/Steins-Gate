using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

  public PlayerController PlayerObject;
  public EnemyController EnemyObject;
  public Text GameOverText;
  public Text GameRestartText;

  private string playerHP;
  private string enemyHP;

  private bool gameOver;
  private bool restart;

	// Use this for initialization
	void Start () {
    gameOver = false;
    restart = false;
	}
	
	// Update is called once per frame
  void FixedUpdate () {

    if (gameOver) {
      GameRestartText.text = "Press 'R' for Restart";
      restart = true;
    }

    if (restart) {
      
      if (Input.GetKeyDown (KeyCode.R))
      {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
    }
	}

  public void GameIsOver(){

    GameOverText.text = "Game Over";
    gameOver = true;

  }
}
