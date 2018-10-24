using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BlueEND : MonoBehaviour {
    public bool blue = false;
    private int buildIndex;
    private bool green;
    private GreenEND g1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        green = g1.green;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (blue == true && green == true)
        {
            SceneManager.LoadScene(buildIndex + 1);
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.transform.tag == "blueball")
        {
            blue = true;
            Destroy(collision.gameObject);
        }
	}

}
