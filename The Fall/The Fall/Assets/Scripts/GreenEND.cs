using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GreenEND : MonoBehaviour {

    public bool green = false;
    private int buildIndex;
    private bool blue;
    //private GameObject b;

    private BlueEND b1;
    // Use this for initialization
    void Start()
    {
        //b = GameObject.Find("");
        //b = GameObject.FindGameObjectWithTag("");
    }

    // Update is called once per frame
    void Update()
    {
        //blue = b.GetComponent<BlueEND>().blue;
        blue = b1.blue;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (blue == true && green == true)
        {
            SceneManager.LoadScene(buildIndex + 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "greenball")
        {
            green = true;
            Destroy(collision.gameObject);
        }
    }
}
