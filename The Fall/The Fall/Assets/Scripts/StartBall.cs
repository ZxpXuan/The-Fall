using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBall: MonoBehaviour {

    Vector3 screenPosition;
    Vector3 mousePositionOnScreen;
    Vector3 mousePositionInWorld;
    Vector3 direction;
    Vector3 V;
    private bool abale;
    public float foce = 10f;
    public Rigidbody Ball;

    public AudioSource shootBall;

	// Use this for initialization
	void Start () {
        abale = true;
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        MouseFollow();
    }
   
    void MouseFollow()
    {
        if (Input.GetMouseButtonDown(0) && abale == true)
        {
            shootBall.Play();
            screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            mousePositionOnScreen = Input.mousePosition;
            mousePositionOnScreen.z = screenPosition.z;
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            direction = new Vector3(mousePositionInWorld.x - Ball.transform.position.x, mousePositionInWorld.y - Ball.transform.position.y, 0.0f);
            V = direction.normalized;
            Ball.AddForce( V * foce, ForceMode.Impulse);
            abale = false;
        }

    }

}
