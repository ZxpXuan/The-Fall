using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class limitation : MonoBehaviour {
    private int i = 0;
    public int limit = 5;
    private int buildIndex;
    public GameManager gm;
    public NewAudioManager audioMan;
    
    public GameObject particleOnDestroy;
    [SerializeField]
    public ParticleSystem partSystem;
    // Use this for initialization
    void Start () {
        gm.updateBounces(limit - i);
        audioMan = NewAudioManager.instance;
    }
	
	// Update is called once per frame
	void Update () {
        getscene();
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
     //   partSystem.transform.position = transform.position;
   //     partSystem.Play();
        Debug.Log("t " + i);
        if (i > limit || collision.collider.tag == "outsidewall")
        {
            //    SceneManager.LoadScene(buildIndex);
            Destroy(gameObject);
            Instantiate(particleOnDestroy, transform.position,Quaternion.identity);
            audioMan.PlaySound("Death");
            gm.restartLevel();
        }
    }
}
