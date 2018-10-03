using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cyclicmoving1 : MonoBehaviour
{
    public float movingblockspeed = 0.02f;
    private float movingblocktime = 0.1f;
    //public float mixtime = 20f;
    public float midtime = 10f;
    public GameObject cycblock;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        movingblocktime += 0.1f;
        if (movingblocktime < midtime)
        {
            cycblock.transform.Translate(Vector3.up * movingblockspeed);
        }
        if (movingblocktime > midtime)
        {
            cycblock.transform.Translate(Vector3.down * movingblockspeed);
            if (movingblocktime > midtime * 2)
            {
                movingblocktime = 0.1f;
            }

        }
        //float TranslateSpeed = 0.02f;
        //float TranslateSpeedTime = 0.1f;
        //TranslateSpeedTime += 0.1f;
        //transform.Translate(Vector3.right * TranslateSpeed);
        //if (TranslateSpeedTime > 150.0f)
        //{
        //    cycblock.transform.Rotate(0, 180, 0); 
        //    TranslateSpeedTime = 0.1f;

        //}



    }
}
