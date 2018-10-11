using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelmanager : MonoBehaviour {
    [SerializeField]
    public UIManager uIManager;

    NewAudioManager audMan;

    [SerializeField]
    List<ParticleSystem> ps;
	// Use this for initialization

    void Start () {
        audMan = NewAudioManager.instance;
	}
	
	// Update is called once per frame
	void Update () {

    }
    private void OnCollisionEnter(Collision collision)
    {
        audMan.PlaySound("Goal");

        foreach(ParticleSystem part in ps){
            part.Play();

        }
        if (collision.collider.gameObject.tag == "ball")
        {
            uIManager.displayWin();
       
        }

    }
  
}
