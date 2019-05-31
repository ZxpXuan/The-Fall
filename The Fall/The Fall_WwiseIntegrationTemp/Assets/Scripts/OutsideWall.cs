using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)

    {
        if (collision.collider.gameObject.tag == "ball")
        {
            //speedz = speed * Time.deltaTime;
            Destroy(collision.collider.gameObject);

        }


    }
}
