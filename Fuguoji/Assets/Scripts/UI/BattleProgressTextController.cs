using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace UI{
  
  public class BattleProgressTextController : MonoBehaviour {

    Text textObject;
  	// Use this for initialization
  	void Start () {
      textObject = GetComponent<Text>();
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}

    public void SetText(string text){
      textObject.text += text; 
    }

  }

}