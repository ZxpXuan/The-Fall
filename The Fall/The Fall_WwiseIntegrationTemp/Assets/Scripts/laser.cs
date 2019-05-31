using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class laser : MonoBehaviour
{
    //number of times the ball has made contact (mike)
    public  int collisionCount;
    
    //audio script reference (mike)   

    // public float fireSpeed = 2f;
    public Rigidbody Ball;


    void Start()
    {
        // set our laser on its merry way. no need to update transform manually
       // this.rigidbody.velocity = Vector3.forward * fireSpeed;

        // freeze the rotation so it doesnt go spinning after a collision
        Ball.freezeRotation = true;

    }

    // we want to store the laser's velocity every frame
    // so we can use this data during collisions to reflect
    [SerializeField]
    private Vector3 oldVelocity;
    [SerializeField]
    private float minVelocity = 10f;
    void FixedUpdate()
    {
        // because we want the velocity after physics, we put this in fixed update
        oldVelocity = Ball.velocity;
    }

    // when a collision happens
    void OnCollisionEnter(Collision collision)
    {
        // get the point of contact (mike)
        ContactPoint contact = collision.contacts[0];

        AkSoundEngine.PostEvent("Play_Impact", gameObject);
        

        
      
        float speed = oldVelocity.magnitude;



        // reflect our old velocity off the contact point's normal vector
        Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, contact.normal);

        // assign the reflected velocity back to the rigidbody
        Ball.velocity = reflectedVelocity.normalized * speed;

    }


}