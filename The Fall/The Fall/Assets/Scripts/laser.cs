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
        Ball.freezeRotation = true;
    }


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

        ContactPoint contact = collision.contacts[0];

        //AkSoundEngine.PostEvent("Play_Impact", gameObject);
        
        float speed = oldVelocity.magnitude;
        
        Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, contact.normal);
        
        Ball.velocity = reflectedVelocity.normalized * speed;

    }


}