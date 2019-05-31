using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAngle : MonoBehaviour {
    [SerializeField] private float timeToFade = 2.5f;

    private float startTime;
    private bool hasFadeInStarted;
    private bool hasFadeOutStarted;
    public float fadeSpeed = 0.05f;
    private int currentOpacity;
    private float t;

    public MeshRenderer textMesh;
    // Use this for initialization
    void Start () {
        hasFadeOutStarted = false;
        hasFadeInStarted = false;
        t = 0;
	}
	
	// Update is called once per frame
	void Update () {
        /*  Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
          Vector3 objpos = Camera.main.WorldToViewportPoint(transform.position);        //Object position on screen
          Vector2 relobjpos = new Vector2(objpos.x - 0.5f, objpos.y - 0.5f);            //Set coordinates relative to object
          Vector2 relmousepos = new Vector2(mouse.x - 0.5f, mouse.y - 0.5f) - relobjpos;
          float angle = Vector2.Angle(Vector2.up, relmousepos);    //Angle calculation
          if (relmousepos.x > 0)
              angle = 360 - angle;
          Quaternion quat = Quaternion.identity;
          quat.eulerAngles = new Vector3(0, 0, angle); //Changing angle
          transform.rotation = quat;
          */
        var renderer = GetComponentInChildren<SpriteRenderer>();
                
            var color = renderer.color;

        if (hasFadeInStarted)
        {
            t += fadeSpeed * Time.deltaTime;
            color.a = Mathf.Lerp(color.a, 1, t);
            renderer.color = color;
            textMesh.material.color = color;

            if (color.a == 1)
            {
                hasFadeInStarted = false;
            }
        }

        if (hasFadeOutStarted)
        {
            t += fadeSpeed * Time.deltaTime;
            color.a = Mathf.Lerp(color.a, 0, t);
            renderer.color = color;
            textMesh.material.color = color;


            if(color.a == 0)
            {
                hasFadeOutStarted = false;
            }
        }

        var pos = Input.mousePosition;
        pos.z = -Camera.main.transform.position.z;
        transform.LookAt(Camera.main.ScreenToWorldPoint(pos), -Vector3.forward);
    }

    public void startFadeIn()
    {
        if (!hasFadeInStarted)
        {
            t = 0;
            hasFadeOutStarted = false;
            hasFadeInStarted = true;
            startTime = Time.time;
        }

    }

    public void startFadeOut()
    {
        if (!hasFadeOutStarted)
        {
            t = 0;

            hasFadeOutStarted = true;
            hasFadeInStarted = false;
            startTime = Time.time;
        }
    }
}
