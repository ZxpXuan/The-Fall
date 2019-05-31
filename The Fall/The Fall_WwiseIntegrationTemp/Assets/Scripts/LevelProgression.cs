using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelProgression : MonoBehaviour {

    PlayerPrefs playerPrefs;
    int levelsUnlocked;
    [SerializeField]
    List<Button> levelButtons;

    [SerializeField]
    public bool unlockAllLevels;
	// Use this for initialization
	void Start () {
        foreach (Button bt in levelButtons)
        {
            bt.interactable = false;

        }
        levelsUnlocked=  PlayerPrefs.GetInt("levelsUnlocked", 1);
        int i = 0;
        while(i<levelsUnlocked){

            levelButtons[i].interactable = true;
            i++;
        }
        if(unlockAllLevels){
            foreach(Button bt in levelButtons){
                bt.interactable = true;

            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
