using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwEmitterStript : MonoBehaviour {

   /* public string EventName = "default";
    public string StopEvent = "default";
    public bool IsInCollider = false;

	// Use this for initialization
	void Start ()
    {
        AkSoundEngine.RegisterGameObj(gameObject);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTrigger(Collider other)
    {
        if(other.tag != "Player" || IsInCollider) { return; }
        IsInCollider = true;
        AkSoundEngine.PostEvent(EventName, gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" || IsInCollider) { return; }
        AkSoundEngine.PostEvent(StopEvent, gameObject);
        IsInCollider = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" || IsInCollider) { return; }
        IsInCollider = true;
        AkSoundEngine.PostEvent(StopEvent, gameObject);

    }*/
}
