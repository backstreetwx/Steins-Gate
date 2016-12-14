using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using Enemies;
using Players;
using GameOver;
using UI;

namespace GamePlay
{
  public class BattleManager : MonoBehaviour {

    public PlayerController PlayerObject;
    public EnemyController EnemyObject;
    public GameOverManager GameOverObject;
    public BattleProgressTextController BattleTextController;

    public UnityEvent GameOverEvent;

    // Use this for initialization
    void Start () {
      GameOverEvent = new UnityEvent();
      GameOverEvent.AddListener (GameOverObject.GameIsOver);
    }

    // Update is called once per frame
    void Update () {

      if (EnemyObject.EnemyHP <= 0||PlayerObject.PlayerHP <= 0) {
        GameOverEvent.Invoke ();
      }
    }

    public void GameStart(){

      float _playerSpeed = PlayerObject.GetSpeed ();
      float _enemySpeed = EnemyObject.GetSpeed ();

      if (_playerSpeed >= _enemySpeed) {

        PlayerAction ();
        if (EnemyObject.EnemyHP <= 0) {
          BattleTextController.SetText ("Enemy Dead\n");
        } else {
          EnemyAction ();
        }

      }
      if (_playerSpeed < _enemySpeed) {

        EnemyAction ();
        if (PlayerObject.PlayerHP <= 0) {
          BattleTextController.SetText ("Player Dead\n");
        } else {
          PlayerAction ();
        }

      }

    }

    public void Defense(){
      
      BattleTextController.SetText ("Player Defense!!\n");
      BattleTextController.SetText ("Enemy Attack!!\n");
      if (IsHit(EnemyObject.HitRate,PlayerObject.MissRate)) {
        int _attackOfEnemey = EnemyObject.AttackDamage ();
        if (IsCriticalStrike (EnemyObject.CriticalStrike)) {
          _attackOfEnemey = _attackOfEnemey * 2;
          BattleTextController.SetText ("Critical Strike!!\n");
        }
        Debug.Log ("Enemy attack "+ _attackOfEnemey.ToString());
        int _damageOfEnemey = DamageAfterDefense (_attackOfEnemey,PlayerObject.Defense*2);
        Debug.Log ("Enemy damage "+ _damageOfEnemey.ToString());
        BattleTextController.SetText ("Player HP -" + _damageOfEnemey.ToString () + "\n");
        PlayerObject.GetDamage (_damageOfEnemey);

      } else{
        BattleTextController.SetText ("Miss!!\n");
      }

    }

    private void PlayerAction(){
      
      BattleTextController.SetText ("Player Attack!!\n");
      if (IsHit(PlayerObject.HitRate,EnemyObject.MissRate)) {
        int _attackOfPlayer = PlayerObject.AttackDamage ();
        if (IsCriticalStrike (PlayerObject.CriticalStrike)) {
          _attackOfPlayer = _attackOfPlayer * 2;
          BattleTextController.SetText ("Critical Strike!!\n");
        }
        Debug.Log ("Player attack "+ _attackOfPlayer.ToString());
        int _damageOfPlayer = DamageAfterDefense (_attackOfPlayer,EnemyObject.Defense);
        Debug.Log ("Player damage "+ _damageOfPlayer.ToString());
        BattleTextController.SetText ("Enemy HP -" + _damageOfPlayer.ToString () + "\n");
        EnemyObject.GetDamage (_damageOfPlayer);

      } else{
        BattleTextController.SetText ("Miss!!\n");
      }
    }

    private void EnemyAction(){

      BattleTextController.SetText ("Enemy Attack!!\n");
      if (IsHit(EnemyObject.HitRate,PlayerObject.MissRate)) {
        int _attackOfEnemey = EnemyObject.AttackDamage ();
        if (IsCriticalStrike (EnemyObject.CriticalStrike)) {
          _attackOfEnemey = _attackOfEnemey * 2;
          BattleTextController.SetText ("Critical Strike!!\n");
        }
        Debug.Log ("Enemy attack "+ _attackOfEnemey.ToString());
        int _damageOfEnemey = DamageAfterDefense (_attackOfEnemey,PlayerObject.Defense);
        Debug.Log ("Enemy damage "+ _damageOfEnemey.ToString());
        BattleTextController.SetText ("Player HP -" + _damageOfEnemey.ToString () + "\n");
        PlayerObject.GetDamage (_damageOfEnemey);

      } else{
        BattleTextController.SetText ("Miss!!\n");
      }

    }


    private bool IsCriticalStrike(int criticalStrike){
      int _randomTemp = Random.Range (1,101);

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

      int _randomHit = Random.Range (0,100);

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

    int defaultHitRate = 50;
  }
}