using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Players
{
  public class PlayerController : MonoBehaviour {

    public Text PlayerHPText;
    public int CriticalStrike;
    public int HitRate;
    public int MissRate;
    public int Defense;
    public int PlayerHP;

    public int AttackMin;
    public int AttackMax;

    public string State;

    public float Speed;
    private Animator animator;

    void Awake(){
      State = "Init";
      Speed = Random.Range (0, 10);
      Debug.Log ("" + gameObject.name + " Speed :" + Speed);
      animator = GetComponent<Animator> ();
    }

  	// Use this for initialization
  	void Start () {
      
      PlayerHP = 100;
      PlayerHPText.text = PlayerHP.ToString ();

  	}
  	
  	// Update is called once per frame
  	void FixedUpdate () {
      PlayerHPText.text = PlayerHP.ToString ();
  	}

    public float GetSpeed(){

      float _speed = Random.Range (0, 10);
      Debug.Log ("Player Speed :" + _speed);
      return _speed;
    }

    public int AttackDamage(){
      animator.SetTrigger ("PlayerAttack");
      int _attackOfPlayer = Random.Range (AttackMin, AttackMax);
      Debug.Log ("Player attack : " + _attackOfPlayer.ToString());
      return _attackOfPlayer;
    }

    public void GetDamage(int demage){
      PlayerHP = PlayerHP - demage;

      Debug.Log ("Player HP : " + PlayerHP.ToString());
    }

  }
}