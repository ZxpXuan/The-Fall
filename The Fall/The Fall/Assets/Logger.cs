using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LogPlayEvent()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Main ", "Play", "true");

    }

    public void LogLevelSelect()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Main ", "LevelSelect", "true");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
