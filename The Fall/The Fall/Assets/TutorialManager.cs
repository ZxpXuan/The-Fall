using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TutorialManager : MonoBehaviour {

    [SerializeField]
    public GameObject tutorialPanel;

    [SerializeField]
    public List<GameObject> tutorialOrder;

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
        tutorialOrder[index].SetActive(true);
        audioSource.clip = tutorialAudio[index];
        currentTutorial = index;
    }

    public void startTutorial(){
        
        tutorialPanel.SetActive(true);
        updateTutorial(currentTutorial);
    }

    public void endTutorial(){

        tutorialPanel.SetActive(false);
        tb.enabled = true;
    }
}
