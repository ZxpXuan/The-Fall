using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitation : MonoBehaviour {
    private int i = 0;
    public int limit = 5;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {

        i = i + 1;
        Debug.Log(i);
        if (i > limit)
        {
            Time.timeScale = 0;
        }
    }
}
