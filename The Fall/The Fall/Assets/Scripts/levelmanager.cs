using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelmanager : MonoBehaviour {
    [SerializeField]
    public UIManager uIManager;
    [SerializeField]
    public int nextLevelid;

    [SerializeField]
    List<ParticleSystem> ps;


    private IEnumerator coroutine;

    [SerializeField]
    public float waitTime;
	// Use this for initialization

    void Start () {
        coroutine = WaitAndWin(waitTime);

    }
	
    IEnumerator WaitAndWin(float waitTime){

        yield return new WaitForSeconds(waitTime);
        uIManager.displayWin();
        PlayerPrefs.SetInt("levelsUnlocked", nextLevelid);
    }

	// Update is called once per frame
	void Update () {

    }
    private void OnCollisionEnter(Collision collision)
    {
       
        AkSoundEngine.PostEvent("Play_Goal_Reached", gameObject);

        foreach(ParticleSystem part in ps){
            part.Play();

        }
        if (collision.collider.gameObject.tag == "ball")
        {
            StartCoroutine(coroutine);
       
        }

    }
  
}
