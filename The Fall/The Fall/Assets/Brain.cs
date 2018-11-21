using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {
  
    public enum MoodTypes { VDisappointed, Disappointed, Neutral, Happy, VHappy };
    public enum GameState { Start, MidWay, Impatient, VeryImpatient, Death, Winner, Mute, Tutorial };

    public enum TimeTaker { TooLong,Long,Normal,Short,JustStarted};
    public enum TrackingType { Time, Tries };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
