using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemeyController : MonoBehaviour {

  public GameObject ActionObject;
  public Text HPText;
  private int defaultHP;
  private int randomSpeed;
	// Use this for initialization
	void Start () {
    ActionObject = GetComponent<GameObject> ();
    defaultHP = 100;
    HPText.text = defaultHP.ToString ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	  
	}


}
