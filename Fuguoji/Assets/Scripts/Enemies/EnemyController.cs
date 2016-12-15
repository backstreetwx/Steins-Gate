using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Enemies
{
  public class EnemyController : MonoBehaviour {

    public Text EnemyHPText;
    public int CriticalStrike;
    public int HitRate;
    public int MissRate;
    public int Defense;
    public int EnemyHP;

    public int AttackMin;
    public int AttackMax;

    public string State;

    public float Speed;
    private Animator animator;

    void Awake(){
      animator = GetComponent<Animator> ();

    }

    void Start () {
      State = "Init";
      Speed = Random.Range (0, 10);
      Debug.Log ("" + gameObject.name + " Speed :" + Speed);
      EnemyHP = 100;
      EnemyHPText.text = EnemyHP.ToString ();
    }

    // Update is called once per frame
    void Update () {
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
}