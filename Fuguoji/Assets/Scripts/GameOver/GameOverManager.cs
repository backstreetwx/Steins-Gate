using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Enemies;
using Players;

namespace GameOver
{
  public class GameOverManager : MonoBehaviour {

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
    void Update () {

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

    public void GameIsOver(string message){

      GameOverText.text = message;
      gameOver = true;

    }
  }
}
