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

    [SerializeField]
    public int gameWinID;


    [SerializeField]
    public bool hasCollided;
    // Use this for initialization


   
    void Start () {
        waitTime = 3.5f;

        coroutine = WaitAndWin(waitTime);
      //  if (GameObject.Find("GameManager").GetComponent<UIManager>()!=null)
        uIManager = GameManager.Instance.GetComponent<UIManager>();
        GameManager.Instance.hasCollided = false ;
        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelStart,Firebase.Analytics.FirebaseAnalytics.ParameterLevel,"Level" + (nextLevelid-1));
    }

    IEnumerator WaitAndWin(float waitTime){

        yield return new WaitForSeconds(waitTime);
        //uIManager.displayWin();
        //uIManager.GetComponent<GameManager>().nextScene();
        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelEnd, Firebase.Analytics.FirebaseAnalytics.ParameterLevel, "Level" + (nextLevelid - 1));

        GameManager.Instance.nextScene();
        PlayerPrefs.SetInt("levelsUnlocked", nextLevelid);
    }

	// Update is called once per frame
	void Update () {

    }
    private void OnCollisionEnter(Collision collision)
    {
       
       // AkSoundEngine.PostEvent("Play_Goal_Reached", gameObject);
       // AkSoundEngine.PostEvent("Stop_Goal_Static", gameObject);

        foreach (ParticleSystem part in ps){
            part.Play();

        }
        if (collision.collider.gameObject.tag == "ball" && gameWinID != nextLevelid)
        {
            uIManager.GetComponent<AI>().setGameState(Brain.GameState.Winner);
            GameManager.Instance.ai.calculateCurrentMood();
            GameManager.Instance.ai.SetBrainState();
           GameManager.Instance.hasCollided = true;

            StartCoroutine(coroutine);
            hasCollided = true;
        }

      else  if(gameWinID == nextLevelid)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Winnder","Level 13 complete", 0.5f);

            GameManager.Instance.winGame();

        }

    }
  
}
