using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class limitation : MonoBehaviour {
    private int i = 0;
    public int limit = 5;
    private int buildIndex;
    public GameManager gm;
    
    public GameObject particleOnDestroy;
    [SerializeField]
    public ParticleSystem partSystem;
    [SerializeField]
    public GameObject bounceCount;
    // Use this for initialization
    void Start () {
        gm.updateBounces(limit - i);
        GetComponentInChildren<TextMesh>().text = "" + (limit - i);

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
        i = i + 1;
        Instantiate(partSystem.gameObject, transform.position, Quaternion.identity);
        gm.updateBounces(limit - i);
        GetComponent<FallingText>().spawnNumbers(7, limit - i);
       // GetComponentInChildren<TextMesh>().text = "" + (limit - i);
        //   partSystem.transform.position = transform.position;
        //     partSystem.Play();
        Debug.Log("t " + i);

        if (collision.collider.tag == "limitadd2")
        {
            limit = limit + 2;
        }


        if (i > limit || collision.collider.tag == "outsidewall")
        {
            //    SceneManager.LoadScene(buildIndex);
            Destroy(gameObject);
            Instantiate(particleOnDestroy, transform.position,Quaternion.identity);

            AkSoundEngine.PostEvent("Play_Death", gameObject);
            AkSoundEngine.PostEvent("Stop_Goal_Static", gameObject);
            gm.restartLevel();
        }


    }



   
}
