using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanceDis : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnCollisionEnter(Collision other)
	{
        if (other.collider.tag == "ball") 
        {
            Destroy(this.gameObject);
        }

	}
}
