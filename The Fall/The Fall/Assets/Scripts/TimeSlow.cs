using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour {
    public GameObject ball;
    public GameObject end;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Vector3 distan = new Vector3(ball.transform.position.x - this.transform.position.x, ball.transform.position.y - this.transform.position.y, 0);
        float distance = (ball.transform.position - this.transform.position).magnitude;

        if (distance < 2.5 && Camera.main.GetComponent<Camera>().fieldOfView > 10)
        {
            AkSoundEngine.SetRTPCValue("Time_Slow", 1);
            Camera.main.transform.LookAt(end.transform);
            Time.timeScale = 0.5f;
            Camera.main.GetComponent<Camera>().fieldOfView = Camera.main.GetComponent<Camera>().fieldOfView - 0.5f;

        }
        if(distance > 2.5)
        {
            Time.timeScale = 1;
            AkSoundEngine.SetRTPCValue("Time_Slow", 0);
        }

	}
}
