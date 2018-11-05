using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    [System.Serializable]
    public class DialogueSet{
        public Brain.MoodTypes moodType;

        public List<string> dialogue;

        public bool randomize;

    }
    [System.Serializable]
    public class TriesCategoriser
    {
        public Brain.MoodTypes moodTypes;
     
        public float fromRange;
        public float toRange;


    }

    [System.Serializable]
    public class TimeCategoriser
    {

        public Brain.TimeTaker timeTaker;
        public float fromRange;
        public float toRange;
        public int changeMoodBy;
        public bool hasMoodBeenAffected;
    }

    [SerializeField]
    public List<DialogueSet> dialogueSet;

    [SerializeField]
    public List<TriesCategoriser> moodSet;



    [SerializeField]
    public List<TimeCategoriser> timeCategoriser;

    public int currentLevelTries;
    public int totalWorldTries;
    public Brain.MoodTypes currentMood = Brain.MoodTypes.Neutral;
    public int happinessLevel;


    private float levelStartTime;
	// Use this for initialization
	void Start () {
        //switch(PlayerPrefs.GetInt("brainMood", 0))
        currentLevelTries = PlayerPrefs.GetInt("currentWorldTries", 0);
        totalWorldTries = PlayerPrefs.GetInt("totalWorldTries", 0);
        levelStartTime = Time.time;
        calculateCurrentMood();
	}

    private void OnLevelWasLoaded(int level)
    {
        
    }

    // Update is called once per frame
    void Update () {

        if(!GetComponent<GameManager>().hasBallBeenShot)
        calculateTimeMood();


	}
    public void playMoodSound()
    {
        foreach( DialogueSet ds in dialogueSet)
        {

            if(ds.moodType == currentMood)
            {
                int random = Random.Range(0, ds.dialogue.Count);
                print(ds.dialogue[random]);
                break;
            }

        }

    }
    public void calculateCurrentMood(){
        foreach (TriesCategoriser cat in moodSet)
        {
          
                if(cat.fromRange <= currentLevelTries && cat.toRange >= currentLevelTries)
                {
                    currentMood = cat.moodTypes;
                    break;
                }

            
       

            

        }

        playMoodSound();

    }

   
 
    public void calculateTimeMood()
    {

        foreach (TimeCategoriser tc in timeCategoriser)
        {
            if (tc.fromRange <= levelStartTime + Time.time && tc.toRange >= levelStartTime + Time.time)
            {
                if (!tc.hasMoodBeenAffected)
                {
                    switchMood(tc.changeMoodBy);
                   
                    tc.hasMoodBeenAffected = true;
                }

                break;
            }

        }

    }


    public void switchMood(int value)
    {

        currentMood += value;
            Vector3 screenToWorld =Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z) );
        playMoodSound();

    }


}
