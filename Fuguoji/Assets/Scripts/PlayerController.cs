using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

  public GameObject AttackObject;
  public Text PlayerHPText;
  public int AttackMin;
  public int AttackMax;
  public static int Attack;
  public static string State;
  public int CriticalStrike;
  public int HitRate;
  public int MissRate;
  public int Defense;

  private float Speed;
  private int playerHP;

  private Animator animator;

  void Awake(){
    animator = GetComponent<Animator> ();
    AttackObject = GetComponent<GameObject> ();
  }

	// Use this for initialization
	void Start () {
    
    State = "init";
    playerHP = 100;
    PlayerHPText.text = playerHP.ToString ();
    Attack = Random.Range (AttackMin, AttackMax);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
    PlayerHPText.text = playerHP.ToString ();
	}

  public float GetSpeed(){

    float _speed = Random.Range (0, 10);
    Debug.Log ("Player Speed :" + _speed);
    return _speed;
  }

  public int AttackAction(){
    animator.SetTrigger ("PlayerAttack");
    int _attackOfPlayer = Random.Range (AttackMin, AttackMax);
    Debug.Log ("Player attack : " + _attackOfPlayer.ToString());
    return _attackOfPlayer;
  }

  public void Attacked(int demage){
    playerHP = playerHP - demage;

    Debug.Log ("Player HP : " + playerHP.ToString());
  }

  public int GetHP(){

    return playerHP;
  }

  public int GetCriticalStrike(){
    return CriticalStrike;
  }

  public int GetHitRate(){
    return HitRate;
  }
  public int GetMissRate(){
    return MissRate;

  }

  public int GetDefense(){

    return Defense;
  }

}
