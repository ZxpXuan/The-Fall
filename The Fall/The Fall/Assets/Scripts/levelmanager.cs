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
        waitTime = 2;

        coroutine = WaitAndWin(waitTime);



    }
	
    IEnumerator WaitAndWin(float waitTime){

        yield return new WaitForSeconds(waitTime);
        //uIManager.displayWin();
        //uIManager.GetComponent<GameManager>().nextScene();
        GameManager.Instance.nextScene();
        PlayerPrefs.SetInt("levelsUnlocked", nextLevelid);
    }

	// Update is called once per frame
	void Update () {

    }
    private void OnCollisionEnter(Collision collision)
    {
       
        AkSoundEngine.PostEvent("Play_Goal_Reached", gameObject);
        AkSoundEngine.PostEvent("Stop_Goal_Static", gameObject);

        foreach (ParticleSystem part in ps){
            part.Play();

        }
        if (collision.collider.gameObject.tag == "ball")
        {
            uIManager.GetComponent<AI>().setGameState(Brain.GameState.Winner);
            StartCoroutine(coroutine);
       
        }

    }
  
}
