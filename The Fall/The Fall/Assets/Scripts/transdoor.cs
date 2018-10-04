using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transdoor : MonoBehaviour {
    public GameObject otherportal;
    private Vector3 orivelocity;
    private Vector3 nevelocity;
    public Rigidbody ball;
    private float angle = 90;

    public AudioSource portalSFX;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        orivelocity = ball.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {

        /*if (other.tag == "ball")
        {
            portalSFX.Play();
            print("sound");
        }*/
        
        if(other.tag == "ball" && otherportal.tag == "no")
        {


            var x = orivelocity.x;
            var y = orivelocity.y;
            //var z = orivelocity.z;
            var sin = Mathf.Sin(Mathf.PI * angle / 180);
            var cos = Mathf.Cos(Mathf.PI * angle / 180);
            var newX = x * cos + y * sin;
            var newY = x * -sin + y * cos;
            nevelocity = new Vector3(newX,newY,0);

            //if(otherportal.transform.rotation.z == 0)
            //{
            //    other.transform.position = otherportal.transform.position;
            //}
            //if (otherportal.transform.rotation.z != 0)
            //{
                other.transform.position = new Vector3(otherportal.transform.position.x + 1, otherportal.transform.position.y, otherportal.transform.position.z);
                ball.velocity = nevelocity;

          //  }



        }
        if (other.tag == "ball" && otherportal.tag == "noandhori")
        {


            var x = orivelocity.x;
            var y = orivelocity.y;
            //var z = orivelocity.z;
            var sin = Mathf.Sin(Mathf.PI * angle / 180);
            var cos = Mathf.Cos(Mathf.PI * angle / 180);
            var newX = x * cos + y * sin;
            var newY = x * -sin + y * cos;
            nevelocity = new Vector3(newX, newY, 0);

            //if(otherportal.transform.rotation.z == 0)
            //{
            //    other.transform.position = otherportal.transform.position;
            //}
            //if (otherportal.transform.rotation.z != 0)
            //{
            other.transform.position = new Vector3(otherportal.transform.position.x, otherportal.transform.position.y + 1, otherportal.transform.position.z);
            ball.velocity = nevelocity;

            //  }



        }
        if (other.tag == "ball" && otherportal.tag == "leftportal")
        {
            portalSFX.Play();
            other.transform.position = new Vector3(otherportal.transform.position.x + 1, otherportal.transform.position.y, otherportal.transform.position.z);
        }
        if (other.tag == "ball" && otherportal.tag == "rightportal" )
        {
            portalSFX.Play();
            other.transform.position = new Vector3(otherportal.transform.position.x - 1, otherportal.transform.position.y, otherportal.transform.position.z);
        }
        if (other.tag == "ball" && otherportal.tag == "horidown")
        {
            portalSFX.Play();
            other.transform.position = new Vector3(otherportal.transform.position.x, otherportal.transform.position.y + 1, otherportal.transform.position.z);
        }
        if (other.tag == "ball" && otherportal.tag == "horiup")
        {
            portalSFX.Play();
            other.transform.position = new Vector3(otherportal.transform.position.x, otherportal.transform.position.y - 1, otherportal.transform.position.z);
        }

    }

}
