using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;
using Battle.Enemies;
using Battle.Players;
using Battle.BattleOver;
using Battle.BattleUI;

[System.Serializable]
public class GameOverEvent : UnityEvent<string> {}

public class DelayToInvoke : MonoBehaviour
{

  public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
  {
    yield return new WaitForSeconds(delaySeconds);
    action();
  }
}



namespace Battle.BattleStart
{

  public class BattleStartManager : MonoBehaviour {

    public PlayerController PlayerObject;
    public EnemyController EnemyObject;
    public BattleOverManager GameOverObject;
    public BattleProgressTextController BattleTextController;

    public GameOverEvent BattleOverEvent;

    public List<EnemyController> enemyObjects;

    private string playerMode;
    private EnemyController SingleTargetEnemyObject;

    // Use this for initialization
    void Start () {
      BattleOverEvent = new GameOverEvent();
      BattleOverEvent.AddListener (GameOverObject.GameIsOver);

      foreach(EnemyController enemy in enemyObjects){
        Debug.Log("Enemey :"+enemy.name);
      }

      playerMode = "Wait";
    }
      
    // Update is called once per frame
    void Update () {

//      if (SingleTargetEnemyObject.EnemyHP <= 0||PlayerObject.PlayerHP <= 0) {
//        GameOverEvent.Invoke ();
//      }
      if (PlayerObject.State=="Dead") {
        BattleOverEvent.Invoke ("Player Lose!");  
      }
      if (IsAllEnemiesDead (enemyObjects)) {
        BattleOverEvent.Invoke ("Player Win!!");  
      }

      if (playerMode=="Attack") {
        if (SingleTargetEnemyObject != null) {
          Attack (SingleTargetEnemyObject);
          SingleTargetEnemyObject = null;
        }
      }

    }

    public void SetTarget(EnemyController enemy){
      Debug.Log ("Set Target State" + enemy.State);
      if (enemy.State != "Dead"&&playerMode == "Attack") {
        this.SingleTargetEnemyObject = enemy;
      }
    }

    public void GameStart(){
      if (playerMode == "Wait") {
        this.playerMode = "Attack";
      }
//      float _playerSpeed = PlayerObject.GetSpeed ();
//      float _enemySpeed = SingleTargetEnemyObject.GetSpeed ();
//
//      if (_playerSpeed >= _enemySpeed) {
//
//        PlayerAction ();
//        if (SingleTargetEnemyObject.EnemyHP <= 0) {
//          BattleTextController.SetText ("Enemy Dead\n");
//        } else {
//          EnemyAction ();
//        }
//
//      }
//      if (_playerSpeed < _enemySpeed) {
//
//        EnemyAction ();
//        if (PlayerObject.PlayerHP <= 0) {
//          BattleTextController.SetText ("Player Dead\n");
//        } else {
//          PlayerAction ();
//        }
//
//      }


    }

