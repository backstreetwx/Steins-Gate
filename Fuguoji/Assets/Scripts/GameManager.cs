using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

  public PlayerController PlayerObject;
  public EnemyController EnemyObject;
  public Text FightMessage;

  private int PlayerHitRate;
  private int PlayerMissRate;
  private int PlayerCriticalStrike;
  private int PlayerDefense;

  private int EnemyHitRate;
  private int EnemyMissRate;
  private int EnemyCriticalStrike;
  private int EnemyDefense;

  private int DefaultHitRate = 50;

  void Awake(){
    PlayerHitRate = PlayerObject.GetHitRate ();
    PlayerMissRate = PlayerObject.GetMissRate ();
    PlayerCriticalStrike = PlayerObject.GetCriticalStrike ();
    PlayerDefense = PlayerObject.GetDefense ();

    EnemyHitRate = EnemyObject.GetHitRate ();
    EnemyMissRate = EnemyObject.GetMissRate ();
    EnemyCriticalStrike = EnemyObject.GetCriticalStrike ();
    EnemyDefense = EnemyObject.GetDefense ();

  }
	// Use this for initialization
	void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void GameStart(){

    float _playerSpeed = PlayerObject.GetSpeed ();
    float _enemySpeed = EnemyObject.GetSpeed ();

    if (_playerSpeed >= _enemySpeed) {
      
      PlayerAction ();
      if (EnemyObject.GetHP () <= 0) {
        FightMessage.text += "Enemy Dead\n";
      } else {
        EnemyAction ();
      }

    }
    if (_playerSpeed < _enemySpeed) {

      EnemyAction ();
      if (PlayerObject.GetHP () <= 0) {
        FightMessage.text += "Player Dead\n";
      } else {
        PlayerAction ();
      }

    }

  }

  public void Defense(){

    FightMessage.text += "Player Defense!!\n";
    FightMessage.text += "Enemy Attack!!\n";
    if (IsHit(EnemyHitRate,PlayerMissRate)) {
      int _attackOfEnemey = EnemyObject.AttackAction ();
      if (IsCriticalStrike (EnemyCriticalStrike)) {
        _attackOfEnemey = _attackOfEnemey * 2;
        FightMessage.text += "Critical Strike!!\n";
      }
      Debug.Log ("Enemy attack "+ _attackOfEnemey.ToString());
      int _damageOfEnemey = DamageAfterDefense (_attackOfEnemey,PlayerDefense*2);
      Debug.Log ("Enemy damage "+ _damageOfEnemey.ToString());
      FightMessage.text += "Player HP -" + _damageOfEnemey.ToString () + "\n";
      PlayerObject.Attacked (_damageOfEnemey);

    } else{
      FightMessage.text += "Miss!!\n";
    }

  }

  private void PlayerAction(){

    FightMessage.text += "Player Attack!!\n";
    if (IsHit(PlayerHitRate,EnemyMissRate)) {
      int _attackOfPlayer = PlayerObject.AttackAction ();
      if (IsCriticalStrike (PlayerCriticalStrike)) {
        _attackOfPlayer = _attackOfPlayer * 2;
        FightMessage.text += "Critical Strike!!\n";
      }
      Debug.Log ("Player attack "+ _attackOfPlayer.ToString());
      int _damageOfPlayer = DamageAfterDefense (_attackOfPlayer,EnemyDefense);
      Debug.Log ("Player damage "+ _damageOfPlayer.ToString());
      FightMessage.text += "Enemy HP -" + _damageOfPlayer.ToString () + "\n";
      EnemyObject.Attacked (_damageOfPlayer);

    } else{
      FightMessage.text += "Miss!!\n";
    }
  }

  private void EnemyAction(){
  
    FightMessage.text += "Enemy Attack!!\n";
    if (IsHit(EnemyHitRate,PlayerMissRate)) {
      int _attackOfEnemey = EnemyObject.AttackAction ();
      if (IsCriticalStrike (EnemyCriticalStrike)) {
        _attackOfEnemey = _attackOfEnemey * 2;
        FightMessage.text += "Critical Strike!!\n";
      }
      Debug.Log ("Enemy attack "+ _attackOfEnemey.ToString());
      int _damageOfEnemey = DamageAfterDefense (_attackOfEnemey,PlayerDefense);
      Debug.Log ("Enemy damage "+ _damageOfEnemey.ToString());
      FightMessage.text += "Player HP -" + _damageOfEnemey.ToString () + "\n";
      PlayerObject.Attacked (_damageOfEnemey);

    } else{
      FightMessage.text += "Miss!!\n";
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
    
    int _hitRate = DefaultHitRate + hit - miss;
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
  
    return Mathf.CeilToInt((attack * attack) / (attack + defense));
  }


}
