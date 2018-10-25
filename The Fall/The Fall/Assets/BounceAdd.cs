using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAdd : MonoBehaviour {
    [SerializeField]
    public int bounceAdd;

    public bool hasAdded;
	// Use this for initialization
	void Start () {
        hasAdded = false;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!hasAdded)
        {
            other.GetComponent<limitation>().addBounce(bounceAdd);
            hasAdded = true;
        }
    
    }
}
