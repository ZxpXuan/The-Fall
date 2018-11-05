using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody))]
public class limitation : MonoBehaviour {
    private int i = 0;
    public int limit = 5;
    private int buildIndex;
    public GameManager gm;

    //public int limitadd = 2;
    
    public GameObject particleOnDestroy;
    [SerializeField]
    public ParticleSystem partSystem;
    [SerializeField]
    public GameObject bounceCount;






    //number of times the ball has made contact (mike)
    public int collisionCount;

    //audio script reference (mike)   

    // public float fireSpeed = 2f;
    public Rigidbody Ball;

    // Use this for initialization
    void Start () {
        gm.updateBounces(limit - i);
        GetComponentInChildren<TextMesh>().text = "" + (limit - i);
        Ball.freezeRotation = true;
    }
    public void addBounce(int value)
    {

        limit += value;
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


    // Update is called once per frame
    void Update () {
        getscene();

        AkSoundEngine.SetRTPCValue("Bounce_Count", i);
	}
    void getscene()
    {
        //sceneName = SceneManager.GetActiveScene().name;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }
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





        i = i + 1;
        Instantiate(partSystem.gameObject, transform.position, Quaternion.identity);
        gm.updateBounces(limit - i);
        GetComponent<FallingText>().spawnNumbers(7, limit - i);
       // GetComponentInChildren<TextMesh>().text = "" + (limit - i);
        //   partSystem.transform.position = transform.position;
        //     partSystem.Play();


        if (collision.collider.tag == "limitadd2")
        {
          //  limit = limit + limitadd;
        }


        if (i > limit || collision.collider.tag == "outsidewall")
        {
            //    SceneManager.LoadScene(buildIndex);
            Destroy(gameObject);
            Instantiate(particleOnDestroy, transform.position,Quaternion.identity);

            AkSoundEngine.PostEvent("Play_Death", gameObject);
            AkSoundEngine.PostEvent("Stop_Goal_Static", gameObject);
            FindObjectOfType<AI>().setMood(Brain.MoodTypes.Death);

            gm.restartLevel();
        }


    }



   
}
