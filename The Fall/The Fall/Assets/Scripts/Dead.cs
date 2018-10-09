using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Dead : MonoBehaviour {
    private int buildIndex;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        getscene();
	}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "ball")
        {
            SceneManager.LoadScene(buildIndex);
        } 
    }
    void getscene()
    {
        //sceneName = SceneManager.GetActiveScene().name;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
