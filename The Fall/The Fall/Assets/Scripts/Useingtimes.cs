using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Useingtimes : MonoBehaviour {
    private int times;
    public int maxtime = 3;
    public GameObject dis;
	// Use this for initialization
	void Start () {
        times = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerExit(Collider other)
	{
        if (other.tag == "ball") 
        {
            times = times + 1;
        }
        if (times > maxtime)
        {
            Destroy(dis);
        }
	}

}

