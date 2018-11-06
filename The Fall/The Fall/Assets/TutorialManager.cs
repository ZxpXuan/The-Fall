using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TutorialManager : MonoBehaviour {

    [SerializeField]
    public GameObject tutorialPanel;

    [SerializeField]
    public List<GameObject> tutorialOrder;

    [SerializeField]
    public List<string> tutorialAudioOrder;

    [SerializeField]
    public List <AudioClip> tutorialAudio;

    private int currentTutorial;

    public bool isTutorialActive;

    public tragball tb;

    public AudioSource audioSource;
	// Use this for initialization
	void Start () {
        tb.enabled = false;
        startTutorial();
        currentTutorial = 0;
        isTutorialActive = true;
        GetComponent<AI>().justSetState(Brain.GameState.Tutorial);
        GetComponent<AI>().enableTimeTracking = false;

    }
	
	// Update is called once per frame
	void Update () {
	
        if(isTutorialActive && Input.GetMouseButtonDown(0) && (currentTutorial +1 < tutorialOrder.Count)){

            updateTutorial(currentTutorial + 1);
        }
        else if (isTutorialActive && Input.GetMouseButtonDown(0) && (currentTutorial + 1 >= tutorialOrder.Count) ){

            endTutorial();
        }
	}


    public void updateTutorial(int index){
        foreach(GameObject go in tutorialOrder){

            go.SetActive(false);
            
        }
        if (tutorialAudioOrder.Count == tutorialOrder.Count && tutorialOrder.Count != 0)
        {
            AkSoundEngine.PostEvent(tutorialAudioOrder[index], gameObject);

            tutorialOrder[index].SetActive(true);
            audioSource.clip = tutorialAudio[index];
        }
        currentTutorial = index;
    }

    public void startTutorial(){
        
        tutorialPanel.SetActive(true);
        updateTutorial(currentTutorial);
    }

    public void endTutorial(){
        isTutorialActive = false;
        tutorialPanel.SetActive(false);
        tb.enabled = true;
        GetComponent<AI>().enableTimeTracking = true;
        GetComponent<AI>().levelStartTime = Time.time;
        GetComponent<AI>().justSetState(Brain.GameState.Start);

    }
}
