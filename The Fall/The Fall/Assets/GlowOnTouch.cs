using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowOnTouch : MonoBehaviour {
    public bool enableRenderer;
	// Use this for initialization
	void Start () {
        if(enableRenderer){

            GetComponent<MeshRenderer>().enabled = true;
        }
        else{

            GetComponent<MeshRenderer>().enabled = false;

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.layer==9){

            if (enableRenderer)
            {

               // GetComponent<MeshRenderer>().enabled = false;
                //enableRenderer = false;
            }
            else
            {

                GetComponent<MeshRenderer>().enabled = true;
                enableRenderer = true;


            }
        }
	}
}
