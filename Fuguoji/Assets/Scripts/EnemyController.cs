﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {

  public GameObject AttackObject;
  public Text EnemyHPText;
  public int CriticalStrike;
  public int HitRate;
  public int MissRate;
  public int Defense;
  public int EnemyHP;

  public int AttackMin;
  public int AttackMax;

  private float Speed;
  private Animator animator;
	
  void Awake(){
    animator = GetComponent<Animator> ();
    AttackObject = GetComponent<GameObject> ();
  }

	void Start () {
    
		EnemyHP = 100;
    EnemyHPText.text = EnemyHP.ToString ();
	}
	
	// Update is called once per frame
  void FixedUpdate () {
    EnemyHPText.text = EnemyHP.ToString ();	  
	}

  public float GetSpeed(){
    
    float _speed = Random.Range (0, 10);
    Debug.Log ("Enemy Speed :" + _speed);
    return _speed;
  }

  public int AttackDamage(){
    animator.SetTrigger ("EnemyAttack");
    int _attackOfEnemy = Random.Range (AttackMin, AttackMax);

    Debug.Log ("Enemy attack : " + _attackOfEnemy.ToString());

    return _attackOfEnemy;
  }

  public void GetDamage(int demage){
    EnemyHP = EnemyHP - demage;
    Debug.Log ("Enemy HP : " + EnemyHP.ToString());
  }

}