    public void Attack(EnemyController targetEnemey){
      float delay;
      this.playerMode = "Action";
      int _temp = enemyObjects.Count + 1;
      EnemyController _maxSpeedEnemy = null;
      float _maxSpeed = 0.0f;
      delay = 0.0f;
      for (int i = 0; i < _temp; i++) {
        
        delay += 0.9f;
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
          foreach (EnemyController enemy in enemyObjects) {
            _maxSpeed = 0.0f;
            if (enemy.Speed >= _maxSpeed && enemy.State=="Init") {
              _maxSpeed = enemy.Speed;
              _maxSpeedEnemy = enemy;
            }
          }
          Debug.Log ("Enemy Fast" + _maxSpeedEnemy.name);
          if (PlayerObject.State=="Init" && PlayerObject.Speed >= _maxSpeed) {
            PlayerAction (targetEnemey);

          } else {
            EnemyAction(_maxSpeedEnemy);
          }
        },delay));
      }
      StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
      {
        OneRoundEnd ();
      },delay));
    }


    public void Defense(){
      
      BattleTextController.SetText ("Player Defense!!\n");
      BattleTextController.SetText ("Enemy Attack!!\n");
      if (IsHit(SingleTargetEnemyObject.HitRate,PlayerObject.MissRate)) {
        int _attackOfEnemey = SingleTargetEnemyObject.AttackDamage ();
        if (IsCriticalStrike (SingleTargetEnemyObject.CriticalStrike)) {
          _attackOfEnemey = _attackOfEnemey * 2;
          BattleTextController.SetText ("Critical Strike!!\n");
        }
        Debug.Log ("Enemy attack "+ _attackOfEnemey.ToString());
        int _damageOfEnemey = DamageAfterDefense (_attackOfEnemey,PlayerObject.Defense*2);
        Debug.Log ("Enemy damage "+ _damageOfEnemey.ToString());
        BattleTextController.SetText ("Player HP -" + _damageOfEnemey.ToString () + "\n");
        PlayerObject.GetDamage (_damageOfEnemey);
        if (PlayerObject.PlayerHP <= 0) {
          PlayerObject.State = "Dead";
        }

      } else{
        BattleTextController.SetText ("Miss!!\n");
      }

    }

    private void PlayerAction(EnemyController targetEnemey){
      
      BattleTextController.SetText ("Player Attack " + targetEnemey.name + "!!\n");
      if (IsHit(PlayerObject.HitRate,targetEnemey.MissRate)) {
        int _attackOfPlayer = PlayerObject.AttackDamage ();
        if (IsCriticalStrike (PlayerObject.CriticalStrike)) {
          _attackOfPlayer = _attackOfPlayer * 2;
          BattleTextController.SetText ("Critical Strike!!\n");
        }
        Debug.Log ("Player attack "+ _attackOfPlayer.ToString());
        int _damageOfPlayer = DamageAfterDefense (_attackOfPlayer,targetEnemey.Defense);
        Debug.Log ("Player damage "+ _damageOfPlayer.ToString());
        BattleTextController.SetText (targetEnemey.name+ " HP -" + _damageOfPlayer.ToString () + "\n");
        targetEnemey.GetDamage (_damageOfPlayer);
        if (targetEnemey.EnemyHP <= 0) {
          targetEnemey.State = "Dead";
        }

      } else{
        BattleTextController.SetText ("Miss!!\n");
      }
       
      PlayerObject.State = "Moved";
    }

    private void EnemyAction(EnemyController _enemy){
      
      BattleTextController.SetText (_enemy.name + " Attack!!\n");
      if (IsHit(_enemy.HitRate,PlayerObject.MissRate)) {
        int _attackOfEnemey = _enemy.AttackDamage ();
        if (IsCriticalStrike (_enemy.CriticalStrike)) {
          _attackOfEnemey = _attackOfEnemey * 2;
          BattleTextController.SetText ("Critical Strike!!\n");
        }
        Debug.Log ("Enemy attack "+ _attackOfEnemey.ToString());
        int _damageOfEnemey = DamageAfterDefense (_attackOfEnemey,PlayerObject.Defense);
        Debug.Log ("Enemy damage "+ _damageOfEnemey.ToString());
        BattleTextController.SetText ("Player HP -" + _damageOfEnemey.ToString () + "\n");
        PlayerObject.GetDamage (_damageOfEnemey);
        if (PlayerObject.PlayerHP <= 0) {
          PlayerObject.State = "Dead";
        }

      } else{
        BattleTextController.SetText ("Miss!!\n");
      }
      //StartCoroutine(Example());

      _enemy.State = "Moved";
    }

    private void OneRoundEnd(){
      if (PlayerObject.PlayerHP > 0) {
        PlayerObject.State = "Init";
      }

      foreach(EnemyController enemy in enemyObjects){
        if (enemy.EnemyHP > 0) {
          enemy.State = "Init";
        }
      }
      this.playerMode = "Wait";
    }

    private bool IsCriticalStrike(int criticalStrike){
      int _randomTemp = UnityEngine.Random.Range (1,101);
      if (_randomTemp <= criticalStrike) {
        return true;
      } else
        return false;
    }


    private bool IsHit(int hit,int miss){

      int _hitRate = this.defaultHitRate + hit - miss;
      if (_hitRate < 0)
        _hitRate = 0;
      if (_hitRate > 100)
        _hitRate = 100;

      int _randomHit = UnityEngine.Random.Range (0,100);
      if (_randomHit <= _hitRate) {
        return true;
      } else
        return false;
    }

    private int DamageAfterDefense(int attack,int defense){

      int _value = Mathf.CeilToInt((attack * attack) / (attack + defense));
      Debug.Log ("Damage After Defense " + _value);

      return _value;
    }

    private bool IsAllEnemiesDead(List<EnemyController> enemies){
      foreach (EnemyController enemy in enemies) {
        if (enemy.EnemyHP > 0) {
          return false;
        }
      }
      return true;
    }

    int defaultHitRate = 50;
  }
}