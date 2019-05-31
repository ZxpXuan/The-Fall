using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacube : MonoBehaviour {


  
    public float speed = 4.0f;
//    private bool _mouseDown = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
            
        transform.Rotate(Vector3.back, Time.deltaTime * speed, Space.World);

        
    }


	}

