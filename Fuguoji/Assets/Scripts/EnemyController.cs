using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {

  public GameObject AttackObject;
  public Text HPText;
  public int AttackMin;
  public int AttackMax;
  public int CriticalStrike;
  public int HitRate;
  public int MissRate;
  public int Defense;


  public static string State;
  public static int Attack;
  private float Speed;

  private int enemyHP;
  private Animator animator;
	
  void Awake(){
    animator = GetComponent<Animator> ();
    AttackObject = GetComponent<GameObject> ();
  }

	void Start () {
    
		enemyHP = 100;
		HPText.text = enemyHP.ToString ();
    Attack = Random.Range (AttackMin, AttackMax);

	}
	
	// Update is called once per frame
  void FixedUpdate () {
    HPText.text = enemyHP.ToString ();	  
	}

  public float GetSpeed(){
    
    float _speed = Random.Range (0, 10);
    Debug.Log ("Enemy Speed :" + _speed);
    return _speed;
  }

  public int AttackAction(){
    animator.SetTrigger ("EnemyAttack");
    int _attackOfEnemy = Random.Range (AttackMin, AttackMax);

    Debug.Log ("Enemy attack : " + _attackOfEnemy.ToString());

    return _attackOfEnemy;
  }

  public void Attacked(int demage){
    enemyHP = enemyHP - demage;
    Debug.Log ("Enemy HP : " + enemyHP.ToString());
  }

  public int GetHP(){

    return enemyHP;
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
