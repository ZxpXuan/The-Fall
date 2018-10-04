using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelmanager : MonoBehaviour {
    [SerializeField]
    public UIManager uIManager;
    
	// Use this for initialization

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "ball")
        {
            uIManager.displayWin();
       
        }

    }
  
}
