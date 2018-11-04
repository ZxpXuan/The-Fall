using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    [System.Serializable]
    public class DialogueSet{
        public Brain.MoodTypes moodType;

        public List<AudioClip> dialogue;

        public bool randomize;

    }
    [System.Serializable]
    public class Categoriser
    {
        public Brain.MoodTypes moodTypes;
        public Brain.TrackingType trackingType;
        public float fromRange;
        public float toRange;


    }

    [SerializeField]
    public List<DialogueSet> dialogueSet;

    [SerializeField]
    public List<Categoriser> moodSet;


    public int currentLevelTries;
    public int totalWorldTries;
    public Brain.MoodTypes currentMood = Brain.MoodTypes.Neutral;
    public int happinessLevel;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void calculateCurrentMood(){



    }



}
