﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorup : MonoBehaviour
{
    public GameObject door;
    public GameObject trangle;
    private bool able;
    public float speed = 5.0f;
    private bool movable = false;
    public float upmos = 13;
    public float downmos = -1;
    //public float speedz;
    // Use this for initialization
    void Start()
    {
        able = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (door.transform.position.y < upmos)
        {
        if (movable == true)
        {

            door.transform.Translate(Vector3.up * Time.deltaTime * speed, Space.Self);
            

        }
        }

        if (trangle.transform.position.y > downmos)
        {
            if (movable == true)
            {
                trangle.transform.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
            }
            
        }
    }

    void OnCollisionEnter(Collision collision)

    {
        if (collision.collider.gameObject.tag == "ball" && able == true)
        {
            //speedz = speed * Time.deltaTime;
            
            able = false;
            movable = true;

        }


    }
}
