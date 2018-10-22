using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour {
    public GameObject ball;
    public GameObject end;
    public float timeSlow;
    public float minDistance;
    public float maxZoom;
   // public float zoomDistance;
    float distance;
    private Vector3 oldCamTransform;

    private bool isCamZooming;
    private float oldCamFieldOfView;
	// Use this for initialization
	void Start () {
        isCamZooming = false;
        Time.timeScale = 1;

	}
	
	// Update is called once per frame
	void Update () {
        // Vector3 distan = new Vector3(ball.transform.position.x - this.transform.position.x, ball.transform.position.y - this.transform.position.y, 0);

        if (ball!=null)
        distance = (ball.transform.position - this.transform.position).magnitude;

        if (distance < minDistance && Camera.main.GetComponent<Camera>().fieldOfView > maxZoom)
        {
            if(!isCamZooming ){

                isCamZooming = true;
                oldCamTransform = Camera.main.transform.position;
                oldCamFieldOfView = Camera.main.fieldOfView;
            }
           
            
            
            Time.timeScale = timeSlow;
            AkSoundEngine.SetRTPCValue("Time_Slow", 0);

            Camera.main.GetComponent<Camera>().fieldOfView = Camera.main.GetComponent<Camera>().fieldOfView - 0.7f;
            // Camera.main.transform.LookAt(end.transform);
            //print("1- " + Camera.main.transform.position);
            Camera.main.transform.position = new Vector3(end.transform.position.x,end.transform.position.y,Camera.main.transform.position.z);
            //print("2- " + Camera.main.transform.position);

        }
        if(distance > minDistance && isCamZooming )
        {
            if (Camera.main.fieldOfView < oldCamFieldOfView)
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, oldCamFieldOfView, 0.4f);

            }else{
                isCamZooming = false;
            }
            Camera.main.transform.position = oldCamTransform;
            //print("3- " + Camera.main.transform.position);

            Camera.main.transform.rotation = Quaternion.Euler(0,0,0);
            Time.timeScale = 1;

            AkSoundEngine.SetRTPCValue("Time_Slow", 1);
        }

	}
}
