using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Attack : MonoBehaviour {


  public Text PlayerHPText;
  public Text EnemeyHPText;
  public int AttackMin;
  public int AttackMax;


  private int defaultPlayerHP;
  private int defaultEnemeyHP;

	// Use this for initialization
	void Start () {
    
    defaultPlayerHP = 100;
    defaultEnemeyHP = 100;
    PlayerHPText.text = defaultPlayerHP.ToString ();
    EnemeyHPText.text = defaultEnemeyHP.ToString ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
    PlayerHPText.text = defaultPlayerHP.ToString ();
    EnemeyHPText.text = defaultEnemeyHP.ToString ();
	}

  public void AttackAction(){
    int _attackOfPlayer = Random.Range (AttackMin, AttackMax);
    int _attackOfEnemey = Random.Range (AttackMin, AttackMax);
    Debug.Log ("Player attack : " + _attackOfPlayer.ToString());
    Debug.Log ("Enemey attack : " + _attackOfEnemey.ToString());

    defaultPlayerHP = defaultPlayerHP - _attackOfEnemey;
    defaultEnemeyHP = defaultEnemeyHP - _attackOfPlayer;
  } 
}
