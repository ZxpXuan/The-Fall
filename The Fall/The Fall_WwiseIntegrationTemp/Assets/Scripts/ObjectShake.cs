using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour {
    bool Up;
    public float shakeDuration;
    public bool isShaking;
    private float startTime;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (isShaking && startTime + shakeDuration > Time.time) { 

        if (Up)
        {
            transform.Translate(0.1f, 0.1f, 0);
            Up = false;
        }
        else
        {
            transform.Translate(-0.1f, -0.1f, 0);
            Up = true;
        }
    }
    else{
            isShaking=  false;
    }
    }

    private void OnCollisionEnter(Collision collision)
    {

        print("shaking;");

        isShaking = true;
        startTime = Time.time;
    }
}
