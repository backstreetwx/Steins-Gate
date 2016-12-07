using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

  public Text PlayerHPText;
  public Text EnemyHPText;
  public Text GameOverText;
  public Text GameRestartText;

  public Text GameFightText;
  private string playerHP;
  private string enemyHP;

  private bool gameOver;
  private bool restart;

	// Use this for initialization
	void Start () {
    playerHP = PlayerHPText.text;
    enemyHP  = EnemyHPText.text;
    gameOver = false;
    restart = false;
	}
	
	// Update is called once per frame
  void FixedUpdate () {
    playerHP = PlayerHPText.text;
    enemyHP  = EnemyHPText.text;

    if (int.Parse(playerHP) <= 0) {
      GameOverText.text = "Player Lose";
      gameOver = true;
    }

    if (int.Parse(enemyHP)<=0) {
      GameOverText.text = "Player Win";
      gameOver = true;
    }

    if (gameOver) {
      GameRestartText.text = "Press 'R' for Restart";
      restart = true;
    }

    if (restart) {
      
      if (Input.GetKeyDown (KeyCode.R))
      {
        Application.LoadLevel (Application.loadedLevel);
      }
    }

	}
}
